# p06 · Fix Bug

Use when: a specific bug is reproducible and you want AI to diagnose + fix. Feed structured info, don't let the AI guess.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D, legacy Input. Namespace VibeJam.

Bug report:
- Severity: [P0 / P1 / P2 / P3]
- Symptom: [ONE sentence — "Player falls through ground when jumping near the screen edge."]
- Repro steps:
  1. [first action]
  2. [second action]
  3. [third action]
- Expected: [what should happen]
- Actual: [what happens]
- First seen: [commit hash or "during playtest #1"]

Relevant files (paths, not content):
- Assets/Scripts/Player/PlayerController2D.cs
- Scene: Assets/Scenes/VibeJam Game.unity (don't edit)
- [any other]

Console errors (if any):
[paste exact messages, or write "none"]

Suspected cause (if any):
[optional — "probably groundCheck overlap missing the ground layer at edges"]

Task:
1. Diagnose — 3 most likely root causes, ranked by probability.
2. Ask me clarifying questions if needed BEFORE writing code. Do not guess.
3. If you have enough info, propose a fix in the highest-probability file only. One file per turn.

Constraints:
- Do not modify the scene file.
- Do not introduce new Unity packages.
- If the fix requires scene-level changes, describe them in comments at the top of your proposed file — Flynn will apply scene edits by hand.
```

## Notes

- Never ask AI to "fix" without a repro. Guess fixes often create worse bugs.
- If AI asks a clarifying question, answer it. That's a good sign.
