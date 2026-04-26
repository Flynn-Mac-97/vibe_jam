// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): trigger detection, GameStateMachine.Win() call.
// Extend (GAMEPLAY): play win VFX/SFX here, unlock next level, show per-level stats.
// Requires: Collider on this GameObject (set to isTrigger automatically). Player tagged "Player".
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;

namespace VibeJam.Core
{
    /// <summary>
    /// Place on a trigger volume at the end of the level.
    /// When the Player enters, calls GameStateMachine.Instance.Win().
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class GoalZone : MonoBehaviour
    {
        // ---------- INFRA ----------
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // ---------- GAMEPLAY ----------
            GameStateMachine.Instance?.Win();
        }
    }
}
