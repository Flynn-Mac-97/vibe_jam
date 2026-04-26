// AI EXTENSION HINTS
// - Add OnTriggerExit to reset to a default color when the player leaves all zones
// - Use a singleton ZoneManager to handle overlapping triggers gracefully
// - Drive additional effects (fog density, music, post-processing) from zoneColor

using UnityEngine;

namespace VibeJam
{
    /// <summary>
    /// Attach to a trigger volume (BoxCollider with isTrigger = true).
    /// When the Player enters, sets the Main Camera background to zoneColor.
    /// Requires the Main Camera clearFlags to be set to "Solid Color".
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ZoneTrigger : MonoBehaviour
    {
        [SerializeField] private Color zoneColor = Color.white;

        private void Awake()
        {
            // Ensure the collider on this object is a trigger
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (Camera.main != null)
            {
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
                Camera.main.backgroundColor = zoneColor;
            }
        }
    }
}
