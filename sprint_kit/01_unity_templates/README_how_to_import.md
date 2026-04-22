# How to Import Unity Templates

These files are reference copies. Unity does not compile them here. When you want to use one, copy it into the target path inside `Assets/`.

## Scripts → target paths

| Source | Target |
|---|---|
| `Scripts/Core/GameStateMachine.cs` | `Assets/Scripts/Core/GameStateMachine.cs` |
| `Scripts/Core/InputMap.cs` | `Assets/Scripts/Core/InputMap.cs` |
| `Scripts/Core/SceneBootstrap.cs` | `Assets/Scripts/Core/SceneBootstrap.cs` |
| `Scripts/Core/DebugToggle.cs` | `Assets/Scripts/Core/DebugToggle.cs` |
| `Scripts/Core/GameConfig.cs` | `Assets/Scripts/Core/GameConfig.cs` |
| `Scripts/Player/PlayerController2D.cs` | `Assets/Scripts/Player/PlayerController2D.cs` |
| `Scripts/Spawning/Spawner.cs` | `Assets/Scripts/Spawning/Spawner.cs` |
| `Scripts/Score/ScoreManager.cs` | `Assets/Scripts/Score/ScoreManager.cs` |
| `Scripts/Audio/AudioService.cs` | `Assets/Scripts/Audio/AudioService.cs` |
| `Scripts/Audio/SfxKey.cs` | `Assets/Scripts/Audio/SfxKey.cs` |
| `Scripts/UI/UIBinder.cs` | `Assets/Scripts/UI/UIBinder.cs` |

## Wiring in the scene (minimum setup)

1. Create an empty GameObject `_Systems` at scene root.
2. Add components (on `_Systems`): `GameStateMachine`, `ScoreManager`, `AudioService`, `SceneBootstrap`, `DebugToggle` (optional).
3. Create `GameConfig` asset via `Assets → Create → VibeJam → GameConfig`. Assign it to `PlayerController2D` and `Spawner`.
4. Create 4 UI prefabs per `UI/PREFAB_SPEC_*.md`, drop them under a Canvas, then add `UIBinder` on the Canvas and wire the panel / text references.
5. Create `MainMixer.mixer` per `Audio/MIXER_SPEC.md`. Assign its groups to `AudioService`.

## Rules

- Do not modify the kit copies once imported. Edit the `Assets/` copy instead.
- Follow `VibeJam` namespace.
- If a script fails to compile after import, check that `using` statements match your project's namespace (should be `VibeJam.Core`, `VibeJam.Player`, etc.).
- Do not introduce new Unity packages. The scripts rely on: `UnityEngine`, `UnityEngine.UI`, `UnityEngine.Audio`, `TMPro`.
