# p02 · Add Enemy / Hazard / Pickup Behavior

Use when: you need a new gameplay object that reacts to the player — a hazard that chases, a pickup with a lifetime, a patroller.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D, legacy Input. Namespace VibeJam.Gameplay, [SerializeField] private conventions, Coroutine over async.

Existing kit services (already in Assets/Scripts/):
- GameStateMachine (Instance, State, OnStateChanged)
- ScoreManager (Instance.Add(int))
- AudioService (Instance.Play(SfxKey))
- GameConfig (ScriptableObject with BasePickupScore, MoveSpeed, DifficultyAt)

Task: create a new MonoBehaviour [NAME].cs for a prefab with the following behavior:
- Role: [pickup / hazard / enemy]
- On trigger with Player (tag "Player"): [what happens — Add(score), EndGame, Destroy self]
- On Update: [movement — fall with gravity / home toward player / patrol between bounds / pulse in place]
- Lifetime: [destroy after N seconds / on exit screen / forever]
- Visual hook: [reference to an Animator or spriteRenderer? any animation trigger on event?]
- Audio hook: [which SfxKey plays on which event]

Constraints:
- Namespace VibeJam.Gameplay.
- Include AI EXTENSION HINTS block.
- Gated by GameStateMachine.State == Playing; no behavior during Menu / Paused / GameOver.
- Under 100 lines. No new dependencies.

Output: only [NAME].cs. No explanation.
```

## Example fill

- NAME: `HazardMeteor`
- Role: hazard
- On trigger: `AudioService.Instance.Play(SfxKey.Hit); AudioService.Instance.Play(SfxKey.Fail); GameStateMachine.Instance.EndGame(); Destroy(gameObject);`
- On Update: falls with Rigidbody2D gravity; no manual movement
- Lifetime: destroy when Y below -10
- Visual: none beyond sprite
- Audio: Hit + Fail on trigger
