# Audio Mixer Spec · MainMixer

Save path: `Assets/Audio/MainMixer.mixer`

## Groups

```
Master
├── Music
├── SFX_UI
└── SFX_Gameplay
```

## Creation steps

1. `Assets → Create → Audio Mixer` → name it `MainMixer`.
2. In the Audio Mixer window, under `Groups`, right-click `Master` → `Add child group` three times for `Music`, `SFX_UI`, `SFX_Gameplay`.
3. For each group, right-click the `Volume` attenuator in the inspector → `Expose 'Volume (of …)' to script`. Rename each exposed parameter:
   - `Master_Volume`
   - `Music_Volume`
   - `SFX_UI_Volume`
   - `SFX_Gameplay_Volume`
4. (Settings page, if any) read/write these exposed parameters through `AudioMixer.SetFloat(name, value)`. Volume is in dB; `-80` is silent, `0` is default, `+20` is max.

## Default levels (dB)

| Group | Default |
|---|---|
| Master | 0 |
| Music | -4 |
| SFX_UI | -2 |
| SFX_Gameplay | 0 |

## Usage in code

`AudioService` holds `AudioMixerGroup` references for each group via `[SerializeField]`. Drag the groups from `MainMixer.mixer` (expand the asset in the Project window) into the corresponding fields on the `AudioService` component.

## Notes

- Keep it simple. Do not add snapshots or ducking for this demo.
- No effects on Master (no compressor, no reverb). Added noise is not worth the CPU or the debug time.
- If the demo ever has to be muted quickly (e.g., during a recording), use a single Master mute toggle reading `Master_Volume`.
