# Project Conventions

One-pager of every convention the team follows during pre-production and Sunday sprint. If `agents/AGENTS.md` covers a topic, this file references it and does not duplicate.

## 1. Language

- English for every filename, folder name, code file, and code comment.
- English for team-facing docs: READMEs, templates, checklists, prefab specs, audio specs, AI prompts.
- Bilingual (English + Chinese, one file each suffixed `.en.md` / `.zh.md`) for 廖琦's design & gameplay docs only: playbooks, theme convergence, MVP definition, scope, core-loop templates.
- Never use Chinese in filenames.

## 2. Unity & engine

See `agents/AGENTS.md` for the full list. Highlights:

- Unity 2022.3.62f1 (locked). Do not upgrade.
- **Visual style: 2.5D.** 2D sprites arranged with z-layering, parallax, and depth cues to read as 2.5D. Implementation stays on Unity's 2D stack (SpriteRenderer, Rigidbody2D, Collider2D, Light2D, orthographic camera by default). Unity C# API names that end in `2D` (Rigidbody2D, Collider2D, Physics2D, Light2D, OnTriggerEnter2D, etc.) are class names — keep them verbatim, never rename to "3D" variants or "2.5D" variants (they don't exist).
- URP 14.0.12 with 2D renderer. 1920×1080. Linear color space. Orthographic camera (switchable to perspective on Sunday if needed for a stronger 2.5D effect).
- Legacy `Input` (`UnityEngine.Input`). Do not add the Input System package.
- TextMeshPro for all text. Do not use the old uGUI `Text`.
- Rigidbody2D / Collider2D for physics. No 3D physics.

## 3. Code structure

See `agents/AGENTS.md` for the authoritative rules. Summary:

- Namespace `VibeJam` (plus sub-namespaces like `VibeJam.Player`, `VibeJam.Audio`).
- Serialized fields: `[SerializeField] private`, never `public` fields.
- Prefer Coroutines over async/await.
- Scripts organized under `Assets/Scripts/<FeatureName>/`. Current feature names used in the kit: `Core`, `Player`, `Spawning`, `Score`, `Audio`, `UI`.
- Gameplay-relevant tuning values live in a single `GameConfig` ScriptableObject. Do not hardcode numbers in MonoBehaviours.
- Every MonoBehaviour template in the kit carries an `AI EXTENSION HINTS` comment block at the top; keep it when you copy the template into the project.

## 4. Scene rules

- The project has exactly one gameplay scene: `Assets/Scenes/VibeJam Game.unity`. Do not add additional gameplay scenes during pre-production. If gameplay absolutely requires a second scene on Sunday, Flynn decides.
- On Sunday the scene has a single owner (Flynn). Other teammates modify prefabs; Flynn drops prefabs into the scene.
- Scene edits on Sunday are additive only — never remove or rename pre-existing GameObjects (Sky, Moon, MoonGlow, Stars, Star2-6, Ground, Character, Main Camera, Global Light 2D, Auto_Default_ParticleSystem_Universal).

## 5. Prefab rules

- New prefabs live in `Assets/Prefabs/`.
- A prefab never references gameplay logic directly — it publishes events and subscribes to them via `UIBinder`, `ScoreManager`, `AudioService`, etc.
- Names use PascalCase: `Player.prefab`, `PickupStar.prefab`, `HazardMeteor.prefab`.

## 6. Audio rules

- All sound plays through `AudioService.Play(SfxKey)` and `AudioService.PlayBgm(BgmKey)`. Do not place `AudioSource` components on random GameObjects.
- Mixer groups: `Master` → `Music`, `SFX_UI`, `SFX_Gameplay`. Each exposes a `_Volume` parameter.
- File naming: see `../03_assets_stub/AUDIO_CONVENTION.md` (after batch 3).

## 7. UI rules

- Canvas: `Scale With Screen Size`, reference resolution `1920×1080`, match `0.5`.
- Text: TMP only. Font asset created from one of the open-source fonts noted in `../01_unity_templates/Fonts/README_font_source.md`.
- UI panels are separate prefabs (`UI_HUD`, `UI_StartMenu`, `UI_GameOver`, `UI_PauseOverlay`). Show/hide is driven by `UIBinder` subscribing to `GameStateMachine` events.
- Buttons call `UIBinder` methods, never game-logic methods directly.

## 8. Input rules

- Keyboard + mouse only for Sunday demo. No gamepad support.
- All input goes through `InputMap.cs`. Do not call `UnityEngine.Input` from gameplay code directly — this makes key rebinding a single-file change.

## 9. Git & branching

- `main` stays stable. Each pushed commit to `main` should at least compile and not crash on Play.
- Sunday sprint work goes on `sunday/<name>-<topic>` branches, e.g., `sunday/flynn-spawner`, `sunday/david-ui`. Merge to `main` at each playtest milestone.
- Commit messages: short imperative, English. "Add Spawner.cs", "Wire UI_GameOver buttons", "Tune difficulty curve v2".
- No `git push --force` on `main` during the jam, ever.

## 10. AI collaboration rules

- One slice per AI turn. Verify the slice compiles and runs before asking for the next slice.
- Do not let AI touch more than 2 files per turn.
- Every AI-generated MonoBehaviour gets the `AI EXTENSION HINTS` comment block. See the header of any template under `01_unity_templates/Scripts/` for the format.
- When prompting an AI, prefer templates from `05_prompts/` — they are already structured to produce usable output.
- AI is told not to add new Unity packages, not to rename existing GameObjects, and not to edit scene YAML directly.

## 11. Out of scope (do not build)

- Unity Input System, Cinemachine (unless Flynn decides on Sunday), Addressables, Localization Package, Save System, DOTween or any tweening library, Git LFS.
- Mobile / touch support. Gamepad support.
- Network / multiplayer.
- Multiple gameplay scenes.
- Difficulty configuration in JSON / CSV. Use `GameConfig` ScriptableObject only.

## 12. Pre-production vs Sunday split

- Pre-production (廖琦, solo, now → Saturday): fill `sprint_kit/`. Do not modify `Assets/` except to add folder-level structure if absolutely needed (and even that can wait for Sunday).
- Saturday night: full team reviews the kit, raises issues, runs a convergence rehearsal using a fake theme.
- Sunday: theme announced → 15-minute convergence → pull templates from kit into `Assets/` → implement core loop → playtest → cut → lock → record.
