# p03 · Add UI Animation

Use when: a UI panel (StartMenu, HUD, GameOver, Pause) feels static. Needs slide-in, pulse, shake, or fade.

## Prompt

```
Context: Unity 2022.3.62f1, URP 2D, TextMeshPro, no DOTween. Namespace VibeJam.UI, [SerializeField] private conventions, Coroutine-based animation (no tweening library).

Target prefab / GameObject: [path e.g., Assets/UI/Prefabs/UI_GameOver.prefab, inside it node named "HeaderText"]

Existing kit scripts: UIBinder handles panel show/hide based on GameStateMachine events.

Task: add a MonoBehaviour [NAME].cs that, when the GameObject becomes active (OnEnable), performs [EFFECT]:
- Effect description: [e.g., "slides in from right: RectTransform.anchoredPosition animates from (1920, 0) to (0, 0) over 0.3 s with smootherstep easing"]
- Optional: [scale pulse, alpha fade, text char-by-char reveal]
- On OnDisable, reset state to initial (ready for next activation).

Constraints:
- Pure Coroutine, no DOTween / Leantween / animation libraries.
- Namespace VibeJam.UI.
- Include AI EXTENSION HINTS block.
- Use Time.unscaledDeltaTime so animation runs during Pause if Time.timeScale is 0.
- Under 80 lines.

Output: only [NAME].cs. No explanation.
```

## Example

- NAME: `HeaderSlideIn`
- Effect: anchoredPosition lerps from (0, 200) down to (0, 0) over 0.4 s, sin-based ease-out, then overshoots 10 px and settles.
- Auto-reset on OnDisable to (0, 200).
