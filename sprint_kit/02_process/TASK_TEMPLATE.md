# Task Template

Use this exact shape for every Sunday task. 6 fields, no more.

```
### [Task Name]
- Owner:       [single person]
- Estimate:    [e.g., 45 min. Max 90 min. If bigger, split.]
- Dependencies:[list task names, or "none"]
- Done when:   [concrete, testable condition — "player jumps and lands without clipping", not "jumping works"]
- Cuttable:    [Yes / No]   (No = part of the core loop; never cut)
- Notes:       [optional, one line only]
```

## Example

```
### Wire PlayerController2D to scene Character
- Owner:       Flynn
- Estimate:    30 min
- Dependencies:PlayerController2D.cs imported to Assets/Scripts/Player/
- Done when:   Press Play → Character moves left/right with WASD, no console errors
- Cuttable:    No
- Notes:       Assign GameConfig asset to the `config` field
```

## Rules

- One owner per task. Pair programming means one owner holds the commit; the other reviews.
- If you can't write a concrete `Done when`, the task is not ready — refine it first.
- 90 minutes is the hard ceiling. Anything larger must split before it's picked up.
- `Cuttable: No` tasks make up the core loop. Protect them from any scope creep decision.
