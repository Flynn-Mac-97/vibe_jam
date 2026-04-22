// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): F1 toggles the debug HUD. FPS + state + score are the default reads.
// Extend (GAMEPLAY): add extra GUI.Label rows inside OnGUI under GAMEPLAY region.
// This script is stripped from Release builds automatically (see #if).
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using VibeJam.Score;

namespace VibeJam.Core
{
    public class DebugToggle : MonoBehaviour
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        // ---------- INFRA ----------
        [SerializeField] private KeyCode toggleKey = KeyCode.F1;
        [SerializeField] private bool showOnStart = true;

        private bool visible;
        private float fps;
        private float fpsTimer;

        private void Start()
        {
            visible = showOnStart;
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey)) visible = !visible;

            fpsTimer -= Time.unscaledDeltaTime;
            if (fpsTimer <= 0f)
            {
                fps = 1f / Mathf.Max(Time.unscaledDeltaTime, 0.0001f);
                fpsTimer = 0.25f;
            }
        }

        private void OnGUI()
        {
            if (!visible) return;
            var style = new GUIStyle(GUI.skin.label) { fontSize = 14, normal = { textColor = Color.white } };
            GUI.Label(new Rect(10, 10, 400, 20), $"FPS: {fps:F0}", style);
            var sm = GameStateMachine.Instance;
            GUI.Label(new Rect(10, 30, 400, 20), $"State: {(sm != null ? sm.State.ToString() : "-")}", style);
            var score = ScoreManager.Instance;
            GUI.Label(new Rect(10, 50, 400, 20), $"Score: {(score != null ? score.Current : 0)}  Best: {(score != null ? score.High : 0)}", style);

            // ---------- GAMEPLAY ----------
            // Append extra debug rows here.
        }
#endif
    }
}
