// AI EXTENSION HINTS
// Stable (INFRA): mirror position tracking from player transform.
// Extend (GAMEPLAY): add fade in/out based on distance to zone border, add ghost trail effect.
// Requires: a SpriteRenderer on this GameObject (or a child) and the Player to be tagged "Player".
// This script follows VibeJam conventions — see agents/AGENTS.md.

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Player
{
    /// <summary>
    /// Visual indicator that shadows the player's position in the OPPOSITE zone.
    /// Place one MirrorIndicator inside each AbilityZone.
    /// Each indicator reflects the player across the X axis relative to both zone centers,
    /// showing the player where they would land if they phase.
    /// </summary>
    public class MirrorIndicator : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Tooltip("Zone this indicator lives in (the destination zone for the player).")]
        [SerializeField] private AbilityZone thisZone;

        [Tooltip("The zone the player is currently in (the source zone to mirror from).")]
        [SerializeField] private AbilityZone sourceZone;

        [SerializeField] private float yOffset = 0f;  // vertical nudge if needed

        private Transform _player;
        private SpriteRenderer _sr;
        private SpriteRenderer _playerSr;

        private void Awake()
        {
            _sr = GetComponentInChildren<SpriteRenderer>();
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _player = playerObj.transform;
                _playerSr = playerObj.GetComponent<SpriteRenderer>();
            }
        }

        private void LateUpdate()
        {
            if (_player == null || thisZone == null || sourceZone == null) return;

            // Reflect player's offset from the source zone center onto this zone center
            Vector3 offsetInSource = _player.position - sourceZone.transform.position;
            offsetInSource.x = -offsetInSource.x;

            Vector3 mirroredPos = thisZone.transform.position + offsetInSource;
            mirroredPos.y += yOffset;
            transform.position = mirroredPos;

            // Mirror the player's current animation frame and flip
            if (_sr != null && _playerSr != null)
            {
                _sr.sprite = _playerSr.sprite;
                _sr.flipX  = !_playerSr.flipX; // inverted because it's mirrored
            }
        }
    }
}
