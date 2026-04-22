# Placeholder Sprite Naming

For any new sprite created during the sprint as a stand-in.

## Format

```
pl_<category>_<descriptor>[_<variant>].png
```

- Prefix `pl_` marks the file as a placeholder (can be replaced without breaking logic).
- `<category>` — one of: `player`, `pickup`, `hazard`, `env`, `ui`, `fx`.
- `<descriptor>` — short english noun or adjective. `star`, `meteor`, `moon`, `button`, `spark`.
- `<variant>` — optional. `red`, `02`, `small`, `highlight`.
- Lowercase, underscores, no spaces.

## Examples

- `pl_player_body.png`
- `pl_pickup_star.png`
- `pl_pickup_star_gold.png`
- `pl_hazard_meteor.png`
- `pl_env_platform.png`
- `pl_ui_button_primary.png`
- `pl_fx_spark.png`

## Location

- `Assets/Sprites/_placeholder/` for any new placeholder sprite added during the sprint.
- Do not replace or rename the four existing sprites in `Assets/Sprites/` (circle, ground, star, white_square) — they are referenced by the existing scene.

## Import settings (Unity)

- Sprite Mode: `Single` (or `Multiple` if a sheet).
- Pixels Per Unit: `100` (keep consistent with existing Flynn sprites unless the playbook needs pixel art at 16 PPU).
- Filter Mode: `Bilinear` (default). `Point` only if going pixel-art.
- Compression: `Normal Quality`. Do not leave at Full Alpha RGBA 32 bit for large sheets.

## Replace rule

- A placeholder is replaced by swapping the file contents while keeping the filename. If the filename changes, the prefab / scene loses the reference.
- When replaced with final art, drop the `pl_` prefix is optional — but renaming means updating references, so only do it if you have time.
