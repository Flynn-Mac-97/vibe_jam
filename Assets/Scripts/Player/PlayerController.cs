// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): CharacterController input pipeline, gravity accumulation, sprite flip, animator speed.
// Extend (GAMEPLAY): add jump, dash, or ability logic below the GAMEPLAY marker.
// Requires: CharacterController on the same GameObject. Optional: Animator with "Speed" (float).
// Movement: WASD / Arrow keys — X = strafe, Z = depth (2.5D). Gravity applied on Y.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // ---------- INFRA ----------
        [SerializeField] private GameConfig config;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private CharacterController _cc;
        private float _gravityVelocity;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 input = InputMap.GetMoveAxis();
            float speed = config != null ? config.MoveSpeed : 5f;

            // Accumulate gravity; reset when grounded
            if (_cc.isGrounded && _gravityVelocity < 0f)
                _gravityVelocity = -2f; // small constant keeps isGrounded reliable
            _gravityVelocity += Physics.gravity.y * Time.deltaTime;

            // Remap 2D input axes to XZ world movement
            Vector3 move = new Vector3(input.x, _gravityVelocity, input.y);
            move.x *= speed;
            move.z *= speed;

            _cc.Move(move * Time.deltaTime);

            // Sprite facing
            if (spriteRenderer != null && Mathf.Abs(input.x) > 0.01f)
                spriteRenderer.flipX = input.x > 0f;

            // Animator
            if (animator != null)
            {
                float flatSpeed = new Vector2(input.x, input.y).magnitude;
                animator.SetFloat("Speed", flatSpeed);
            }
        }

        // ---------- GAMEPLAY ----------
        // Add jump, dash, or ability logic here.
    }
}
