# p01 · Extend Spawner

Use when: the default `Spawner.cs` interval-random pattern isn't enough for the playbook — you need wave spawning, position-based, or rhythm-based.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D, legacy Input. Namespace VibeJam, [SerializeField] private conventions.

Current file: Assets/Scripts/Spawning/Spawner.cs (kit template). It has an interval-random coroutine that picks a random prefab from prefabs[] and instantiates inside an areaSize box. It subscribes to GameStateMachine state changes.

Task: extend Spawner by adding a new SpawnPattern enum with options [Random, WaveAcrossTop, BurstOnDifficultyThreshold, RhythmOnBeat]. Current behavior should map to SpawnPattern.Random.

For the new pattern [PATTERN NAME]:
- Trigger: [when does it spawn — every N seconds, on event, on threshold]
- Placement: [where — top row, along a line, at specific points]
- Count: [how many per trigger]
- Constraints: [anything it must NOT do — don't spawn at player position, don't overlap existing]

Constraints:
- Keep existing Random behavior intact.
- All new logic goes in the GAMEPLAY region of Spawner.cs, below existing INFRA region.
- Include AI EXTENSION HINTS comment at top (it's already there — preserve it).
- Do not add new Unity packages.

Output: only the modified Spawner.cs. No explanation. Keep under 200 lines.
```

## Notes

- Pattern name examples: "wave across top", "burst every 10 s", "spawn on beat from BeatConductor".
- Fill [PATTERN NAME] and the bracketed specifics before pasting.
