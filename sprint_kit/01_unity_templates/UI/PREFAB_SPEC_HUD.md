# Prefab Spec · UI_HUD

Save path: `Assets/UI/Prefabs/UI_HUD.prefab`

## Canvas settings (top-level Canvas in the scene)

- Render Mode: `Screen Space - Overlay`
- Canvas Scaler: `Scale With Screen Size`, Reference Resolution `1920 × 1080`, Screen Match Mode `Match Width Or Height`, Match `0.5`
- Add `GraphicRaycaster`.
- Add an `EventSystem` GameObject in the scene (Unity creates one automatically when first UI is added — verify it exists).

## Prefab hierarchy

```
UI_HUD (GameObject, RectTransform full-stretch)
├── ScoreText            (TextMeshProUGUI, anchored top-left, 80 px from each edge)
├── HighScoreText        (TextMeshProUGUI, anchored top-left, below ScoreText)
├── PauseButton          (Button + TMP label "Pause" or icon, anchored top-right)
└── HintText (optional)  (TextMeshProUGUI, anchored bottom-center, for prompts like "Press R to restart")
```

## Text styling

- ScoreText: font size 72, white, regular. Default content: `0`.
- HighScoreText: font size 28, 70% white opacity. Default content: `Best: 0`.
- HintText: font size 32, 80% white opacity.

## UIBinder wiring (on the parent Canvas)

- `scoreText`     → HUD/ScoreText
- `highScoreText` → HUD/HighScoreText
- `hudPanel`      → this UI_HUD root
- `PauseButton.onClick` → `UIBinder.OnClickPause()`

## Notes

- Do not put gameplay logic on this prefab. It reads from events.
- If the chosen playbook needs more HUD elements (combo counter, time remaining), add them in Sunday's prefab edit — but still expose them through `UIBinder` fields, not directly via Find or singletons.
