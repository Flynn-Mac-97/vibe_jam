// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): this ScriptableObject is the single source of tuning values. MonoBehaviours read from it.
// Extend (GAMEPLAY): add new serialized fields + public getters below. Never hardcode tuning in MonoBehaviours.
// Create asset: Assets → Create → VibeJam → GameConfig.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using UnityEngine;

namespace VibeJam.Core
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "VibeJam/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        // ---------- INFRA / Player ----------
        [Header("Player")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;

        // ---------- INFRA / Spawning ----------
        [Header("Spawning")]
        [SerializeField] private float spawnIntervalSeconds = 1.5f;
        [SerializeField] private float spawnIntervalMin = 0.4f;

        // ---------- INFRA / Scoring ----------
        [Header("Scoring")]
        [SerializeField] private int basePickupScore = 10;

        // ---------- INFRA / Difficulty ----------
        [Header("Difficulty curve: x = seconds since loop start, y = spawn rate multiplier")]
        [SerializeField] private AnimationCurve difficultyCurve = AnimationCurve.Linear(0f, 1f, 60f, 2f);

        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public float SpawnIntervalSeconds => spawnIntervalSeconds;
        public float SpawnIntervalMin => spawnIntervalMin;
        public int BasePickupScore => basePickupScore;
        public float DifficultyAt(float timeSeconds) => difficultyCurve.Evaluate(timeSeconds);

        // ---------- GAMEPLAY ----------
        // Add playbook-specific knobs here (e.g., jumpBuffer, dashCooldown).
    }
}
