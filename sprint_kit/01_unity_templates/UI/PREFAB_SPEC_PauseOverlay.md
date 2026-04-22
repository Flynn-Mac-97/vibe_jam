# Prefab Spec · UI_PauseOverlay

Save path: `Assets/UI/Prefabs/UI_PauseOverlay.prefab`

## Prefab hierarchy

```
UI_PauseOverlay (GameObject, RectTransform full-stretch)
├── Background (Image, black 50% alpha)
├── HeaderText    (TextMeshProUGUI, anchored center, ~150 px above center)
├── ResumeButton  (Button + TMP label "Resume")
└── MenuButton    (Button + TMP label "Main Menu")
```

## Styling

- HeaderText: font size 72, white, content `Paused`.
- Buttons: 400 × 90 px, stacked, 20 px gap, centered.

## UIBinder wiring

- `pausePanel` → this UI_PauseOverlay root
- `ResumeButton.onClick` → `UIBinder.OnClickResume()`
- `MenuButton.onClick`   → `UIBinder.OnClickReturnToMenu()`

## Time-scale behavior

- On Pause, set `Time.timeScale = 0f`. On Resume / Restart / ReturnToMenu, set `Time.timeScale = 1f`.
- Implement this in a small method on `GameStateMachine` if the playbook needs it — or in `UIBinder.HandleStateChanged`. Not included in the template by default; add on Sunday if needed.

## Notes

- Pause only applies to Playing → Paused transitions. Do not allow pausing in Menu or GameOver states.
- Avoid blocking all input in Pause — ESC should still resume.
