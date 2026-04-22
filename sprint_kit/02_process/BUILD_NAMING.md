# Build Naming

## Format

```
YYYYMMDD_HHmm_vibejam_<stage>[.ext]
```

- `YYYYMMDD_HHmm` — local time of the build.
- `<stage>` — one of: `preloop`, `firstPlayable`, `juiced`, `final`, `hotfix`.
- `.ext` — `.zip` if archived, or leave off for the folder form.

## Examples

- `20260426_0930_vibejam_preloop/`
- `20260426_1230_vibejam_firstPlayable.zip`
- `20260426_1530_vibejam_juiced/`
- `20260426_1730_vibejam_final.zip`

## Rules

- Build per gate: after each playtest, and on Lock.
- Zip each build into `Assets/../Builds/` (outside the Unity Assets folder) or a separate `builds/` directory at the repo root — never inside `Assets/`.
- Keep at least the last 3 builds on disk. Older ones can be deleted.
- Final demo upload uses the `final` build only.
- If a hotfix happens after `final`, bump stage to `hotfix` and include a one-line note in the zip name comment or in `CHANGELOG.txt` inside the zip.

## Recording naming

- `YYYYMMDD_HHmm_vibejam_demo.mp4` (uncompressed or H.264, 1080p, 30 or 60 fps).
- `YYYYMMDD_HHmm_vibejam_screenshot_<n>.png`.
