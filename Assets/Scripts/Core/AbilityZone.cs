// AI EXTENSION HINTS
// Stable (INFRA): OnTriggerEnter/Exit dispatch, camera color, mirror zone reference.
// Extend (GAMEPLAY): add zone-specific music, post-processing overrides, particle effects.
// Requires: Collider on same GameObject with isTrigger = true. Player must be tagged "Player".
// This script follows VibeJam conventions — see agents/AGENTS.md.

using UnityEngine;
using VibeJam.Player;

namespace VibeJam.Core
{
    /// <summary>
    /// Trigger zone that:
    ///   1. Sets the camera background color when the Player enters.
    ///   2. Activates the configured ability type on the PlayerAbilityController.
    ///   3. Holds a reference to the mirrored zone for phase teleportation.
    ///
    /// Replace the ZoneTrigger component with this one.
    /// Collider on this GameObject must have isTrigger = true.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class AbilityZone : MonoBehaviour
    {
        // ---------- INFRA ----------
        [SerializeField] private Color zoneColor = Color.white;
        [SerializeField] private AbilityType abilityType = AbilityType.None;

        [Tooltip("The zone on the opposite side — used for phase teleportation and the mirror indicator.")]
        [SerializeField] private AbilityZone mirrorZone;

        public AbilityType AbilityType  => abilityType;
        public AbilityZone MirrorZone   => mirrorZone;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // Camera color
            if (Camera.main != null)
            {
                Camera.main.clearFlags    = CameraClearFlags.SolidColor;
                Camera.main.backgroundColor = zoneColor;
            }

            // Ability switch
            var abilityCtrl = other.GetComponent<PlayerAbilityController>();
            abilityCtrl?.EnterZone(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var abilityCtrl = other.GetComponent<PlayerAbilityController>();
            abilityCtrl?.ExitZone(this);
        }
    }
}
