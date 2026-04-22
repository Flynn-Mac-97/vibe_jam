# Cut Order

When behind schedule, cut in this strict order. Do not skip around. Cutting is not failure — cutting is how a playable demo ships.

## Cut from the top, stop when back on schedule

1. Settings page / volume sliders / key rebind UI
2. Start menu polish (fancy title, animated background, fade-in)
3. Secondary difficulty levels / additional modes
4. Non-core enemy variants or pickup variants (keep 1 of each category)
5. Cosmetic animations (idle, wind, ambient particles beyond the existing moon glow)
6. Extra BGM tracks (keep 1 loop)
7. Screen shake / particle juice on non-critical events (keep shake on the one most important event)
8. Tutorial text / hints beyond a single hint line on HUD
9. High-score persistence (remove PlayerPrefs save; session-only high score)
10. Pause functionality (hard to implement cleanly if late)
11. GameOver screen polish → replace with a plain text overlay
12. Main menu → boot directly into Playing with a "Press any key to start" overlay

## Never cut (these are the loop)

- Movable player (input responds, character moves)
- At least one source of failure (a hazard, a timer, a miss condition)
- At least one source of success feedback (score increments, a pickup effect, a positive sound)
- A way to restart (R key is enough; doesn't need a button)
- A 30–90 second single-run cadence

If any "never cut" item is at risk, **stop cutting and protect it** — revert the last cut, or cut further down the list to buy time.

## Rule

Cuts are reversible until Lock. If a cut frees up 30 min and the team is ahead, they can re-add a lower-priority cut item — but never before the core loop is confirmed stable.

## Who decides

廖琦 calls the cuts based on `PLAYTEST_CHECKLIST.md` results. Flynn and David execute. No debate during the sprint — disagreement is raised once, 廖琦 decides, work proceeds.
