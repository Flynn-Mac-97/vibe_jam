# Font Source

One open-source CJK font so TMP can render Chinese and English without missing glyphs.

## Recommended

**Source Han Sans (思源黑体)** — Adobe / Google, SIL Open Font License.
- Download: https://github.com/adobe-fonts/source-han-sans/releases
- Pick the `SubsetOTF/CN` folder → `SourceHanSansCN-Regular.otf` and `SourceHanSansCN-Bold.otf`.

## Alternative

**Alibaba PuHuiTi (阿里巴巴普惠体)** — free for commercial and non-commercial.
- Download: https://fonts.alibabagroup.com/

## Steps to use in Unity

1. Download the OTF / TTF files above.
2. Place under `Assets/UI/Fonts/` (create the folder).
3. In Unity: `Window → TextMeshPro → Font Asset Creator`.
4. Settings:
   - Source Font File: the OTF.
   - Sampling Point Size: `Auto Sizing`.
   - Padding: 5.
   - Packing Method: `Optimum`.
   - Atlas Resolution: 2048 × 2048.
   - Character Set: `Characters from File` + text file listing your used characters (English A-Z a-z 0-9 punctuation, plus any Chinese on UI_StartMenu's title if you use Chinese).
   - Render Mode: `SDFAA`.
5. `Generate Font Atlas` → `Save`.
6. Drop the resulting Font Asset into every TMP component's `Font Asset` field.

## Notes

- Do not use the Dynamic mode for SDF fonts in a jam — too slow to generate on demand.
- One font is enough. Adding a second font for flavor is low priority until the juice pass.
- Do not ship closed-source commercial fonts. Check licenses before adding anything else.
