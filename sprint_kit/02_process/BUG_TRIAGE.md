# Bug Triage

Four levels. Decision is triage, not debate. If a bug arrives and can't be classified in 30 seconds, default to P1 and revisit at the next gate.

## P0 — Blocker. Fix NOW, drop everything else.

Criteria (any one triggers P0):
- Project fails to launch or fails to enter Playing state.
- Core loop is unreachable from a fresh run.
- Crash within the first 60 seconds of play.
- Scene reference broken (Missing Script, null prefab) on a Cuttable:No path.
- Corrupted scene YAML that blocks a merge.

Action: whoever is free stops and helps. Flynn arbitrates. Do not add features while a P0 is open.

## P1 — Harms the experience but the build is playable.

Criteria:
- Player can get stuck in a way that requires restart.
- A primary mechanic only works ~50% of the time.
- Missing SFX or visual feedback on the core interaction.
- GameOver screen shows wrong or stale score.
- Score persistence broken.

Action: add to the fix list; address during the next fix window (after Playtest #1) or the juice pass.

## P2 — Minor, noticeable but not loop-breaking.

Criteria:
- Off-center layout at common resolution.
- Minor visual jitter or animation cut.
- One non-critical button unwired.
- Edge-case input combination misbehaves.

Action: fix only if P0/P1 are clear and there is time before Lock.

## P3 — Polish / nice-to-have.

Criteria:
- Typography imperfection.
- Particle timing slightly off.
- Any wish starting with "it would be cool if…"

Action: do not fix during the jam. File in a post-jam list.

## Reporting shape

```
[P0/P1/P2/P3] Short title
Repro:  1) … 2) … 3) …
Expected:  …
Actual:  …
First seen: commit hash or playtest #
```

## When to escalate P1 → P0

- Same bug appears in two consecutive playtests.
- Bug prevents the demo from being recorded.

## When to de-escalate P0 → P1

- Workaround found and documented, loop now reachable.
- Bug only manifests in rare input sequences unlikely in a 3-minute demo.
