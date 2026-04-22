# p07 · Playbook → Minimum Prototype

Use when: theme convergence just finished and you need AI to spin up the smallest playable version of the chosen playbook.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D, legacy Input, TextMeshPro. Namespace VibeJam, [SerializeField] private conventions.

Kit scripts already imported to Assets/Scripts/ (do not rewrite them, only reference):
- Core/GameStateMachine.cs
- Core/InputMap.cs
- Core/SceneBootstrap.cs
- Core/DebugToggle.cs
- Core/GameConfig.cs (ScriptableObject)
- Player/PlayerController2D.cs
- Spawning/Spawner.cs
- Score/ScoreManager.cs
- Audio/AudioService.cs
- Audio/SfxKey.cs
- UI/UIBinder.cs

Chosen Playbook: [paste the entire chosen playbook .en.md here]

Theme mapping decided in convergence:
- One-sentence definition: [paste]
- 3 must-do: [paste]
- 3 won't-do: [paste]

Task: produce the MINIMUM set of gameplay scripts to reach a first-playable build. First-playable means:
- Player can move.
- At least one spawnable object exists (pickup or hazard).
- A collision with that object produces a consequence (score or GameOver).
- GameOver can be reached and Restart works.

Constraints:
- Only NEW scripts under Assets/Scripts/Gameplay/ with namespace VibeJam.Gameplay.
- Max 3 new files. If more are needed, stop and list them.
- Each new file includes AI EXTENSION HINTS block.
- Do not modify existing kit files. Do not modify scene.
- Each file ≤ 120 lines.
- List (in a comment at top of each file or in a separate TODO message) any scene-level work the human needs to do (prefab creation, component wiring, references).

Output: 1 file per turn. Start with the most critical one first. I will verify compilation and come back for the next file.
```

## Workflow with this prompt

1. Paste with filled playbook.
2. AI returns file #1 + a list of scene-level TODOs.
3. You create the file + do the scene work.
4. Run, verify no errors.
5. Return, ask for file #2. AI continues.
6. Repeat until first-playable.

Typical output: 2–3 files (e.g., `Pickup.cs`, `Hazard.cs`, maybe a `PlaybookAController.cs`).
