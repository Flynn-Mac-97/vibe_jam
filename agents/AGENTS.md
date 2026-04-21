# AGENTS.md — Vibe Jam Unity Project Reference

Quick-start reference for agents and developers working in this codebase.

---

## Project Identity

| Field | Value |
|---|---|
| **Project Name** | Vibe Jam |
| **Unity Version** | 2022.3.62f1 (last version available in China — do not upgrade) |
| **Render Pipeline** | Universal Render Pipeline (URP) 14.0.12 — **Universal (3D Forward) renderer** |
| **Default Resolution** | 1920 × 1080 |
| **Color Space** | Linear |
| **Company** | DefaultCompany |

---

## Project Folder Map

```
vibe_jam/
├── agents/                         # Agent and developer reference docs (this file)
├── Assets/
│   ├── Scenes/                     # Unity scene files (.unity)
│   │   └── VibeJam Game.unity      # Main (and currently only) scene — entry point
│   ├── Scripts/                    # All C# game scripts (currently empty — greenfield)
│   └── Settings/                   # URP render pipeline configuration assets
│       ├── UniversalRenderer.asset # ACTIVE: URP 3D Forward renderer (lighting, shadows, post-FX)
│       ├── Renderer2D.asset        # INACTIVE: original 2D renderer — do not re-enable
│       ├── UniversalRP.asset       # URP pipeline asset — points to UniversalRenderer.asset
│       └── Lit2DSceneTemplate      # Scene template (legacy 2D — not used for new scenes)
├── Packages/
│   └── manifest.json               # Package dependencies — edit this to add/remove packages
├── ProjectSettings/                # Unity project config (YAML) — version controlled
│   └── ProjectSettings.asset       # Core settings: company, product name, resolution, etc.
├── Library/                        # Unity-generated build cache — DO NOT version control
└── Temp/                           # Unity temp files — DO NOT version control
```

> Every asset file has a paired `.meta` file. **Never delete `.meta` files** — they contain GUIDs Unity uses to track asset references. Deleting one breaks all scene/prefab links to that asset.

---

## Key Packages

| Package | Version | Purpose |
|---|---|---|
| `com.unity.feature.2d` | 2.0.1 | 2D toolset bundle (Sprites, Tilemaps, Pixel Perfect, 2D Animation, etc.) |
| `com.unity.render-pipelines.universal` | 14.0.12 | Universal Render Pipeline (URP) |
| `com.unity.textmeshpro` | 3.0.7 | Text rendering — use `TMP_Text` everywhere, not legacy `Text` |
| `com.unity.timeline` | 1.7.7 | Cutscene / sequenced animation |
| `com.unity.ugui` | 1.0.0 | Unity UI (Canvas, Image, Button, etc.) |
| `com.unity.visualscripting` | 1.9.4 | Visual scripting (Bolt) — available but not primary workflow |
| `com.unity.collab-proxy` | 2.7.1 | Unity Version Control (Plastic SCM) integration |

---

## Current Scenes

| Scene | Path | Notes |
|---|---|---|
| VibeJam Game | `Assets/Scenes/VibeJam Game.unity` | Only scene — all gameplay goes here for now |

---

## Unity MonoBehaviour Lifecycle (Execution Order)

```
Awake()          → called once on instantiation, before any Start()
OnEnable()       → called every time the object is enabled
Start()          → called once on first frame, after all Awake()s
  FixedUpdate()    → physics timestep (default 0.02s) — use for Rigidbody / CharacterController physics
Update()         → every frame — use for input, movement, logic
LateUpdate()     → after all Updates — use for camera follow
OnDisable()      → object disabled or destroyed
OnDestroy()      → final cleanup
```

---

## C# Scripting Conventions

- **Serialized fields**: use `[SerializeField] private` — never public fields for inspector exposure.
- **Namespace**: not enforced yet, but recommended as `VibeJam` when adding scripts.
- **ScriptableObjects**: use for shared config/data (stats, audio clips, item definitions) — avoids coupling.
- **Coroutines**: use for time-based async logic (`StartCoroutine`) — prefer over `async/await` for game logic.
- **No Assembly Definitions yet** — all scripts compile into the default Unity assembly. Consider adding `.asmdef` files when the project grows.
- **Prefabs**: place in `Assets/Prefabs/<FeatureName>/` (folder to be created when first prefab is added).
- **Script organisation**: group by feature inside `Assets/Scripts/`, e.g.:
  ```
  Assets/Scripts/
  ├── Player/
  ├── Enemy/
  ├── UI/
  └── Core/
  ```

---

## Rendering & Scene Configuration (2.5D)

This project uses a **2.5D visual style**: 3D physics and lighting, but character visuals are 2D sprites. Do not revert to 2D-only settings.

### Renderer (ACTIVE)
- **Renderer**: `Assets/Settings/UniversalRenderer.asset` — URP 3D Forward renderer. **This is the active renderer.**
- **Pipeline asset**: `Assets/Settings/UniversalRP.asset` — points to `UniversalRenderer.asset`. Do not change the renderer reference.
- `Renderer2D.asset` is **kept but inactive** — do not re-enable it; it breaks 3D lighting.

