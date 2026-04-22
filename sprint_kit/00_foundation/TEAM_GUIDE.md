# Team Guide — What This Kit Has for You

One section per role. Find yours, skim it, know where to look on Sunday.

## Flynn (programming, scene owner)

**Your kit sections**:
- `01_unity_templates/Scripts/` — 11 script skeletons you can copy into `Assets/Scripts/<Feature>/`.
- `01_unity_templates/README_how_to_import.md` — target paths for each script.
- `01_unity_templates/UI/` — 4 prefab specs describing the UI panels you'll build in Unity.
- `01_unity_templates/Audio/MIXER_SPEC.md` — how to create the AudioMixer.

**You own on Sunday**:
- The scene `Assets/Scenes/VibeJam Game.unity`. Only you edit it directly. Others hand you prefabs.
- Final build and packaging.

**You do not own**:
- Gameplay design (廖琦 drives the playbook choice and the must-do / won't-do list).
- Music and SFX authoring (trixie).
- Art style (mengping's night sky is the anchor).

**Read before Sunday**:
- `00_foundation/PROJECT_CONVENTIONS.md` — all the rules.
- `02_process/RUNDOWN_SUNDAY.md` — time plan and gates.
- `02_process/CUT_ORDER.md` — what to cut when we're behind.

---

## David (programming)

**Your kit sections**:
- Same as Flynn: `01_unity_templates/Scripts/` and `01_unity_templates/UI/`.
- On Sunday, pair with Flynn on the core loop. Flynn owns the scene; you own gameplay scripts, enemy behaviors, spawn patterns, UI wiring.

**Recommended first pull-ins on Sunday**:
1. `Core/GameStateMachine.cs` → `Assets/Scripts/Core/`
2. `Core/InputMap.cs` → `Assets/Scripts/Core/`
3. `Player/PlayerController2D.cs` → `Assets/Scripts/Player/`
4. Whichever Spawner / Score / Audio / UI pieces the chosen playbook needs.

**Read before Sunday**: same as Flynn.

---

## 廖琦 / Liao (design & gameplay, 5th person on Sunday, convergence driver)

**Your kit sections**:
- `04_design/` — 3 playbooks (A dodge&collect, B rhythm catch, C risk-reward) and 4 design templates (theme convergence, MVP definition, scope, core-loop). All bilingual.
- `05_prompts/` — 8 AI prompt templates for Sunday. Copy, fill, paste.
- `02_process/PLAYTEST_CHECKLIST.md` — your sheet during playtests.

**You own on Sunday**:
- The 15-minute theme convergence. Protocol in `04_design/THEME_CONVERGENCE.en.md`.
- "3 must-do / 3 won't-do" commitment (nobody overrides this once committed).
- Running playtests #1 and #2, filling the checklist, deciding cuts per `CUT_ORDER.md`.
- Keeping the scope board current.

**You do not own**:
- Unity implementation.
- Music / SFX authoring.
- Art assets.

**Read before Sunday**: all of `04_design/` end to end, at least twice. Rehearse convergence with a fake theme on Saturday.

---

## mengping (art)

**Your kit sections**:
- `03_assets_stub/PLACEHOLDER_NAMING.md` — naming rule for any placeholder sprites you add.
- `01_unity_templates/UI/PREFAB_SPEC_*.md` — four UI prefab specs list where art is needed (backgrounds, icons, button states, title text style).

**You own**:
- `Assets/Character/` (already committed — 25-frame walk cycle).
- Any art that comes up Sunday. Style anchor is the existing night sky (moon, stars, Light2D glow).

**You do not own**:
- Scene composition (Flynn).
- UI layout / wiring (David/Flynn build panels against your art).

**Read before Sunday**:
- `03_assets_stub/PLACEHOLDER_NAMING.md` (2 min).
- `HOW_THIS_FITS_EXISTING_WORK.md` — confirms your work is preserved and how it integrates.

---

## trixie (music & sound)

**Your kit sections**:
- `03_assets_stub/AUDIO_CONVENTION.md` — file naming, format, loudness target.
- `01_unity_templates/Audio/MIXER_SPEC.md` — mixer groups (Master / Music / SFX_UI / SFX_Gameplay) and exposed volume parameters.
- `01_unity_templates/Audio/AUDIO_INTEGRATION.md` — the list of `SfxKey` / `BgmKey` values the programmers will trigger; deliver one audio file per key, named to match.

**You own**:
- BGM (short loop, ~30–60 s, set up to loop cleanly).
- SFX: Jump, Pickup, Hit, Fail, UIClick, UIBack, Win, Tick (plus anything added Sunday for the chosen playbook).

**You do not own**:
- Audio triggering in code (programmers do it via `AudioService.Play`).

**Read before Sunday**:
- `03_assets_stub/AUDIO_CONVENTION.md` (5 min).
- `01_unity_templates/Audio/AUDIO_INTEGRATION.md` — this is the contract. Match these names on your files.

---

## Everyone, Saturday night

- Walk through `02_process/RUNDOWN_SUNDAY.md` together (15 min).
- Run the convergence rehearsal (30 min): 廖琦 picks a fake theme, team executes the 15-minute protocol.
- Confirm the packing list (`02_process/SUNDAY_PACKING_LIST.md`).
- Go to sleep early.
