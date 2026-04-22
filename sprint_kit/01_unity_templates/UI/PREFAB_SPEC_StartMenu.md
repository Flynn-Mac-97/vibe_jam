# Prefab Spec · UI_StartMenu

Save path: `Assets/UI/Prefabs/UI_StartMenu.prefab`

## Prefab hierarchy

```
UI_StartMenu (GameObject, RectTransform full-stretch)
├── Background (Image, black 60% alpha — optional; skip if scene art is enough)
├── TitleText  (TextMeshProUGUI, anchored top-center, ~200 px from top)
├── StartButton (Button + TMP child label "Start")
└── QuitButton  (Button + TMP child label "Quit")
```

## Styling

- TitleText: font size 120, white. Default content: `VIBE JAM` (teams can change on Sunday to match chosen theme).
- Buttons: 400 × 90 px, rounded rectangle, TMP label font size 48.
- Button vertical layout: StartButton centered at screen y ≈ 50%, QuitButton 120 px below.

## UIBinder wiring

- `startPanel` → this UI_StartMenu root
- `StartButton.onClick` → `UIBinder.OnClickStart()`
- `QuitButton.onClick`  → `UIBinder.OnClickQuit()`

## Notes

- The title content is the one place theme-specific text appears in a template. Everything else stays neutral.
- No animation required at the template stage. Sunday juice pass can add fade-in.
