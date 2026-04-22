// ========================================
// AI EXTENSION HINTS
// Stable (INFRA): the state subscription + coroutine loop + area gizmo.
// Extend (GAMEPLAY): add spawn patterns (wave, burst, pattern-based) as new coroutines below GAMEPLAY marker,
// selected by a serialized enum so playbooks switch via inspector.
// This script follows VibeJam conventions — see agents/AGENTS.md.
// ========================================

using System.Collections;
using UnityEngine;
using VibeJam.Core;

namespace VibeJam.Spawning
{
    public class Spawner : MonoBehaviour
    {
        // ---------- INFRA ----------
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private Vector2 areaSize = new Vector2(10f, 1f);
        [SerializeField] private GameConfig config;
        [SerializeField] private bool spawnInWorldSpace = true;

        private Coroutine loop;
        private float elapsed;

        private void OnEnable()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnStateChanged += HandleStateChanged;
        }

        private void OnDisable()
        {
            if (GameStateMachine.Instance != null)
                GameStateMachine.Instance.OnStateChanged -= HandleStateChanged;
            StopSpawning();
        }

        private void Start()
        {
            if (GameStateMachine.Instance != null && GameStateMachine.Instance.State == GameState.Playing)
                StartSpawning();
        }

        private void HandleStateChanged(GameState prev, GameState next)
        {
            if (next == GameState.Playing)
            {
                if (prev == GameState.Menu) elapsed = 0f;
                StartSpawning();
            }
            else
            {
                StopSpawning();
            }
        }

        public void StartSpawning()
        {
            if (loop != null) return;
            loop = StartCoroutine(SpawnLoop());
        }

        public void StopSpawning()
        {
            if (loop != null)
            {
                StopCoroutine(loop);
                loop = null;
            }
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                float baseInterval = config != null ? config.SpawnIntervalSeconds : 1.5f;
                float minInterval  = config != null ? config.SpawnIntervalMin      : 0.4f;
                float difficulty   = config != null ? config.DifficultyAt(elapsed)  : 1f;
                float wait = Mathf.Max(minInterval, baseInterval / Mathf.Max(difficulty, 0.01f));
                yield return new WaitForSeconds(wait);
                elapsed += wait;
                SpawnOne();
            }
        }

        private void SpawnOne()
        {
            if (prefabs == null || prefabs.Length == 0) return;
            var prefab = prefabs[Random.Range(0, prefabs.Length)];
            if (prefab == null) return;
            var offset = new Vector3(
                Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f),
                Random.Range(-areaSize.y * 0.5f, areaSize.y * 0.5f),
                0f);
            var pos = transform.position + offset;
            var parent = spawnInWorldSpace ? null : transform;
            Instantiate(prefab, pos, Quaternion.identity, parent);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0.01f));
        }

        // ---------- GAMEPLAY ----------
        // Add custom spawn patterns here.
    }
}
