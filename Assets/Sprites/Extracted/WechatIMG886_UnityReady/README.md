# WechatIMG886 Unity-Ready Sprite Assets

Generated from `source/sprite_sheet_source.png`.

- Final usable assets: `sprites_3x/`
- Scale: 3x
- Format: PNG with alpha transparency
- Count: 135
- `manifest.csv` maps every output file back to its crop location in the source sheet.

Unity import suggestion:
- Texture Type: Sprite (2D and UI)
- Sprite Mode: Single
- Alpha Is Transparency: enabled
- Filter Mode: Bilinear for this hand-drawn art, or Point if the team wants sharper pixels
- Compression: None while prototyping

Note: five UI meter/descent assets span a horizontal grid line in the source image, so this folder includes merged versions and omits their split halves.
