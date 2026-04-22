# Playtest Checklist

25 items, two passes (Playtest #1 and #2). Tick each item live during the test.

## Pre-test (30 s)

1. [ ] Latest build launches from a double-click or from Unity Editor without a prior console error.
2. [ ] A fresh tester (someone not coding the loop) is at the keyboard.

## Onboarding (first 30 s of the tester's experience)

3. [ ] The tester sees a title or start prompt and knows how to begin.
4. [ ] The tester figures out the primary control (move) within 5 seconds.
5. [ ] The tester figures out the secondary control (jump / action) within 15 seconds.
6. [ ] The tester knows the objective within 30 seconds without being told.

## Core loop (seconds 30–180)

7. [ ] The tester successfully performs the core interaction (pick up / dodge / hit) at least once.
8. [ ] There is immediate, visible feedback on the core interaction.
9. [ ] There is an audible feedback on the core interaction.
10. [ ] The score or progress indicator updates when the tester performs the interaction.
11. [ ] There is a clear failure condition the tester can observe (not "the game gets boring").
12. [ ] The failure condition triggers a distinct visual + audio event.
13. [ ] After failure, the tester sees a GameOver screen with their score and a way to restart.

## Replay

14. [ ] Restart works from the GameOver screen.
15. [ ] Restart resets score to 0.
16. [ ] Restart resets gameplay state (no leftover enemies, no leftover projectiles).
17. [ ] The tester wants to play again without being prompted. (If no → critical signal, see §Reflection.)

## Stability

18. [ ] No console errors during a 3-minute play session.
19. [ ] No frame rate drop below 30 fps during peak spawn density.
20. [ ] No input lost during rapid presses.
21. [ ] Pause (if implemented) freezes gameplay and resumes cleanly.

## Packaging

22. [ ] `Build → Windows (or Mac) → StandaloneBuild` completes without errors.
23. [ ] The build launches and plays identically to Editor mode.
24. [ ] Quit button actually quits (desktop builds only).

## Presentability

25. [ ] The demo can be demoed to a stranger in 3 minutes without verbal explanation.

---

## Reflection (fill right after the test, 2 min max)

- Top 3 issues (ranked by impact, not ease):
  1. …
  2. …
  3. …
- Did the tester want to play again?  Yes / No / Unclear
- What surprised the tester?
- If we had 30 more minutes, we would…

Skip issues ranked #4+. Address top-3 only between playtests.
