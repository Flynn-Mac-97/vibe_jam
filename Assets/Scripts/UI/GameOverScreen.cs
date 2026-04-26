// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): show/hide panel on game over, restart hook.
// Extend (GAMEPLAY): add death count display, high-score comparison, animated panel.
// Requires: GameStateMachine in scene. TMP package.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.UI
{
    /// <summary>
    /// Shows the Game Over overlay when GameStateMachine fires OnGameOver.
    /// Attach to the Canvas (or a child). Assign panel in Inspector — it will be hidden at start.
    /// Press R (GetRestartDown) is handled by GameStateMachine.Update() — no duplicate logic here.
    /// </summary>
    public class GameOverScreen : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Tooltip("Root GameObject of the game-over overlay. Hidden at Start, shown on game over.")]
        [SerializeField] private GameObject panel;

        private void Start()
        {
            if (panel != null) panel.SetActive(false);

            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnGameOver.AddListener(ShowGameOverScreen);
        }

        private void OnDestroy()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnGameOver.RemoveListener(ShowGameOverScreen);
        }

        // ---------- GAMEPLAY ----------
        private void ShowGameOverScreen()
        {
            if (panel != null) panel.SetActive(true);
        }
    }
}
