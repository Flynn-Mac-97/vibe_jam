# Sunday Rundown

The 8-hour plan. Times are relative to the theme announcement (`T+0`). If announcement is 09:00, T+0 = 09:00 and lock is at T+8h = 17:00. Adjust offsets to the actual clock.

## Timeline

| Time  | Duration | Block | Driver | Output |
|---|---|---|---|---|
| T+0:00 | 15 min | **Theme convergence** (`04_design/THEME_CONVERGENCE.en.md`) | 廖琦 | Chosen playbook A/B/C, 1-sentence definition, 3 must-do / 3 won't-do |
| T+0:15 | 45 min | **Kit import & scene setup** | Flynn | Kit scripts copied into `Assets/Scripts/`, `_Systems` GameObject created with core components, `GameConfig` asset created |
| T+1:00 | 90 min | **Core loop implementation** | David + Flynn (pair) | Player moves; something spawns; collision gives score or ends the run |
| T+2:30 | 15 min | Break / gather | — | Bodies and brains reset |
| T+2:45 | 15 min | **Playtest #1** | 廖琦 leads, all watch | Filled `PLAYTEST_CHECKLIST.md`, 3 top issues identified |
| T+3:00 | 60 min | Fix top-3 issues from playtest #1 | Flynn/David | Issues closed |
| T+4:00 | 45 min | Lunch + mengping polish pass on one hero visual | — | Any one art upgrade that reinforces the chosen playbook |
| T+4:45 | 60 min | **Juice & audio pass** | David + trixie | Jump/pickup/hit/fail SFX in, BGM looping, at least one screen shake or particle pop on a key event |
| T+5:45 | 45 min | **Difficulty tuning** | 廖琦 + Flynn | `GameConfig` curve adjusted so a new player reaches Game Over in 45–90 s |
| T+6:30 | 15 min | **Playtest #2** | 廖琦 leads | 2nd checklist filled, decide cuts per `CUT_ORDER.md` |
| T+6:45 | 45 min | Final cuts + stability fixes (no new features) | Flynn | All Cuttable items from cut list resolved |
| T+7:30 | 15 min | **Lock**: no more code changes | Flynn | Final build per `BUILD_NAMING.md` |
| T+7:45 | 15 min | Record demo video + screenshots | 廖琦 | `demo.mp4`, 3 screenshots |

## Gates (must pass to proceed)

- **Playtest #1 gate (T+2:45)**: there is a playable loop. If there isn't, stop and cut features until there is one — do not add more features.
- **Lock gate (T+7:30)**: the project runs, has a failure state, shows a score, and can be restarted. If any of these four are missing, this is a P0 and stays P0 until fixed.

## Scene owner rule

- `Assets/Scenes/VibeJam Game.unity` has exactly one editor on Sunday: **Flynn**.
- Everyone else modifies prefabs in `Assets/Prefabs/`, hands the prefab to Flynn, Flynn drops it into the scene.
- No exceptions. Two people editing the same `.unity` file creates unmergeable YAML conflicts.

## Branching

- Work on `sunday/<name>-<topic>` branches.
- Merge to `main` at each gate (after Playtest #1, after juice pass, after Lock).
- No `git push --force` to `main`, ever.

## AI collaboration rules (during the sprint)

- One slice per AI turn. Verify the slice compiles and runs before the next slice.
- Max 2 files per AI turn unless you explicitly ask for more.
- Every AI-generated MonoBehaviour must include the `AI EXTENSION HINTS` block.
- Use prompts from `05_prompts/` as the starting point; do not freehand prompts on Sunday.
- If AI output introduces a new Unity package or tries to edit `VibeJam Game.unity`, reject and ask for a different approach.

## Communication rules

- Voice is faster than text when everyone is in the room. Use it for anything blocking.
- Slack/Discord only for things with file links and attachments.
- Announce every state change clearly: "I'm merging to main now", "Pause for playtest in 10", "I'm blocked on X".
