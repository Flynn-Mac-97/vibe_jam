# p08 · Split a Big Task Into AI-Sized Slices

Use when: someone describes a feature that obviously can't be done in one AI turn (or 90 minutes). Use AI to split before starting.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D. Team of 5 on an 8-hour game jam. Rule: one AI-generated slice per turn, ≤ 2 files, ≤ 120 lines each.

Feature description:
[FREE-FORM — describe the feature in plain English. Include gameplay intent, not implementation. Example: "On GameOver, the player sees a slow-motion replay of the last 3 seconds with a red overlay, then the GameOver screen fades in over 0.8s."]

Kit scripts available: GameStateMachine, InputMap, SceneBootstrap, GameConfig, PlayerController2D, Spawner, ScoreManager, AudioService, UIBinder.

Constraints:
- We do not add Unity packages.
- We do not edit the scene file directly (scene-level work is a human-only task).
- Each slice must compile and not crash on its own, even if incomplete (feature flags are fine).

Task:
1. Split the feature into 1–N slices, each doable in ONE AI turn with ≤ 2 new or modified files.
2. For each slice, list:
   - Slice name
   - File(s) to create or modify
   - What it does
   - Dependencies on previous slices
   - How to verify it manually
3. Order slices so the earliest slice produces a visible (even partial) effect.
4. If a slice requires scene work, mark it `[SCENE]` and describe what a human needs to do.

Output: a numbered list of slices. Do NOT write any code yet. I will return and ask for one slice at a time using p07's shape.
```

## Why this exists

A big feature asked in one AI turn produces a monolith that often doesn't compile or uses patterns you don't want. Splitting first gives a plan; the plan itself is cheap; then each slice is a clean, verifiable step.

## Anti-example

Do NOT paste this:
```
"Write the entire slow-motion replay + GameOver fade-in"
```

Do paste this (after p08 gave you the plan):
```
"Execute slice 1 only: [name]. Per p08's plan."
```
