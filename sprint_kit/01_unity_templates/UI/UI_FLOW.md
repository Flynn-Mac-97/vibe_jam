# UI Flow

```
                +-------------+
                |  StartMenu  |
                +------+------+
                       | OnClickStart
                       v
              +-------------------+
              |        HUD        |<------+
              +--+-------------+--+       |
                 |             |          |
                 | OnClickPause|          | OnClickResume
                 v             |          |
           +-----------+       |          |
           |   Pause   |-------+----------+
           +-----+-----+
                 | OnClickReturnToMenu
                 v
              (Menu)
                 
  HUD -----(player dies / time up / win condition)-----> GameOver
  GameOver --OnClickRestart--> HUD (new run, score reset)
  GameOver --OnClickReturnToMenu--> StartMenu
```

## States & panels

| GameState | StartMenu | HUD | Pause | GameOver |
|---|---|---|---|---|
| Menu     | shown | hidden | hidden | hidden |
| Playing  | hidden | shown | hidden | hidden |
| Paused   | hidden | shown | shown | hidden |
| GameOver | hidden | hidden | hidden | shown |

`UIBinder.HandleStateChanged` is the single place that toggles visibility. Do not add `SetActive` calls elsewhere.

## Transitions triggered from game code (not buttons)

- `Playing → Paused`: player pressed Esc/P → `InputMap.GetPauseDown()` → `GameStateMachine.Pause()`. Wire this check in a small `PauseListener.cs` if you want, or inside `UIBinder.Update()`. (Not included in templates — add on Sunday if the playbook requires pausing.)
- `Playing → GameOver`: gameplay script calls `GameStateMachine.Instance.EndGame()` when the loss condition triggers.

## Buttons

| Panel | Button | UIBinder method |
|---|---|---|
| StartMenu | Start | `OnClickStart` |
| StartMenu | Quit  | `OnClickQuit` |
| Pause     | Resume | `OnClickResume` |
| Pause     | Main menu | `OnClickReturnToMenu` |
| GameOver  | Restart | `OnClickRestart` |
| GameOver  | Main menu | `OnClickReturnToMenu` |

All button OnClick listeners are wired via inspector, no runtime AddListener.
