// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): singleton lifecycle, state enum, Win/Restart flow.
// Extend (GAMEPLAY): add Paused state, death count, time tracking, level progression.
// Requires: SceneManager (UnityEngine.SceneManagement). No other dependencies.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using VibeJam.Core;

namespace VibeJam.Core
{
    public enum GameState { Playing, Win, GameOver }

    /// <summary>
    /// Singleton game state machine. Controls the Playing → Win flow.
    /// Add to a GameManager GameObject in the scene.
    /// </summary>
    public class GameStateMachine : MonoBehaviour
    {
        // ---------- INFRA ----------
        public static GameStateMachine Instance { get; private set; }

        public GameState State { get; private set; } = GameState.Playing;

        [Tooltip("Fired when the player reaches the goal.")]
        public UnityEvent OnWin = new UnityEvent();

        [Tooltip("Fired when the player runs out of lives.")]
        public UnityEvent OnGameOver = new UnityEvent();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if ((State == GameState.Win || State == GameState.GameOver) && InputMap.GetRestartDown())
                Restart();
        }

        // ---------- GAMEPLAY ----------
        /// <summary>Call this when the player reaches the GoalZone.</summary>
        public void Win()
        {
            if (State != GameState.Playing) return;
            State = GameState.Win;
            OnWin.Invoke();
        }

        /// <summary>Call this when the player runs out of lives.</summary>
        public void GameOver()
        {
            if (State != GameState.Playing) return;
            State = GameState.GameOver;
            OnGameOver.Invoke();
        }

        /// <summary>Reload the current scene from scratch.</summary>
        public void Restart()
        {
            State = GameState.Playing;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
