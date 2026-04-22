// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): the Start() order — state machine resolves to Menu at launch.
// Extend (GAMEPLAY): if new systems need an explicit init pass, add them under GAMEPLAY region, never before state resolution.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;

namespace VibeJam.Core
{
    public class SceneBootstrap : MonoBehaviour
    {
        // ---------- INFRA ----------
        private void Start()
        {
            var sm = GameStateMachine.Instance;
            if (sm != null) sm.ReturnToMenu();
        }

        // ---------- GAMEPLAY ----------
        // Add gameplay-specific one-shot setup here (e.g., pre-warm pools).
    }
}
