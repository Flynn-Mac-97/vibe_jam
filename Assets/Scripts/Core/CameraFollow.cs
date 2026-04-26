// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): Y-only lerp follow, fixed X/Z captured at Awake.
// Extend (GAMEPLAY): add X-axis follow, screen-shake via AddShake(), look-ahead offset.
// Requires: Transform target assigned in Inspector (the Player).
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;

namespace VibeJam.Core
{
    /// <summary>
    /// Smooth Y-axis camera follow. X and Z stay fixed at their startup values.
    /// Attach to the Main Camera. Assign the Player transform as target.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        // ---------- INFRA ----------
        [Tooltip("The transform to follow (Player).")]
        [SerializeField] private Transform target;

        [Tooltip("Vertical offset above the target.")]
        [SerializeField] private float yOffset = 8f;

        [Tooltip("Lower = snappier, higher = floatier.")]
        [SerializeField] private float smoothSpeed = 5f;

        [Tooltip("Clamp the camera so it never goes below this world Y (prevents seeing under the floor).")]
        [SerializeField] private float yMin = -5f;

        [Tooltip("Clamp the camera so it never goes above this world Y.")]
        [SerializeField] private float yMax = 120f;

        private float _fixedX;
        private float _fixedZ;

        private void Awake()
        {
            _fixedX = transform.position.x;
            _fixedZ = transform.position.z;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            // ---------- GAMEPLAY ----------
            float desiredY = target.position.y + yOffset;
            float clampedY = Mathf.Clamp(desiredY, yMin, yMax);
            float smoothedY = Mathf.Lerp(transform.position.y, clampedY, smoothSpeed * Time.deltaTime);

            transform.position = new Vector3(_fixedX, smoothedY, _fixedZ);
        }
    }
}
