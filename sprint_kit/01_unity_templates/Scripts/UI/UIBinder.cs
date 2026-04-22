// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): panel show/hide bound to GameStateMachine events; score/high-score text bound to ScoreManager events.
// Extend (GAMEPLAY): add extra text refs (combo, multiplier) and subscribe in OnEnable. Keep UI prefabs decoupled from gameplay scripts — all UI flow goes through this binder.
// Wire buttons to OnClickStart / OnClickPause / etc. via inspector.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using TMPro;
using VibeJam.Core;
using VibeJam.Score;

namespace VibeJam.UI
{
    public class UIBinder : MonoBehaviour
    {
        // ---------- INFRA / Panels ----------
        [Header("Panels")]
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject hudPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header("Texts (TMP)")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private TMP_Text finalHighScoreText;

        private void OnEnable()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnStateChanged += HandleStateChanged;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged     += HandleScoreChanged;
                ScoreManager.Instance.OnHighScoreChanged += HandleHighScoreChanged;
                HandleScoreChanged(ScoreManager.Instance.Current);
                HandleHighScoreChanged(ScoreManager.Instance.High);
            }

            if (GameStateMachine.Instance != null)
                HandleStateChanged(GameStateMachine.Instance.State, GameStateMachine.Instance.State);
        }

        private void OnDisable()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnStateChanged -= HandleStateChanged;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged     -= HandleScoreChanged;
                ScoreManager.Instance.OnHighScoreChanged -= HandleHighScoreChanged;
            }
        }

        private void HandleStateChanged(GameState prev, GameState next)
        {
            if (startPanel    != null) startPanel.SetActive(next == GameState.Menu);
            if (hudPanel      != null) hudPanel.SetActive(next == GameState.Playing || next == GameState.Paused);
            if (pausePanel    != null) pausePanel.SetActive(next == GameState.Paused);
            if (gameOverPanel != null) gameOverPanel.SetActive(next == GameState.GameOver);

            if (next == GameState.GameOver && ScoreManager.Instance != null)
            {
                if (finalScoreText     != null) finalScoreText.text     = $"Score: {ScoreManager.Instance.Current}";
                if (finalHighScoreText != null) finalHighScoreText.text = $"Best: {ScoreManager.Instance.High}";
            }
        }

        private void HandleScoreChanged(int s)
        {
            if (scoreText != null) scoreText.text = s.ToString();
        }

        private void HandleHighScoreChanged(int s)
        {
            if (highScoreText != null) highScoreText.text = $"Best: {s}";
        }

        // ---------- INFRA / Button hookups ----------
        public void OnClickStart()         { if (GameStateMachine.Instance != null) GameStateMachine.Instance.StartPlaying(); }
        public void OnClickPause()         { if (GameStateMachine.Instance != null) GameStateMachine.Instance.Pause(); }
        public void OnClickResume()        { if (GameStateMachine.Instance != null) GameStateMachine.Instance.Resume(); }
        public void OnClickRestart()       { if (GameStateMachine.Instance != null) GameStateMachine.Instance.StartPlaying(); }
        public void OnClickReturnToMenu()  { if (GameStateMachine.Instance != null) GameStateMachine.Instance.ReturnToMenu(); }
        public void OnClickQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        // ---------- GAMEPLAY ----------
        // Add combo text, multiplier bars, etc. here.
    }
}
