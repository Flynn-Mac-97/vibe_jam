# p05 · Add Juice (screen shake / particle / hit stop / layered sound)

Use when: playtest says "it works but feels flat". Pick ONE key event, make it feel great.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D, Cinemachine NOT installed. Namespace VibeJam.Feedback, [SerializeField] private conventions, no tweening library.

Target event: [which moment — "on hazard collision", "on pickup", "on GameOver transition"]

Existing kit services:
- AudioService.Instance.Play(SfxKey)
- Main Camera is orthographic

Task: add a MonoBehaviour [NAME].cs that on being called via a public method Trigger(), performs:
- Screen shake: Main Camera position offsets by random(±amplitude) for duration seconds, then returns exactly to origin. Use Time.unscaledDeltaTime. Non-overlapping — if Trigger is called while active, restart with fresh duration.
- Particle burst: instantiate a one-shot ParticleSystem prefab at [WORLD POSITION] (Vector3 parameter to Trigger).
- Hit stop: set Time.timeScale = 0.05 for [N] ms, then restore to 1.0 via Coroutine using realtime.
- Audio layer: play two SfxKeys with slight pitch randomization on two pooled AudioSources (add to AudioService as Play3D overload if needed, or use PlayOneShot with pitch via temp source).

Pick 2–3 of the 4 sub-effects (over-juicing is worse than under-juicing). Prioritize:
1. [Sub-effect to include]
2. [Sub-effect to include]

Constraints:
- Namespace VibeJam.Feedback.
- Include AI EXTENSION HINTS block.
- Under 120 lines.
- Do not introduce new Unity packages.

Output: [NAME].cs only. No explanation.
```

## Example fill

- NAME: `HitFeedback`
- Target event: player collides with hazard
- Picks: screen shake 0.4 amplitude / 0.15 s + hit stop 50 ms + layered Hit SFX with ±5% pitch
