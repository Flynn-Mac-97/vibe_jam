# How `sprint_kit/` Fits with Existing Work

This document is the boundary contract between the pre-production kit and the work Flynn and mengping already committed. Read it before editing anything under `Assets/` or `ProjectSettings/`.

## Why a separate `sprint_kit/` folder

1. It is outside `Assets/`, so Unity does not index it and will not generate `.meta` files for its contents.
2. It never overwrites a teammate's file. Teammates pull what they want from here into their own area.
3. Everything pre-production is in one place — easy to review, easy to delete after the jam if we want to keep the repo clean.

## Keep as-is (do not move, rename, or overwrite)

| Path | Owner | Why |
|---|---|---|
| `agents/AGENTS.md` | Flynn | Authoritative coding / Unity conventions. The kit defers to it. |
| `Assets/Scenes/VibeJam Game.unity` | Flynn | The main scene and its night-sky mood. Sunday edits are additive only. |
| `Assets/Sprites/circle.png`, `ground.png`, `star.png`, `white_square.png` | Flynn | Placeholders already in use in the main scene. |
| `Assets/Materials/Unlit2D.mat` | Flynn | Referenced by scene objects. |
| `Assets/Settings/` (URP configs, scene template) | Flynn | URP 2D pipeline is configured and working. |
| `Assets/Character/Walking_loop_animation_202604212250/` | mengping | 25 frames + `walk_0421.anim` + Animator Controller. Will be wired into the scene on Sunday once gameplay is known. |
| `Assets/test_0421.unity` | mengping | Her sandbox. Leave it. |
| `ProjectSettings/*` | Flynn | Project-wide Unity configuration. |
| `Packages/manifest.json`, `Packages/packages-lock.json` | Flynn | Do not add packages (especially not Input System). |
| `.gitignore`, `README.md` | Flynn | In use. |

## How Unity templates in the kit become Sunday production code

The scripts in `01_unity_templates/Scripts/` are **not compiled** — Unity does not see them. When someone wants to use a template:

1. Read `01_unity_templates/README_how_to_import.md` for the target path (e.g., `PlayerController2D.cs` → `Assets/Scripts/Player/PlayerController2D.cs`).
2. Copy the file into that path. Unity compiles it automatically.
3. The kit copy stays as reference; you can modify the `Assets/` copy freely without touching the kit.

This two-copy pattern is intentional: teammates can diverge the production copy from the kit without polluting the kit.

## Conflict rules

- Scene file `VibeJam Game.unity` has a **single owner on Sunday** (Flynn). Others create prefabs in `Assets/Prefabs/` and hand them to Flynn to drop in. This prevents `.unity` YAML merge conflicts.
- If the kit says "English only" and `AGENTS.md` says something compatible, the kit phrasing is fine. If they ever conflict, `AGENTS.md` wins — update the kit to match.
- If a teammate wants to add a template to the kit, put it in the right subfolder and mention it in the next team sync. Do not delete or rewrite existing kit files unilaterally.

## Post-jam cleanup (optional, out of scope for now)

After the jam, the team can either (a) delete `sprint_kit/` entirely, (b) keep it as a reference for future jams, or (c) promote the parts that were actually used into proper project docs. Decision deferred to after the jam.
