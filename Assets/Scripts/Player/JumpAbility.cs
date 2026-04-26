// AI EXTENSION HINTS
// Stable (INFRA): OnActivate / OnDeactivate state toggle, GetKeyDown guard.
// Extend (GAMEPLAY): tune jumpForce, add double-jump counter, add coyote time.
// Requires: PlayerController on the same GameObject.
// This script follows VibeJam conventions — see agents/AGENTS.md.

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Player
{
    /// <summary>
    /// Grants the player a jump when active.
    /// Activated by entering a zone with AbilityType.Jump.
    /// Press Jump (Space / W / Up) to jump.
    /// </summary>
    public class JumpAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private float jumpForce = 8f;

        private bool _isActive;

        public AbilityType AbilityType => AbilityType.Jump;

        public void OnActivate()   => _isActive = true;
        public void OnDeactivate() => _isActive = false;

        public void Execute(PlayerController player)
        {
            if (!_isActive) return;
            if (InputMap.GetJumpDown() && player.IsGrounded)
                player.SetVerticalVelocity(jumpForce);
        }
    }
}
