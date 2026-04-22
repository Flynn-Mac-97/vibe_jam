# Playbook A · Dodge & Collect

Reliability: highest. This is the safe choice when the team is tired or the theme is vague.

## 1. One-sentence definition

The player moves a character through a night-sky space, collecting falling stars for points while dodging falling hazards that end the run.

## 2. Three-tier core loop

- **10 s**: tester learns WASD / arrow keys move the character and that touching stars adds score.
- **60 s**: hazards start falling alongside stars; tester starts making micro-decisions about which pickups are worth the risk.
- **3 min**: difficulty curve pushes spawn rate up; tester is trying to beat their best score, takes increasingly risky grabs.

## 3. Success & failure conditions

- **Success**: there is no "win" — this is score-driven. "Success" = beating your previous best on the GameOver screen.
- **Failure**: one collision with a hazard → Game Over.

## 4. Kit templates used (checklist)

- [x] `GameStateMachine.cs`
- [x] `InputMap.cs`
- [x] `SceneBootstrap.cs`
- [x] `GameConfig.cs` (moveSpeed, spawnInterval, difficultyCurve)
- [x] `PlayerController2D.cs` (TopDown mode; or SideScroller if gravity fits the theme)
- [x] `Spawner.cs` (one for stars, one for hazards, two instances in scene)
- [x] `ScoreManager.cs`
- [x] `AudioService.cs` (Pickup, Hit, Fail, Tick, BgmMain)
- [x] `UIBinder.cs` + all four UI prefabs
- [x] `DebugToggle.cs`

## 5. Decision density source

- "Do I chase the gold star that's drifting near the hazard?"
- "Do I stay safe in the center and take fewer but lower-value pickups?"

## 6. Replay motivation source

- Personal best chasing (ScoreManager persists via PlayerPrefs).
- Near-miss tension — the hazard graze produces a thrill.

## 7. 3 must-do / 3 won't-do

**Must do**
- Immediate audio-visual feedback on every pickup and every hit.
- A visible "Best: N" next to current score during play.
- Short GameOver (≤ 2 s) → Restart available instantly.

**Won't do**
- Multiple lives / health bar. One hit = death. Clean and readable.
- Powerups, weapon upgrades, combo multipliers (Sunday: only if #2 playtest is strong and time remains).
- Multiple hazard types (only one, scaling up in quantity, not variety).

## 8. Theme-mapping examples

| Theme | Mapping |
|---|---|
| Symbiosis | Stars heal you back after N hits (still one-hit logic but with small grace pool) |
| Loop | Fallen stars respawn at top after a delay — loop continues |
| Fragment | Hazards are "shards"; stars are "pieces to reassemble" |
| Light & Shadow | Safe zones lit by moon; hazards come from shadows |
| Duality | Two characters move in mirror; player controls one, other mirrors |
| Memory | Score trails the player briefly as ghost stars |
| Small things matter | Tiny pickups worth more than large ones |

## 9. AI prompt starter

```
You are helping build a Unity 2D Game Jam demo in a URP project.
Rules: Unity 2022.3.62f1, legacy Input (no Input System), TextMeshPro, namespace VibeJam, [SerializeField] private fields.

Given these existing kit scripts in Assets/Scripts/: GameStateMachine, InputMap, SceneBootstrap, GameConfig (ScriptableObject), PlayerController2D (ControlMode.TopDown), Spawner, ScoreManager, AudioService, UIBinder.

Implement Playbook A · Dodge & Collect:
- A "PickupStar" prefab with a trigger Collider2D that on OnTriggerEnter2D(Collider2D) calls ScoreManager.Instance.Add(config.BasePickupScore), plays AudioService.Play(SfxKey.Pickup), and Destroys itself.
- A "HazardMeteor" prefab with a trigger Collider2D that calls AudioService.Play(SfxKey.Hit), then AudioService.Play(SfxKey.Fail), then GameStateMachine.Instance.EndGame(), and Destroys itself.
- Two Spawner instances in the scene: one for stars at top, one for hazards at top, both spawning downward via their prefab's Rigidbody2D gravity.
- Player is tagged "Player"; prefabs check for that tag on collision.

Produce the two prefab-attachable scripts (Pickup.cs under VibeJam.Gameplay, Hazard.cs under VibeJam.Gameplay). One file per turn. Include AI EXTENSION HINTS blocks.
```