### Lighting
- Use standard Unity **3D lights**: `Directional Light`, `Point Light`, `Spot Light`.
- **Do NOT use `Light2D`** — it only works with the 2D renderer, which is inactive.
- Scene has one `Directional Light` at rotation (50°, -30°, 0°). Add to it, don't replace it.
- Ambient lighting is set via **Window → Rendering → Lighting → Environment**.

### Camera (Main Camera)
- **Projection: Perspective** — FOV 15°, position (0, 40, -60), rotation ~35° pitch downward.
- This gives the 2.5D top-angled look. Do not switch to Orthographic unless explicitly asked.
- Do not move the camera via script without discussing with the team.

### Materials & Shaders
- Use URP-compatible shaders only. Default for 3D meshes: `Universal Render Pipeline/Lit`.
- Sprites (SpriteRenderer) use `Universal Render Pipeline/2D/Sprite-Unlit-Default` by default — they render flat, which is intentional for the 2.5D art style.
- **Do not assign `Sprite-Lit-Default`** — it requires the 2D renderer and will appear black.

### Physics
- **3D physics**: use `Rigidbody`, `BoxCollider`, `CapsuleCollider`, `CharacterController` etc.
- **Do NOT use `Rigidbody2D` or `Collider2D`** for gameplay objects — the world is 3D.
- Player movement: XZ plane (WASD), gravity on Y. Handled by `CharacterController`.

### Sorting
- Sprite draw order: **Sorting Layers** (`ProjectSettings/TagManager.asset`) + **Order in Layer** on `SpriteRenderer`.
- Z-depth in 3D space also contributes to visual layering with the perspective camera.

---

## Current Project State

- `Assets/Scripts/Core/` — `InputMap.cs`, `GameConfig.cs` (copied from sprint_kit templates).
- `Assets/Scripts/Player/` — `PlayerController.cs` (CharacterController-based, WASD/arrows, XZ movement).
- Scene `VibeJam Game.unity` contains: `Main Camera`, `Directional Light`, `Ground` (3D cube + BoxCollider), `Player` (SpriteRenderer + Animator + CharacterController + PlayerController).
- Character animation: mengping's 25-frame walk cycle wired in Animator with `Speed` float parameter; Idle ↔ Walk transitions at threshold 0.1.
- URP 3D Forward renderer active (`UniversalRenderer.asset`). `Renderer2D.asset` is inactive.
- No `GameConfig` ScriptableObject asset created yet — human must do: right-click `Assets/Scripts/Core` → **Create → VibeJam → GameConfig**.
- No prefabs, no audio yet.
- No Input System package installed — legacy `Input` only (see `InputMap.cs`).

---

## MCP Server

Live Unity Editor access via [CoplayDev/unity-mcp](https://github.com/CoplayDev/unity-mcp). Server runs on `localhost:8080`. Use the `mcp_unitymcp_*` tool family to read and mutate the scene without touching files directly.

### Key MCP tool groups
| Group | What it does |
|---|---|
| `manage_scene` | Hierarchy read, scene load/save |
| `manage_gameobject` | Create / modify / delete GameObjects |
| `manage_components` | Add, remove, set properties on components |
| `manage_animation` | AnimatorController states, transitions, parameters, clips |
| `manage_camera` | Camera config + screenshot capture |
| `manage_asset` | Search and read project assets |
| `execute_code` | Run arbitrary C# inside the Editor (CodeDom, no `using` at top level — use fully qualified names) |
| `read_console` | Unity console output — check after every script change |

---

## Agent Working Rules

1. **Never delete `.meta` files.**
2. Place all new C# scripts in `Assets/Scripts/<FeatureName>/`.
3. Use `TMP_Text` (TextMeshPro) — never the legacy `UnityEngine.UI.Text`.
4. Use URP-compatible shaders and materials only.
5. Do not modify `Assets/Settings/UniversalRenderer.asset` or `UniversalRP.asset` unless explicitly asked — changes affect the entire project's rendering. Never re-enable `Renderer2D.asset` as the active renderer.
6. Do not upgrade the Unity version — 2022.3.62f1 is locked.
7. `Library/` and `Temp/` are generated — never edit files inside them.
8. When creating new scenes, do **not** use `Lit2DSceneTemplate` — it is a legacy 2D template. Create a blank scene and manually add a Camera and Directional Light.
9. **Scene validation — stop and ask the human**: After any implementation step that affects the scene visually, take a screenshot (`take_screenshot` / `mcp_unitymcp_manage_editor`) and inspect it. If the scene does not look correct (wrong layout, missing objects, broken lighting, invisible sprites, unexpected behaviour), **do not attempt to self-correct silently**. Instead:
   - State clearly what looks wrong in the screenshot.
   - List the most likely causes you can identify.
   - Ask the human to open the Unity Editor, inspect the scene hierarchy / Inspector, and report back with what they observe.
   - Wait for the human's response before proceeding with any further changes.
