// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): register with KeyManager on Awake, collect on player touch, deactivate self.
// Extend (GAMEPLAY): spawn pickup VFX on collect, play SFX, add bob animation.
// Requires: Collider (isTrigger=true) on this GameObject. Player must be tagged "Player".
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;

namespace VibeJam.Core
{
    /// <summary>
    /// Place on a Key GameObject. Registers itself with KeyManager on Awake.
    /// When the Player enters the trigger, notifies KeyManager and hides itself.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class KeyPickup : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Tooltip("Degrees per second the key spins on its Y axis for visibility.")]
        [SerializeField] private float spinSpeed = 90f;

        private bool _collected;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            KeyManager.Instance?.Register();
        }

        private void Update()
        {
            if (_collected) return;
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collected) return;
            if (!other.CompareTag("Player")) return;

            // ---------- GAMEPLAY ----------
            _collected = true;
            KeyManager.Instance?.Collect();
            gameObject.SetActive(false);
        }
    }
}
