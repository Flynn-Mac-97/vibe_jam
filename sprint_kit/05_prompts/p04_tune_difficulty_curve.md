# p04 · Tune Difficulty Curve

Use when: playtest says too easy (player lives > 2 min) or too hard (player dies < 15 s).

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D. GameConfig ScriptableObject has:
- float spawnIntervalSeconds (base cadence)
- float spawnIntervalMin (floor)
- AnimationCurve difficultyCurve (x = seconds since loop start, y = rate multiplier)

Current spawner logic: wait = max(spawnIntervalMin, spawnIntervalSeconds / difficultyCurve.Evaluate(elapsed))

Playtest observation: [WHAT HAPPENED — e.g., "tester died at 12 s, felt unfair"; "tester lived 3 min without losing focus"]

Target: tester reaches Game Over in 45–90 seconds on a typical first run. Difficulty should feel fair at 10 s, notable at 30 s, tense at 60 s, frantic at 90 s.

Task: propose 3 concrete changes to GameConfig numbers (spawnIntervalSeconds, spawnIntervalMin, difficultyCurve keys). For each:
- Before value
- After value
- Reasoning in 1 line
- What to watch for on the next playtest

Constraints:
- Change only GameConfig values. Do not change code.
- Keep the curve monotonic non-decreasing.
- Keep spawnIntervalMin ≥ 0.1 so the spawner can't busy-loop.

Output: a 3-row table. No code. No long explanation.
```

## Tip

- Run p04 twice. First run: use the observation to get a suggestion. Second run (after applying and re-testing): use new observation to fine-tune.
