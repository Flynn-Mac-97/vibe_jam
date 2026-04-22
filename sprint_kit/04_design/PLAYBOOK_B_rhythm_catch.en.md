# Playbook B · Rhythm Catch

Medium reliability. Best when trixie has authored a strong loop and the theme invites tempo.

## 1. One-sentence definition

Beats from the BGM trigger falling objects on-rhythm; the player presses a button or positions a catcher at the right moment to catch them for score, with failure tightening the tempo.

## 2. Three-tier core loop

- **10 s**: tester hears BGM; objects start dropping in time with the beat; tester figures out the catch timing.
- **60 s**: the tester is catching most beats; occasional off-beat trap objects teach the contrast.
- **3 min**: tempo gradually doubles; tester's best combo is their trophy.

## 3. Success & failure conditions

- **Success**: score-driven + longest combo stat.
- **Failure**: 3 consecutive misses, or one "trap" catch → Game Over.

## 4. Kit templates used (checklist)

- [x] `GameStateMachine.cs`
- [x] `InputMap.cs`
- [x] `SceneBootstrap.cs`
- [x] `GameConfig.cs` (add `beatBpm`, `toleranceMs` at top of GAMEPLAY region)
- [x] `PlayerController2D.cs` (TopDown, restricted to horizontal)
- [x] `Spawner.cs` (rhythm-driven, see prompt)
- [x] `ScoreManager.cs` (add combo field in GAMEPLAY region)
- [x] `AudioService.cs` (Tick, Pickup, Hit, Fail, BgmMain)
- [x] `UIBinder.cs` (add comboText)
- [x] `DebugToggle.cs`

## 5. Decision density source

- "Do I risk moving for the combo, or stay where I am for the guaranteed catch?"
- "That object is off-beat — is it a trap?"

## 6. Replay motivation source

- Combo as bragging right — long combos feel earned, losing one hurts.
- The music itself is rewarding; each run is a performance.

## 7. 3 must-do / 3 won't-do

**Must do**
- On-beat objects are visually distinct from trap objects.
- Combo counter is large and visible.
- Catch feedback is crisp — both SFX and visual on the same frame.

**Won't do**
- Full rhythm-game lane UI (Guitar Hero style). Too much scope.
- Charts authored per-beat by hand. Use procedural on-beat spawn instead.
- Difficulty branches (easy/hard). One curve only.

## 8. Theme-mapping examples

| Theme | Mapping |
|---|---|
| Symbiosis | Player catches for a companion; missing hurts both |
| Loop | Each loop of BGM changes spawn pattern |
| Fragment | Catch assembled fragments build a visual progress bar |
| Light & Shadow | Catches illuminate the scene; misses darken |
| Duality | Two lanes; left = music beat, right = off-beat; reward for distinguishing |
| Memory | A short pattern plays, then player has to repeat the catches |
| Time | Fast beats slow time briefly on a perfect catch |

## 9. AI prompt starter

```
You are helping build a Unity 2D Game Jam demo in a URP project.
Rules: Unity 2022.3.62f1, legacy Input (no Input System), TextMeshPro, namespace VibeJam, [SerializeField] private fields.

Given these existing kit scripts: GameStateMachine, InputMap, GameConfig, PlayerController2D (TopDown), Spawner, ScoreManager (with combo), AudioService, UIBinder.

Implement Playbook B · Rhythm Catch:
- A "BeatConductor" MonoBehaviour under VibeJam.Gameplay that, while State == Playing, fires a C# event OnBeat every (60f / config.BeatBpm) seconds. Also plays AudioService.Play(SfxKey.Tick) on each beat.
- A "BeatSpawner" that subscribes to BeatConductor.OnBeat and spawns either a catch object (90%) or trap (10%) from Spawner's prefab list.
- A "Catcher" MonoBehaviour on the player: trigger collision with catch object adds score and increments combo; trigger with trap or missing (object reaches bottom) triggers Hit/Fail per rules.

Produce BeatConductor.cs first (one file per turn, include AI EXTENSION HINTS).
```
