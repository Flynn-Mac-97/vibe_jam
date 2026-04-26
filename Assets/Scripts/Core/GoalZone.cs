// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): trigger detection, key-check gate, GameStateMachine.Win() call.
// Extend (GAMEPLAY): play win VFX/SFX here, unlock next level, show per-level stats.
// Requires: Collider on this GameObject (set to isTrigger automatically). Player tagged "Player".
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using UnityEngine.Events;

namespace VibeJam.Core
{
    /// <summary>
    /// Place on a trigger volume at the end of the level (Heaven Gate).
    /// When the Player enters, checks all keys are collected before allowing the win.
    /// If locked, fires OnLockedAttempt so the HUD can show feedback.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class GoalZone : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Tooltip("Fired when the player tries to enter but hasn't collected all keys yet.")]
        public UnityEvent OnLockedAttempt = new UnityEvent();

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // ---------- GAMEPLAY ----------
            if (KeyManager.Instance != null && !KeyManager.Instance.AllCollected)
            {
                OnLockedAttempt.Invoke();
                return;
            }

            GameStateMachine.Instance?.Win();
        }
    }
}
