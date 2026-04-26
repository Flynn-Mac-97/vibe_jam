// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): active side label, phase cooldown bar.
// Extend (GAMEPLAY): add collectible count text, level timer, combo display.
// Requires: TMP package. PlayerAbilityController on the Player. Image with fillAmount for bar.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private void Update()
        {
            if (playerAbility == null) return;

            // ---------- GAMEPLAY ----------
            if (activeSideText != null)
                activeSideText.text = playerAbility.CurrentSideLabel;

            if (phaseCooldownBar != null)
                phaseCooldownBar.fillAmount = playerAbility.PhaseCooldownFraction;
        }
    }
}
