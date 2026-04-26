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
        private PlayerAbilityController _abilityController;

        // ---------- Public ability API ----------
        public bool IsGrounded => _cc.isGrounded;
        public void SetVerticalVelocity(float v) => _gravityVelocity = v;
        public void AddVerticalVelocity(float delta) => _gravityVelocity += delta;

        /// <summary>Fire the "Jump" trigger on the Animator (called by JumpAbility at jump moment).</summary>
        public void TriggerJump()
        {
            if (animator != null)
                animator.SetTrigger("Jump");
        }

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
            _abilityController = GetComponent<PlayerAbilityController>();
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

            // Ability update (may modify _gravityVelocity via SetVerticalVelocity / AddVerticalVelocity)
            _abilityController?.ProcessAbility();

            // Remap 2D input axes to X world movement only — Z axis locked (no depth movement)
            Vector3 move = new Vector3(input.x, _gravityVelocity, 0f);
            move.x *= speed;

            _cc.Move(move * Time.deltaTime);

            // Sprite facing
            if (spriteRenderer != null && Mathf.Abs(input.x) > 0.01f)
                spriteRenderer.flipX = input.x < 0f;

            // Animator
            if (animator != null)
            {
                float flatSpeed = Mathf.Abs(input.x);
                animator.SetFloat("Speed", flatSpeed);
            }
        }

        // ---------- GAMEPLAY ----------
        // Add jump, dash, or ability logic here.
    }
}
