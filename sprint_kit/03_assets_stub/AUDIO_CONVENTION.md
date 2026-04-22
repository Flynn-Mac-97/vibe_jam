# Audio Naming & Delivery Convention

## Naming

```
sfx_<category>_<name>[_<variant>].wav
bgm_<scene>_<mood>[_<variant>].ogg
```

- Lowercase, underscores, no spaces.
- SFX always `.wav`. BGM always `.ogg`.
- `<category>` for SFX: `player`, `pickup`, `hazard`, `ui`, `game`.
- `<scene>` for BGM: `main`, `menu`, `gameover`.
- `<mood>` for BGM: `calm`, `tense`, `trigger`, `loop`.

## Examples

- `sfx_player_jump.wav`
- `sfx_player_jump_02.wav`
- `sfx_player_pickup.wav`
- `sfx_hazard_hit.wav`
- `sfx_ui_click.wav`
- `sfx_ui_back.wav`
- `sfx_game_fail.wav`
- `sfx_game_win.wav`
- `sfx_game_tick.wav`
- `bgm_main_loop.ogg`

## Format targets

### SFX
- WAV, 44.1 kHz, 16-bit, mono preferred (stereo allowed for spatial effects).
- Peak at -6 dB, RMS around -18 to -12 dB.
- Duration: 50 ms – 900 ms typical. Trim silence from head and tail.
- No tail longer than the sound itself — long reverb tails stack and muddy the mix.

### BGM
- OGG Vorbis, quality 6 or higher (~192 kbps).
- 44.1 kHz, stereo.
- Integrated loudness around -14 LUFS (YouTube / streaming target).
- 30–60 seconds seamless loop. Test the loop join in a DAW — no click, no volume jump.
- No hard full-stop ending; designed to loop.

## Delivery location

- `Assets/Audio/` (created on Sunday when first file is committed).
- Direct replacement: drop new file in place of the same name, scene references update automatically.

## Mapping to code

See `../01_unity_templates/Audio/AUDIO_INTEGRATION.md` for the `SfxKey` / `BgmKey` ↔ filename mapping.

## Do not

- Do not use MP3 (decoding cost + variable latency).
- Do not embed watermarks or DAW credit tones.
- Do not deliver a single stem that contains BGM + SFX mixed together.
- Do not name files with spaces, Chinese characters, or CamelCase.
