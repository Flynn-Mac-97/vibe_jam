# Playbook C · Risk-Reward Positioning

Highest ceiling, lowest reliability. Choose only if the team is well-rested and the theme demands strategic movement.

## 1. One-sentence definition

The play space has a safe zone and a scoring zone; time in the scoring zone earns points at an accelerating rate, but the scoring zone is actively hostile — the player must decide when to push in and when to retreat.

## 2. Three-tier core loop

- **10 s**: tester discovers the scoring zone pulses with rewards and the safe zone is calm.
- **60 s**: tester is making brief runs into the scoring zone; each run feels like a decision.
- **3 min**: tester is chasing a long streak — the tension between greed and survival is the whole game.

## 3. Success & failure conditions

- **Success**: score-driven; longest single streak in scoring zone is the secondary trophy.
- **Failure**: taking one hit inside scoring zone → Game Over. Safe zone is always safe.

## 4. Kit templates used (checklist)

- [x] `GameStateMachine.cs`
- [x] `InputMap.cs`
- [x] `SceneBootstrap.cs`
- [x] `GameConfig.cs` (add `baseZoneScorePerSecond`, `zoneStreakMultiplierCurve`)
- [x] `PlayerController2D.cs` (TopDown or SideScroller — playbook works with either)
- [x] `Spawner.cs` (hazards only; they move through or into the scoring zone)
- [x] `ScoreManager.cs` (extend to support continuous score accrual over time)
- [x] `AudioService.cs` (Tick while in zone, Hit, Fail, Win, BgmMain)
- [x] `UIBinder.cs` (add "IN ZONE" indicator)
- [x] `DebugToggle.cs`

## 5. Decision density source

- "How long do I stay in the zone?"
- "The hazard is close — do I push one more second for the multiplier bump?"
- "My streak just hit the threshold — do I bank it and retreat?"

## 6. Replay motivation source

- "Almost made it" tension — the player pushed one second too long.
- Streak multiplier feels earned — the score curve rewards nerve.

## 7. 3 must-do / 3 won't-do

**Must do**
- The scoring zone is visually unmistakable — a clear outlined area, distinct color or brightness.
- A tick or pulse sound while earning (audio reinforcement of the risk).
- Score accrual rate ramps up the longer you stay — the player feels the escalation.

**Won't do**
- Multiple scoring zones with different rules (scope explosion).
- Power-ups that affect zone rules.
- A "safe timer" that forces the player out — the point is that the player decides.

## 8. Theme-mapping examples

| Theme | Mapping |
|---|---|
| Symbiosis | Zone slowly drains a companion resource; you push for yourself at someone's cost |
| Loop | Zone shrinks with each loss; each restart gives you slightly less margin |
| Fragment | Scoring in zone collects fragments on a gauge; fill to level up multiplier |
| Light & Shadow | Scoring zone is dark; safe zone is lit; hazards only appear in dark |
| Duality | Two zones alternating; score only in the "open" one; its which-is-which shifts |
| Greed | Literal — the more you have, the more hazards spawn |

## 9. AI prompt starter

```
You are helping build a Unity 2.5D Game Jam demo (2D sprites with z-layering for depth) in a URP 2D project.
Rules: Unity 2022.3.62f1, legacy Input (no Input System), TextMeshPro, namespace VibeJam, [SerializeField] private fields.

Given these existing kit scripts: GameStateMachine, InputMap, GameConfig, PlayerController2D, Spawner, ScoreManager, AudioService, UIBinder.

Implement Playbook C · Risk-Reward Positioning:
- A "ScoringZone" MonoBehaviour with a Collider2D (isTrigger = true) that detects the player staying inside via OnTriggerStay2D. While the player is inside AND GameState == Playing, the zone accrues fractional score at a rate baseZoneScorePerSecond * zoneStreakMultiplierCurve.Evaluate(currentStreakSeconds), and calls ScoreManager.Instance.Add at whole-point boundaries.
- Plays AudioService.Play(SfxKey.Tick) periodically (every 0.5s) while the player is inside.
- Resets streak when the player exits the zone.
- Spawner inside the zone spawns hazards that the player must dodge within the zone — hitting one triggers GameStateMachine.Instance.EndGame().

Produce ScoringZone.cs first (include AI EXTENSION HINTS).
```
