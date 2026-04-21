# AGENTS.md — Vibe Jam Unity Project Reference

Quick-start reference for agents and developers working in this codebase.

---

## Project Identity

| Field | Value |
|---|---|
| **Project Name** | Vibe Jam |
| **Unity Version** | 2022.3.62f1 (last version available in China — do not upgrade) |
| **Render Pipeline** | Universal Render Pipeline (URP) 14.0.12 — 2D renderer |
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
│       ├── Renderer2D.asset        # 2D renderer config (controls lighting, post-FX)
│       ├── UniversalRP.asset       # URP pipeline asset (quality settings, shadows)
│       └── Lit2DSceneTemplate      # Scene template with lit 2D setup pre-configured
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

To add a package: edit `Packages/manifest.json` directly, or use **Window → Package Manager** inside Unity.

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
FixedUpdate()    → physics timestep (default 0.02s) — use for Rigidbody2D
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

## URP 2D Specifics

- **Renderer**: `Assets/Settings/Renderer2D.asset` — controls 2D lighting, shadow casters, post-processing.
- **Pipeline asset**: `Assets/Settings/UniversalRP.asset` — quality tiers, HDR, MSAA.
- **Lighting**: use `Light2D` component for in-scene lights. The scene template uses the **Lit** render mode.
- **Materials/Shaders**: only use URP-compatible shaders. Default sprite shader: `Universal Render Pipeline/2D/Sprite-Lit-Default`.
- **Sorting**: control draw order via **Sorting Layers** (defined in `ProjectSettings/TagManager.asset`) and **Order in Layer** on `SpriteRenderer`.
- **Physics**: use `Rigidbody2D` and `Collider2D` variants (`BoxCollider2D`, `CircleCollider2D`, etc.) — not 3D physics components.
- **Camera**: use a `Camera` with `Projection: Orthographic` for 2D. Attach `Cinemachine 2D` camera if smooth follow is needed.

---

## Current Project State

- `Assets/Scripts/` is **empty** — this is a greenfield project, no game logic exists yet.
- Only one scene exists: `VibeJam Game.unity`.
- URP 2D renderer and pipeline are configured and ready.
- No prefabs, no audio, no animations yet.
- No input system package installed — if needed, add `com.unity.inputsystem` to `manifest.json`.

---

## MCP Server (CoplayDev/unity-mcp)

The project uses [CoplayDev/unity-mcp](https://github.com/CoplayDev/unity-mcp) (MIT, free) to give AI agents live access to the Unity Editor.

### Why this one
- Supports Unity 2021.3 LTS+ — compatible with 2022.3.62f1
- Explicitly supports VS Code / GitHub Copilot as an MCP client
- 8.7k ⭐, actively maintained (v9.6.6)
- Python-based server (no Node.js required)

### Prerequisites (already installed on this machine)
- `uv` 0.11.7 — `%USERPROFILE%\.local\bin\uv.exe`
- Python 3.12.13 — managed by uv

### Install the Unity package *(one-time, do inside Unity Editor)*
1. Open Unity → **Window > Package Manager**
2. Click **+** → **Add package from git URL...**
3. Paste: `https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main`
4. Click **Add** and wait for import

> **China/GitHub note**: GitHub must be reachable during this step. Use a VPN if needed — only required during initial package install.

### Start the server & connect to VS Code *(each session)*
1. In Unity: **Window > MCP for Unity**
2. Click **Start Server** — launches HTTP server on `localhost:8080`
3. In the same window, select **VS Code Copilot** from the client dropdown
4. Click **Configure** — auto-writes `.vscode/mcp.json`
5. Reload VS Code — look for 🟢 "Connected ✓" in the MCP for Unity window

### Available MCP tools (selection)
| Tool | What it does |
|---|---|
| `get_scene_hierarchy` | List all GameObjects in the active scene |
| `create_gameobject` | Create a new GameObject with components |
| `update_component` | Add/edit component fields on any GameObject |
| `create_script` | Generate a C# script in `Assets/Scripts/` |
| `run_tests` | Run Unity Test Runner tests |
| `get_console_logs` | Read Unity console output |
| `take_screenshot` | Capture the Game View |

### Troubleshooting
- **Server won't start**: ensure Unity Editor is open and not in Play Mode
- **Not connecting**: check `localhost:8080` is not blocked by Windows Firewall
- **Package install fails in China**: enable VPN, retry Package Manager step

---

## Agent Working Rules

1. **Never delete `.meta` files.**
2. Place all new C# scripts in `Assets/Scripts/<FeatureName>/`.
3. Use `TMP_Text` (TextMeshPro) — never the legacy `UnityEngine.UI.Text`.
4. Use URP-compatible shaders and materials only.
5. Do not modify `Assets/Settings/Renderer2D.asset` or `UniversalRP.asset` unless explicitly asked — changes affect the entire project's rendering.
6. Do not upgrade the Unity version — 2022.3.62f1 is locked.
7. `Library/` and `Temp/` are generated — never edit files inside them.
8. When creating new scenes, use `Assets/Settings/Lit2DSceneTemplate` as the template.
