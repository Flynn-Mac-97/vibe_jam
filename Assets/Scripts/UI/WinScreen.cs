// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): show/hide panel on win, restart hook.
// Extend (GAMEPLAY): add completion time display, star rating, next-level button.
// Requires: GameStateMachine in scene. TMP package.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.UI
{
    /// <summary>
    /// Shows the win overlay when GameStateMachine fires OnWin.
    /// Attach to the Canvas (or a child). Assign panel in Inspector — it will be hidden at start.
    /// </summary>
    public class WinScreen : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Tooltip("Root GameObject of the win overlay. Will be hidden at Start and shown on win.")]
        [SerializeField] private GameObject panel;

        private void Start()
        {
            if (panel != null) panel.SetActive(false);

            // Subscribe to win event
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnWin.AddListener(ShowWinScreen);
        }

        private void OnDestroy()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnWin.RemoveListener(ShowWinScreen);
        }

        // ---------- GAMEPLAY ----------
        private void ShowWinScreen()
        {
            if (panel != null) panel.SetActive(true);
        }
    }
}
