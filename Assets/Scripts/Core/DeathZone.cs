// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): trigger detection, player respawn to spawnPoint.
// Extend (GAMEPLAY): add death VFX spawn, audio trigger, lives counter decrement.
// Requires: BoxCollider (isTrigger = true) on this GameObject. Player must be tagged "Player".
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using System.Collections;
using UnityEngine;

namespace VibeJam.Core
{
    /// <summary>
    /// Kills (respawns) the player when they enter this trigger volume.
    /// Attach to any GameObject with a trigger Collider.
    /// Assign spawnPoint in the Inspector, or leave null to use the player's starting position.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class DeathZone : MonoBehaviour
    {
        // ---------- INFRA ----------
        [SerializeField] private Transform spawnPoint;

        [Tooltip("Brief freeze before respawn so the hit registers visually.")]
        [SerializeField] private float respawnDelay = 0.3f;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StartCoroutine(RespawnCoroutine(other.gameObject));
        }

        // ---------- GAMEPLAY ----------
        private IEnumerator RespawnCoroutine(GameObject player)
        {
            // Disable controller so physics doesn't fight the teleport
            var cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            yield return new WaitForSeconds(respawnDelay);

            // Deduct a life — if this triggers GameOver, skip the respawn
            LivesManager.Instance?.TakeDamage();

            if (GameStateMachine.Instance != null &&
                GameStateMachine.Instance.State == GameState.GameOver)
            {
                if (cc != null) cc.enabled = true;
                yield break;
            }

            Vector3 destination = spawnPoint != null
                ? spawnPoint.position
                : Vector3.zero;

            player.transform.position = destination;

            yield return null; // one frame for physics to settle

            if (cc != null) cc.enabled = true;
        }
    }
}
