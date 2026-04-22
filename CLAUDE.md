# CLAUDE.md — Vibe Jam Project Entry Point

This file orients any AI assistant (Claude Code, Cursor, etc.) entering this repository. Humans: read `README.md` and `sprint_kit/README.md` instead.

## Project

- **Event**: AI Game Vibe Jam. Sunday, 8-hour on-site development.
- **Engine**: Unity 2022.3.62f1 (locked, do not upgrade). URP 2D renderer. 1920×1080. Legacy `Input` (no Input System package).
- **Team**: Flynn, David (programming) · 廖琦 / Liao (design & gameplay) · mengping (art) · trixie (audio).
- **Phase at time of writing (2026-04-22)**: pre-production. Theme and gameplay are intentionally undecided until the organizers announce Sunday morning.

## Authoritative documents (read in this order)

1. **`agents/AGENTS.md`** — coding conventions, Unity setup, MCP info. Authored by Flynn. Treat as source of truth for every technical detail. Do not contradict.
2. **`sprint_kit/README.md`** — the pre-production kit. All new pre-production content lives under `sprint_kit/`.
3. This file — AI-operational notes only (what follows).

If anything below conflicts with `AGENTS.md`, `AGENTS.md` wins.

## Language policy (strict)

- All filenames, folder names, code, comments: **English only**.
- Team-facing docs (READMEs, templates, checklists, specs, prompts): **English only** so Flynn (non-Chinese reader) and David can read everything.
- Design & gameplay documents authored by 廖琦 (playbooks, theme convergence, MVP definition, scope, core-loop templates): **bilingual** — `<name>.en.md` and `<name>.zh.md`, same content, kept in sync.
- Never introduce Chinese filenames anywhere.

## Do not touch (existing team contributions)

- `agents/AGENTS.md`
- `Assets/Scenes/VibeJam Game.unity` — Flynn's night-sky scene. Sunday edits must be additive (new GameObjects) only. Do not remove or rename existing GameObjects.
- `Assets/Character/` — mengping's 25-frame walking animation + `walk_0421.anim` + Animator Controller.
- `Assets/Sprites/circle.png`, `ground.png`, `star.png`, `white_square.png` — Flynn's placeholders. Reuse, do not replace.
- `Assets/Settings/` — URP configuration.
- `Assets/test_0421.unity` — mengping's sandbox.
- `ProjectSettings/`, `Packages/manifest.json`, `.gitignore`, `README.md`.

## Where new content goes

- Pre-production artifacts (docs, script templates, specs, prompts) → `sprint_kit/`.
- Unity templates in `sprint_kit/01_unity_templates/` are **reference copies** — teammates opt in and copy into `Assets/Scripts/<Feature>/` themselves.
- Sunday production code (once theme and gameplay are chosen) → `Assets/Scripts/<FeatureName>/`, following `AGENTS.md` conventions (namespace `VibeJam`, `[SerializeField] private`, coroutines over async/await).

## AI working rules (for Sunday sprint)

- One slice per turn. Verify the user has pulled and tested before the next slice.
- Never touch more than 2 files in a single turn unless explicitly asked.
- Never introduce new dependencies or Unity packages.
- Never edit `Assets/Scenes/VibeJam Game.unity` directly — ask the human scene owner (Flynn) to apply scene changes.
- When generating a new MonoBehaviour, include the `AI EXTENSION HINTS` comment block at the top (see any script in `sprint_kit/01_unity_templates/Scripts/` for the format).
- Chinese replies are fine in chat; file contents stay English (except bilingual design docs).

## Auto-behavior: when the user drops an asset or describes a new thing

When the user says things like "I made a jump sound", "here's a meteor sprite", "look at this code I wrote" — WITHOUT explicit instructions — do this automatically:

1. Identify category: audio / sprite / animation / script / design / bug.
2. Consult the matching kit file(s):
   - Audio → `sprint_kit/03_assets_stub/AUDIO_CONVENTION.md` + `sprint_kit/01_unity_templates/Audio/AUDIO_INTEGRATION.md`
   - Sprite → `sprint_kit/03_assets_stub/PLACEHOLDER_NAMING.md`
   - Script → `agents/AGENTS.md` + templates under `sprint_kit/01_unity_templates/Scripts/`
   - UI → `sprint_kit/01_unity_templates/UI/*.md`
   - Design / playbook / scope → `sprint_kit/04_design/`
   - Bug → `sprint_kit/02_process/BUG_TRIAGE.md` + `sprint_kit/05_prompts/p06_fix_bug.md`
3. Reply in exactly three sections:
   - **Rename to:** correct filename
   - **Put in:** full repo path
   - **Wire up:** step-by-step for the programmer (component, field, Unity import settings)
4. Prefer the simplest option when multiple exist.
5. If intent is ambiguous (e.g., a sound without knowing which gameplay action), ask ONE clarifying question first.

## Out of scope for this project

- Unity Input System migration.
- DI frameworks, event-bus libraries, general-purpose config systems beyond the single `GameConfig` ScriptableObject.
- CI, automated tests, Git LFS, localization framework, save system.
- Inventing a new visual style — Flynn's night sky (moon / stars / Light2D / particle glow) is the anchor.

## The pre-production plan

The complete plan lives at `/Users/liaoqi/.claude/plans/rosy-noodling-lynx.md` (廖琦's local plan store, not committed). The plan is executed in 4 batches of kit files, all landing under `sprint_kit/`.
