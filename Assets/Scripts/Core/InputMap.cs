// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): all methods below wrap legacy UnityEngine.Input so gameplay never calls Input directly.
// Extend (GAMEPLAY): add new named actions (e.g., GetDashDown) as static methods — never call Input.* from gameplay code.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;

namespace VibeJam.Core
{
    public static class InputMap
    {
        public static Vector2 GetMoveAxis()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        public static bool GetJumpDown()
        {
            return Input.GetKeyDown(KeyCode.Space)
                || Input.GetKeyDown(KeyCode.W)
                || Input.GetKeyDown(KeyCode.UpArrow);
        }

        public static bool GetJumpHeld()
        {
            return Input.GetKey(KeyCode.Space)
                || Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.UpArrow);
        }

        public static bool GetActionDown()
        {
            return Input.GetKeyDown(KeyCode.E)
                || Input.GetKeyDown(KeyCode.Return)
                || Input.GetMouseButtonDown(0);
        }

        public static bool GetPauseDown()
        {
            return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P);
        }

        public static bool GetRestartDown()
        {
            return Input.GetKeyDown(KeyCode.R);
        }
    }
}
