// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): Current / High score, PlayerPrefs persistence, OnScoreChanged event.
// Extend (GAMEPLAY): add multipliers, combos, per-category scores below GAMEPLAY marker. Always raise OnScoreChanged after mutation.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using System;
using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Score
{
    public class ScoreManager : MonoBehaviour
    {
        // ---------- INFRA ----------
        public static ScoreManager Instance { get; private set; }

        private const string PrefKey = "VibeJam.HighScore";

        public int Current { get; private set; }
        public int High { get; private set; }

        public event Action<int> OnScoreChanged;
        public event Action<int> OnHighScoreChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            High = PlayerPrefs.GetInt(PrefKey, 0);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void OnEnable()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnStateChanged += HandleStateChanged;
        }

        private void OnDisable()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnStateChanged -= HandleStateChanged;
        }

        private void HandleStateChanged(GameState prev, GameState next)
        {
            // Reset when entering Playing from Menu OR GameOver (i.e., new run).
            if (next == GameState.Playing && prev != GameState.Paused) ResetScore();
        }

        public void Add(int amount)
        {
            Current += amount;
            OnScoreChanged?.Invoke(Current);
            if (Current > High)
            {
                High = Current;
                PlayerPrefs.SetInt(PrefKey, High);
                OnHighScoreChanged?.Invoke(High);
            }
        }

        public void ResetScore()
        {
            Current = 0;
            OnScoreChanged?.Invoke(Current);
        }

        // ---------- GAMEPLAY ----------
        // Add multipliers, combos, per-category score buckets here.
    }
}
