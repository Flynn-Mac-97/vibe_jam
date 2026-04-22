# Audio Integration · trixie ↔ programmers

This is the contract between trixie's audio deliveries and the code that triggers them. Follow this and no wiring work is needed on Sunday beyond dragging the clips into the `AudioService` component.

## Keys the code will trigger

### SFX (one-shot)

| SfxKey | When | Typical length |
|---|---|---|
| `Jump` | player jump input successfully leaves ground | 150–300 ms |
| `Pickup` | player collects a point item | 150–400 ms |
| `Hit` | player takes damage / collides with hazard | 200–500 ms |
| `Fail` | loss condition triggered (GameOver) | 400–900 ms |
| `UIClick` | any UI button press | 80–200 ms |
| `UIBack` | back / cancel button | 80–200 ms |
| `Win` | success condition reached (if the playbook has one) | 400–900 ms |
| `Tick` | periodic tick (timer / beat) | 50–150 ms |

### BGM (looping)

| BgmKey | When | Typical length |
|---|---|---|
| `Main` | entire session (plays in Menu, HUD, Pause, GameOver) | 30–60 s loop |

## Delivery naming

Name files to match the key (case-insensitive). Filename uses the convention in `../../03_assets_stub/AUDIO_CONVENTION.md`:

- `sfx_player_jump.wav`
- `sfx_player_pickup.wav`
- `sfx_player_hit.wav`
- `sfx_game_fail.wav`
- `sfx_ui_click.wav`
- `sfx_ui_back.wav`
- `sfx_game_win.wav`
- `sfx_game_tick.wav`
- `bgm_main_loop.ogg`

## How files reach the game

1. trixie drops files into `Assets/Audio/` (folder created on Sunday when first audio lands).
2. In Unity, select a clip → Inspector → set **Load In Background: off** for short SFX, **Compression: Vorbis (ogg)** for BGM. Short SFX can stay PCM for zero latency.
3. On the `AudioService` component:
   - Expand `Sfx[]` → for each row, set `key` (enum) and drag the matching clip into `clip`.
   - Expand `Bgm[]` → do the same for each BGM key.
4. Programmers call `AudioService.Instance.Play(SfxKey.Jump)` — that's it.

## Delivery format

- SFX: WAV, 44.1 kHz, 16-bit, mono or stereo, headroom -6 dB (avoid clipping).
- BGM: OGG, 44.1 kHz, stereo, target loudness -14 LUFS integrated, 30–60 s clean loop (check the loop join in a DAW before export).

## What programmers will not do

- Will not add AudioSource components anywhere except inside `AudioService`.
- Will not hardcode filenames or paths. Always go through `SfxKey` / `BgmKey`.
- Will not reorder or remove existing enum values in `SfxKey.cs`. New keys append at the end.

## What trixie can assume

- Every `SfxKey` listed above will be triggered by code at least once in a typical demo.
- If a key is not listed, do not author for it until asked.
- You can deliver placeholder mixes Saturday; code will happily use them. Replace with final versions during the Sunday juice pass.
