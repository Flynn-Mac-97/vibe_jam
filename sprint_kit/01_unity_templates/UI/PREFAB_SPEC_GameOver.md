# Prefab Spec · UI_GameOver

Save path: `Assets/UI/Prefabs/UI_GameOver.prefab`

## Prefab hierarchy

```
UI_GameOver (GameObject, RectTransform full-stretch)
├── Background (Image, black 70% alpha)
├── HeaderText         (TextMeshProUGUI, anchored top-center)
├── FinalScoreText     (TextMeshProUGUI, center)
├── FinalHighScoreText (TextMeshProUGUI, below final score)
├── RestartButton      (Button + TMP label "Restart")
└── MenuButton         (Button + TMP label "Main Menu")
```

## Styling

- HeaderText: font size 96, white, content `Game Over` (or `Run Ended` / `Time's Up` per playbook).
- FinalScoreText: font size 56, content template `Score: {n}` (set by `UIBinder`).
- FinalHighScoreText: font size 32, content template `Best: {n}`.
- Buttons: 400 × 90 px, side by side, 40 px gap, centered below the scores.

## UIBinder wiring

- `gameOverPanel`      → this UI_GameOver root
- `finalScoreText`     → FinalScoreText
- `finalHighScoreText` → FinalHighScoreText
- `RestartButton.onClick` → `UIBinder.OnClickRestart()`
- `MenuButton.onClick`    → `UIBinder.OnClickReturnToMenu()`

## Notes

- A GameOver prefab should also accept an `R` keyboard shortcut for restart. Add a tiny script `RestartShortcut.cs` on Sunday if desired, reading `InputMap.GetRestartDown()` and calling `UIBinder.OnClickRestart`.
- Keep the transition from gameplay → GameOver snappy. No fade-out longer than 0.3 s.
