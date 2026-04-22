// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): Rigidbody2D access, InputMap usage, state-gated movement, controlMode switch.
// Extend (GAMEPLAY): add playbook-specific abilities below the GAMEPLAY marker (dash, double jump, etc.).
// Keep this controller minimal. No wall-jump / coyote time / buffered input unless the playbook explicitly requires it.
// Requires: Rigidbody2D on the same GameObject. Optional: Animator with "Speed" (float) and "Grounded" (bool).
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Player
{
    public enum ControlMode
    {
        SideScroller,
        TopDown,
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour
    {
        // ---------- INFRA ----------
        [SerializeField] private ControlMode controlMode = ControlMode.SideScroller;
        [SerializeField] private GameConfig config;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.15f;
        [SerializeField] private LayerMask groundLayer = ~0;

        private Rigidbody2D rb;
        private bool isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (controlMode == ControlMode.TopDown) rb.gravityScale = 0f;
        }

        private void Update()
        {
            if (!IsPlaying()) return;
            UpdateGrounded();
            if (controlMode == ControlMode.SideScroller && isGrounded && InputMap.GetJumpDown())
            {
                rb.velocity = new Vector2(rb.velocity.x, GetJumpForce());
            }
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            if (!IsPlaying()) return;
            var move = InputMap.GetMoveAxis();
            var speed = GetMoveSpeed();
            if (controlMode == ControlMode.TopDown)
            {
                rb.velocity = move * speed;
            }
            else
            {
                rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
            }
        }

        private bool IsPlaying()
        {
            return GameStateMachine.Instance == null
                || GameStateMachine.Instance.State == GameState.Playing;
        }

        private void UpdateGrounded()
        {
            if (groundCheck == null)
            {
                isGrounded = true;
                return;
            }
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
        }

        private void UpdateAnimator()
        {
            if (animator == null) return;
            var v = rb.velocity;
            animator.SetFloat("Speed", Mathf.Abs(v.x) + Mathf.Abs(v.y));
            animator.SetBool("Grounded", isGrounded);
        }

        private float GetMoveSpeed() => config != null ? config.MoveSpeed : 5f;
        private float GetJumpForce() => config != null ? config.JumpForce : 10f;

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        // ---------- GAMEPLAY ----------
        // Add playbook-specific abilities here.
    }
}
