// AI EXTENSION HINTS
// Stable (INFRA): OnActivate / OnDeactivate state toggle, held-key fast-fall.
// Extend (GAMEPLAY): tune fallAcceleration, add ground-slam on landing, add screen shake.
// Requires: PlayerController on the same GameObject.
// This script follows VibeJam conventions — see agents/AGENTS.md.

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Player
{
    /// <summary>
    /// Grants the player a fast-fall when active.
    /// Activated by entering a zone with AbilityType.Fall.
    /// Hold Jump (Space / W / Up) to accelerate downward.
    /// </summary>
    public class FallAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private float fallAcceleration = 25f;

        private bool _isActive;

        public AbilityType AbilityType => AbilityType.Fall;

        public void OnActivate()   => _isActive = true;
        public void OnDeactivate() => _isActive = false;

        public void Execute(PlayerController player)
        {
            if (!_isActive) return;
            if (InputMap.GetJumpHeld() && !player.IsGrounded)
                player.AddVerticalVelocity(-fallAcceleration * Time.deltaTime);
        }
    }
}
