# Scope Board · Must / Should / Could / Won't

Living document. 廖琦 updates it at every gate (after each playtest, at Lock). Pin on a wall or a shared screen. Everyone reads it before picking up a task.

## Board

```
┌───────────────────────┬───────────────────────┐
│ MUST                  │ SHOULD                │
│ (ship or demo fails)  │ (ship if time permits)│
│                       │                       │
│ - …                   │ - …                   │
│ - …                   │ - …                   │
│                       │                       │
├───────────────────────┼───────────────────────┤
│ COULD                 │ WON'T                 │
│ (nice to have)        │ (explicitly excluded) │
│                       │                       │
│ - …                   │ - …                   │
│ - …                   │ - …                   │
└───────────────────────┴───────────────────────┘
```

## Definitions

- **MUST**: if any MUST item is missing at Lock, the demo is broken. Core loop, failure state, score, restart, one hero visual, one hero audio.
- **SHOULD**: strong second-tier features that polish the MUST into a real demo — juice, hint text, key feedback sounds, settings. Ship when MUST is done.
- **COULD**: optional enhancements considered only after SHOULD is complete. Rare in an 8-hour jam.
- **WON'T**: explicitly not in scope. Writing these down is as important as the MUST list — it protects the team from silent scope creep.

## Rules

- Moving an item from SHOULD to MUST requires 廖琦's approval and a swap (move one MUST down to SHOULD).
- Adding a new item to the board requires a swap or a deletion. Nothing gets added silently.
- WON'T items do not come back. If someone raises "what about…", point to the WON'T box.
- Review the board at every gate. Items can move down (MUST → SHOULD → COULD) if playtest shows they don't carry weight.

## Example (Playbook A · theme "Loop")

```
MUST
 - Player moves with arrows/WASD (TopDown)
 - Stars spawn from top, fall, add score on collect
 - Hazards spawn from top, fall, GameOver on collision
 - GameOver screen with score, best, restart
 - Pickup SFX + Hit/Fail SFX
 - BGM loop (trixie)

SHOULD
 - Screen shake on Hit
 - "Loop counter" UI showing which loop (theme tie-in)
 - Difficulty curve steepens per loop
 - Hero star visual (brighter than placeholder)

COULD
 - Gold star worth 5x with short lifespan
 - Near-miss slow-motion
 - Title fade-in

WON'T
 - Multiple difficulty modes
 - Power-ups
 - Second gameplay scene
 - Pause functionality (P key quit is enough)
 - Player health bar
```
