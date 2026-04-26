// AI EXTENSION HINTS
// Stable (INFRA): zone enter/exit dispatch, ability activation, phase teleport coroutine.
// Extend (GAMEPLAY): add phase cooldown timer, phase VFX spawn, phase sound trigger.
// Requires: PlayerController, at least one IAbility implementation — all on same GameObject.
// This script follows VibeJam conventions — see agents/AGENTS.md.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Player
{
    /// <summary>
    /// Manages the player's active ability based on the zone they occupy,
    /// and handles phasing to the mirrored zone on button press (E / Enter).
    /// Attach to the Player GameObject alongside PlayerController.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAbilityController : MonoBehaviour
    {
        // ---------- INFRA ----------
        private PlayerController _playerController;
        private CharacterController _cc;

        // All IAbility components present on this GameObject
        private readonly List<IAbility> _allAbilities = new List<IAbility>();

        private IAbility _activeAbility;
        private AbilityZone _currentZone;

        [SerializeField] private float phaseCooldown = 0.5f;
        private float _phaseTimer;

        // ---------- Public HUD API ----------
        /// <summary>0 = on cooldown, 1 = fully ready. Use for fillAmount on a UI bar.</summary>
        public float PhaseCooldownFraction => phaseCooldown > 0f ? Mathf.Clamp01(1f - (_phaseTimer / phaseCooldown)) : 1f;

        /// <summary>Display name of the current zone (e.g. "Left", "Right"), or "—" if between zones.</summary>
        public string CurrentSideLabel => _currentZone != null ? _currentZone.name : "—";

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _cc = GetComponent<CharacterController>();

            // Discover all ability modules on this GameObject
            foreach (var ability in GetComponents<IAbility>())
                _allAbilities.Add(ability);
        }

        // ---------- Called by PlayerController.Update (before Move) ----------
        public void ProcessAbility()
        {
            _activeAbility?.Execute(_playerController);

            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f && _currentZone != null && _currentZone.MirrorZone != null)
            {
                if (InputMap.GetPhaseLeftDown() || InputMap.GetPhaseRightDown())
                {
                    _phaseTimer = phaseCooldown;
                    StartCoroutine(PhaseCoroutine(_currentZone.MirrorZone));
                }
            }
        }

        // ---------- Called by AbilityZone ----------
        public void EnterZone(AbilityZone zone)
        {
            _currentZone = zone;
            ActivateAbilityForType(zone.AbilityType);
        }

        public void ExitZone(AbilityZone zone)
        {
            if (_currentZone == zone)
            {
                _currentZone = null;
                DeactivateCurrent();
            }
        }

        // ---------- GAMEPLAY ----------
        private void ActivateAbilityForType(AbilityType type)
        {
            DeactivateCurrent();
            foreach (var ability in _allAbilities)
            {
                if (ability.AbilityType == type)
                {
                    _activeAbility = ability;
                    _activeAbility.OnActivate();
                    return;
                }
            }
        }

        private void DeactivateCurrent()
        {
            _activeAbility?.OnDeactivate();
            _activeAbility = null;
        }

        // ---------- Phase VFX settings ----------
        [Header("Phase VFX")]
        [Tooltip("Total time (seconds) for the full phase animation (fade-out + fade-in).")]
        [SerializeField] private float phaseDuration = 0.45f;

        [Tooltip("Color flicker tint applied briefly when the player materialises on the other side.")]
        [SerializeField] private Color phaseFlickerColor = new Color(0.5f, 0.8f, 1f, 1f);

        private IEnumerator PhaseCoroutine(AbilityZone targetZone)
        {
            // Compute destination before any movement
            Vector3 offsetInZone = transform.position - _currentZone.transform.position;
            offsetInZone.x = -offsetInZone.x;
            Vector3 destination = targetZone.transform.position + offsetInZone;

            var sr = GetComponent<SpriteRenderer>();
            if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();

            float halfDur  = phaseDuration * 0.5f;
            float elapsed  = 0f;
            Color baseColor = sr != null ? sr.color : Color.white;
            // Ensure we start from full opacity in case a previous phase was interrupted
            baseColor.a = 1f;

            _cc.enabled = false;

            // ---- Phase OUT: fade to transparent + scale down ----
            while (elapsed < halfDur)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / halfDur);
                float eased = 1f - (t * t); // ease-in quad

                if (sr != null)
                {
                    Color c = baseColor;
                    c.a = eased;
                    // Tint toward phase colour as we fade
                    sr.color = Color.Lerp(c, phaseFlickerColor * new Color(1,1,1, c.a), t * 0.6f);
                }
                // Scale squish toward border (pull effect on X)
                float scaleX = Mathf.Lerp(1f, 0.05f, t * t);
                float scaleY = Mathf.Lerp(1f, 1.3f,  t * 0.5f);
                transform.localScale = new Vector3(scaleX, scaleY, 1f);

                yield return null;
            }

            // ---- Teleport at zero scale / full transparent ----
            transform.position  = destination;
            transform.localScale = new Vector3(0.05f, 1.3f, 1f);
            if (sr != null) { Color c = baseColor; c.a = 0f; sr.color = c; }

            yield return null; // one frame for physics to settle

            // ---- Phase IN: scale up + fade back in ----
            elapsed = 0f;
            while (elapsed < halfDur)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / halfDur);
                float eased = t * (2f - t); // ease-out quad

                if (sr != null)
                {
                    // Flicker: flash the phase colour early, return to base
                    float flickerT = Mathf.Clamp01((t - 0.1f) / 0.5f);
                    Color arriving = Color.Lerp(phaseFlickerColor, baseColor, flickerT);
                    arriving.a = eased;
                    sr.color = arriving;
                }
                // Expand from squished back to normal with slight overshoot
                float overshoot = 1f + Mathf.Sin(t * Mathf.PI) * 0.12f;
                float scaleX = Mathf.Lerp(0.05f, overshoot, eased);
                float scaleY = Mathf.Lerp(1.3f,  1f / overshoot, eased);
                transform.localScale = new Vector3(scaleX, scaleY, 1f);

                yield return null;
            }

            // Snap back to exact values
            transform.localScale = Vector3.one;
            if (sr != null) sr.color = baseColor;

            _cc.enabled = true;
        }
    }
}
