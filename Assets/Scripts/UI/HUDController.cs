// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): active side label, phase cooldown bar, keys display, lives display, gate-locked flash.
// Extend (GAMEPLAY): add level timer, combo display, score counter.
// Requires: TMP package. PlayerAbilityController on the Player. Image with fillAmount for bar.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VibeJam.Core;
using VibeJam.Player;

namespace VibeJam.UI
{
    /// <summary>
    /// Drives the in-game HUD each frame.
    /// Attach to a Canvas child. Wire all Inspector refs.
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Header("Player")]
        [SerializeField] private PlayerAbilityController playerAbility;

        [Header("HUD Elements")]
        [Tooltip("Shows which side the player is on (Left = Hell, Right = Heaven).")]
        [SerializeField] private TMP_Text activeSideText;

        [Tooltip("Image (Fill Method = Horizontal). fillAmount 0 = on cooldown, 1 = ready.")]
        [SerializeField] private Image phaseCooldownBar;

        [Tooltip("Displays collected vs. total keys, e.g. \"Keys: 1/3\".")]
        [SerializeField] private TMP_Text keysText;

        [Tooltip("Displays current lives, e.g. \"Lives: 3\".")]
        [SerializeField] private TMP_Text livesText;

        [Tooltip("Shown briefly in red when the player tries the gate without all keys.")]
        [SerializeField] private TMP_Text gateLockedText;

        [Tooltip("How many seconds the gate-locked message stays visible.")]
        [SerializeField] private float gateLockedDuration = 2f;

        private Coroutine _gateLockedCoroutine;

        private void Start()
        {
            if (gateLockedText != null) gateLockedText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (playerAbility == null) return;

            // ---------- GAMEPLAY ----------
            if (activeSideText != null)
                activeSideText.text = playerAbility.CurrentSideLabel;

            if (phaseCooldownBar != null)
                phaseCooldownBar.fillAmount = playerAbility.PhaseCooldownFraction;

            if (keysText != null && KeyManager.Instance != null)
                keysText.text = $"Keys: {KeyManager.Instance.CollectedCount}/{KeyManager.Instance.TotalCount}";

            if (livesText != null && LivesManager.Instance != null)
                livesText.text = $"Lives: {LivesManager.Instance.CurrentLives}";
        }

        /// <summary>
        /// Called (e.g. via GoalZone.OnLockedAttempt Inspector event) to flash the locked message.
        /// </summary>
        public void ShowGateLockedMessage()
        {
            if (gateLockedText == null) return;
            if (_gateLockedCoroutine != null) StopCoroutine(_gateLockedCoroutine);
            _gateLockedCoroutine = StartCoroutine(GateLockedFlash());
        }

        private IEnumerator GateLockedFlash()
        {
            gateLockedText.text = "Collect all keys first!";
            gateLockedText.gameObject.SetActive(true);
            yield return new WaitForSeconds(gateLockedDuration);
            gateLockedText.gameObject.SetActive(false);
            _gateLockedCoroutine = null;
        }
    }
}
