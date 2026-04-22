// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): the GameState enum, the Set() method, the OnStateChanged event.
// Extend (GAMEPLAY): add new transition convenience methods below the INFRA region.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using System;
using UnityEngine;

namespace VibeJam.Core
{
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver,
    }

    public class GameStateMachine : MonoBehaviour
    {
        // ---------- INFRA ----------
        public static GameStateMachine Instance { get; private set; }

        public GameState State { get; private set; } = GameState.Menu;

        public event Action<GameState, GameState> OnStateChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void Set(GameState next)
        {
            if (next == State) return;
            var prev = State;
            State = next;
            OnStateChanged?.Invoke(prev, next);
        }

        // ---------- GAMEPLAY ----------
        public void StartPlaying() => Set(GameState.Playing);
        public void Pause()        { if (State == GameState.Playing) Set(GameState.Paused); }
        public void Resume()       { if (State == GameState.Paused)  Set(GameState.Playing); }
        public void EndGame()      => Set(GameState.GameOver);
        public void ReturnToMenu() => Set(GameState.Menu);
    }
}
