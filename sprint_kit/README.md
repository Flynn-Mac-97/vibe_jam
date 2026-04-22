# sprint_kit

Pre-production kit for the Vibe Jam 8-hour sprint. Everything in here is theme-agnostic and gameplay-agnostic. On Sunday, after the theme is announced, the team pulls what it needs from this kit into the Unity project and starts work on the core gameplay loop — no one should be building infrastructure on Sunday.

## Who should read what

- **Everyone**, first: `00_foundation/TEAM_GUIDE.md` — find your own section, see what the kit has for you.
- **Programmers (Flynn, David)**: `01_unity_templates/` — 11 script templates, 4 UI prefab specs, audio mixer spec, font notes. All are reference copies; you import what you want when you want.
- **Designer (廖琦)**: `04_design/` — playbooks and templates (bilingual). `05_prompts/` — ready-to-paste AI prompts for Sunday.
- **Artist (mengping)**: `03_assets_stub/PLACEHOLDER_NAMING.md`, `01_unity_templates/UI/` (prefab specs describe UI hooks that need art).
- **Audio (trixie)**: `03_assets_stub/AUDIO_CONVENTION.md`, `01_unity_templates/Audio/` (mixer spec + integration contract).
- **Everyone**, Saturday night: `02_process/RUNDOWN_SUNDAY.md`, `02_process/SUNDAY_PACKING_LIST.md`, `02_process/PLAYTEST_CHECKLIST.md`.

## Structure

```
sprint_kit/
├── README.md                          this file
├── HOW_THIS_FITS_EXISTING_WORK.md     boundary with Flynn's and mengping's existing work
├── 00_foundation/                     conventions and per-role guide
├── 01_unity_templates/                script skeletons, UI specs, audio specs — reference copies
├── 02_process/                        task template, rundown, checklists, bug triage, build naming, cut order
├── 03_assets_stub/                    placeholder naming + audio naming
├── 04_design/                         playbooks and design templates (bilingual .en.md / .zh.md)
└── 05_prompts/                        AI prompt templates for Sunday
```

## Rules

- English-only for filenames, code, and team-facing docs. Design docs in `04_design/` are bilingual.
- `01_unity_templates/` files are **not auto-imported** into `Assets/`. A teammate decides when and whether to copy each one into `Assets/Scripts/<Feature>/` (paths are given in `01_unity_templates/README_how_to_import.md`).
- Do not touch anything listed in `HOW_THIS_FITS_EXISTING_WORK.md` as "keep as-is".
- If you find a gap in the kit, add a file and list it in `02_process/DOCS_INDEX.md` (if that file doesn't exist yet, just add the file and mention it at the next sync).

## How pre-production moves forward

廖琦 fills this kit in 4 batches (foundation → unity templates → process docs → design playbooks + AI prompts). Each batch is committed once complete. Pace is self-directed; there is no fixed schedule. The kit is "ready" when all four batches are in and the Saturday-night convergence rehearsal passes.
