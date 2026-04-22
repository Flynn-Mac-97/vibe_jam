# AI Prompts

Eight ready-to-paste prompts for Sunday. Pick the one that matches your task, fill the bracketed placeholders, paste.

## How to use

1. Open the file (e.g., `p01_extend_spawner.md`).
2. Copy the prompt block.
3. Replace placeholders in `[BRACKETS]` with specifics.
4. Paste into Claude Code / Cursor / your AI editor.
5. Verify the AI's output compiles and runs before asking for the next slice.

## Available

| # | File | Use when |
|---|---|---|
| 01 | `p01_extend_spawner.md` | You need a custom spawn pattern (wave, burst, position-based) |
| 02 | `p02_add_enemy_behavior.md` | You need a new enemy / hazard / pickup with behavior |
| 03 | `p03_add_ui_animation.md` | UI feels static; needs tween-in, pulse, shake |
| 04 | `p04_tune_difficulty_curve.md` | Playtest says too easy / too hard |
| 05 | `p05_add_juice.md` | Screen shake, particles, hit stop, sound layer on a key event |
| 06 | `p06_fix_bug.md` | A specific bug needs AI help to diagnose + fix |
| 07 | `p07_playbook_to_prototype.md` | Just finished convergence; need AI to spin up the minimum prototype |
| 08 | `p08_sliced_implementation.md` | Task is too big for one AI turn; need help splitting |

## Rules

- One prompt → one turn → verify → next prompt. Do not queue prompts.
- If a prompt produces bad output, rewrite the placeholders with more specifics rather than re-running the same prompt.
- All prompts assume the kit scripts are imported (see `01_unity_templates/README_how_to_import.md`).
- Prompts are in English; AI responds more reliably in English.
