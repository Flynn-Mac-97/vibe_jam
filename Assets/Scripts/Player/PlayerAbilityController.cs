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

        public AbilityType CurrentAbilityType => _currentZone != null ? _currentZone.AbilityType : AbilityType.None;
        public float PhaseReady01 => phaseCooldown <= 0f ? 1f : 1f - Mathf.Clamp01(_phaseTimer / phaseCooldown);
        public bool CanPhase => _currentZone != null && _currentZone.MirrorZone != null && _phaseTimer <= 0f;

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

        private IEnumerator PhaseCoroutine(AbilityZone targetZone)
        {
            // Mirror player's offset within the current zone to the target zone
            Vector3 offsetInZone = transform.position - _currentZone.transform.position;
            offsetInZone.x = -offsetInZone.x; // reflect on X axis
            Vector3 destination = targetZone.transform.position + offsetInZone;

            _cc.enabled = false;
            transform.position = destination;
            yield return null; // wait one frame for physics to settle
            _cc.enabled = true;
        }
    }
}
