// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): singleton lifecycle, TakeDamage, ResetLives, OnLivesChanged event.
// Extend (GAMEPLAY): add invincibility frames after hit, extra-life pickups, lives persistence.
// Requires: GameStateMachine in scene.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using UnityEngine.Events;

namespace VibeJam.Core
{
    /// <summary>
    /// Singleton that owns the player's life count.
    /// Add to the GameManager GameObject alongside GameStateMachine.
    /// </summary>
    public class LivesManager : MonoBehaviour
    {
        // ---------- INFRA ----------
        public static LivesManager Instance { get; private set; }

        [Tooltip("How many lives the player starts with each run.")]
        [SerializeField] private int startingLives = 3;

        public int CurrentLives { get; private set; }

        [Tooltip("Fired whenever lives change. Passes new life count.")]
        public UnityEvent<int> OnLivesChanged = new UnityEvent<int>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            CurrentLives = startingLives;
        }

        // ---------- GAMEPLAY ----------
        /// <summary>
        /// Decrement lives by one. If lives reach zero, triggers GameOver via GameStateMachine.
        /// </summary>
        public void TakeDamage()
        {
            if (GameStateMachine.Instance != null &&
                GameStateMachine.Instance.State != GameState.Playing) return;

            CurrentLives = Mathf.Max(0, CurrentLives - 1);
            OnLivesChanged.Invoke(CurrentLives);

            if (CurrentLives <= 0)
                GameStateMachine.Instance?.GameOver();
        }

        /// <summary>Reset lives to starting value (called via scene reload).</summary>
        public void ResetLives()
        {
            CurrentLives = startingLives;
            OnLivesChanged.Invoke(CurrentLives);
        }
    }
}
