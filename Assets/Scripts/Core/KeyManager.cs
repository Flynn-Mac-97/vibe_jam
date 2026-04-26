// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): singleton lifecycle, register/collect flow, AllCollected bool, OnKeyCollected event.
// Extend (GAMEPLAY): add per-level required count override, play pickup SFX here, save collected state.
// Requires: KeyPickup scripts in scene to register themselves on Awake.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using UnityEngine.Events;

namespace VibeJam.Core
{
    /// <summary>
    /// Singleton that tracks how many keys have been collected vs. total placed in the scene.
    /// Add to the GameManager GameObject. KeyPickup instances register themselves automatically.
    /// </summary>
    public class KeyManager : MonoBehaviour
    {
        // ---------- INFRA ----------
        public static KeyManager Instance { get; private set; }

        public int CollectedCount { get; private set; }
        public int TotalCount { get; private set; }
        public bool AllCollected => TotalCount > 0 && CollectedCount >= TotalCount;

        [Tooltip("Fired each time a key is collected. Passes current collected count.")]
        public UnityEvent<int> OnKeyCollected = new UnityEvent<int>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            CollectedCount = 0;
            TotalCount = 0;
        }

        // ---------- GAMEPLAY ----------
        /// <summary>Called by KeyPickup.Awake() to register a key in the scene.</summary>
        public void Register()
        {
            TotalCount++;
        }

        /// <summary>Called by KeyPickup when the player touches it.</summary>
        public void Collect()
        {
            if (CollectedCount >= TotalCount) return;
            CollectedCount++;
            OnKeyCollected.Invoke(CollectedCount);
        }
    }
}
