# Core Loop Template

Write three timescales of the player's experience. If you can't fill all three with specifics, the loop is not yet a loop.

```
10-second layer (first impression):
  - What the player perceives:
  - What the player tries first:
  - First positive signal (what rewards the try):

60-second layer (entering skill territory):
  - New element introduced:
  - First meaningful decision:
  - First non-trivial failure possibility:

3-minute layer (mastery territory):
  - Escalation (what keeps getting harder):
  - What the player is chasing:
  - What the "almost" moment feels like:
```

## Validation rules

- Every layer must have at least one concrete noun and one concrete verb. "The player feels the tension" is not concrete. "The meteor passes 2 units from the player, score bumps to 45" is concrete.
- The 10-second layer's positive signal must be visible + audible within the first 10 seconds of a first-time play. If it takes 15, it doesn't count.
- The 60-second decision must be a real trade-off, not a correct/wrong answer.
- The 3-minute escalation must come from something already present at 60 s — don't introduce a new system at 3 min.

## Worked example (Playbook A · theme "Loop")

```
10-second layer:
  - Perceives: a character on ground under a moon; stars begin to fall from the top.
  - Tries first: walks left/right (common platformer intuition).
  - First positive signal: touches a star, score jumps from 0 to 10 with a bright pop sfx.

60-second layer:
  - New element: hazards (meteors) now fall among stars.
  - Decision: chase the star drifting toward the right while a hazard approaches from top-right, or stay safe in the middle.
  - Failure: one collision → Game Over; the tester has felt the consequence.

3-minute layer:
  - Escalation: spawn rate ramps (difficultyCurve); the screen fills; reaction time shrinks.
  - Chasing: personal best score + "how long can I survive this loop".
  - Almost moment: grazed a meteor by half a unit, got the star, score jumps visibly — the thrill that drives the next run.
```

## Anti-patterns (rewrite if you see these)

- The 10 s layer contains "the player reads a tutorial" — no tutorial. Intuition or a one-line hint only.
- The 60 s decision is "the player gets better" — that's not a decision, that's growth.
- The 3 min layer introduces a powerup system — scope explosion.
