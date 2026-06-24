# QA Test Plan

## Overview
This plan outlines the testing strategy for Tiny Garden Helper to ensure a child-safe, stable, and responsive experience on mobile devices.

## Device & Platform Testing
- **Android**: Test on at least one low/mid-range Android device and one Android tablet. Check safe areas and back button behavior.
- **iOS**: Test on iPhone (notched) and iPad. Verify safe area padding and audio interruption behavior.
- **Orientation**: App must remain locked in portrait mode.

## Scene Flow Testing
- **Boot to Main Menu**: Start from the Boot scene, verify the splash screen displays briefly, then automatically loads the Main Menu.
- **Main Menu**: Verify the "Play" button loads the Garden scene. Verify the "Settings" button opens the Parental Gate placeholder.
- **Parental Gate**: Verify that clicking the incorrect answer closes the gate, and the correct answer logs a success message.
- **Garden**: Verify the "Home" button returns to the Main Menu. Verify tapping the three activity spots outputs a "Coming next" debug log.

## Core Interaction Testing
- **Touch Responsiveness**: Ensure all buttons and interactive elements have large hit areas.
- **Drag and Drop**: For shape sorting, verify shapes follow the finger smoothly and snap correctly without getting stuck.
- **Multi-Touch Avoidance**: Ensure accidental multi-touch doesn't trigger unexpected behavior or crashes.

## Gameplay & Logic Testing
- **Color Match Mini-Game**:
  - Drag butterflies to matching flowers. Ensure `DragItemBase` handles touch/mouse inputs correctly.
  - Verify dragging the wrong color returns the butterfly gently to its starting position and triggers a pulse hint on the drop target.
  - Verify dragging the correct color snaps it to the target and plays a success sparkle sound.
  - Verify completing all three matches opens the Celebration Panel and successfully saves progress.
- **Counting Fruits Mini-Game**:
  - Drag apples into the basket. Verify the basket tracks the correct number and the dots illuminate properly.
  - Verify that if you put too many apples in the basket, it pulses as a hint but does not fail the game.
  - Verify that you can drag an apple out of the basket to reduce the count (self-correction).
  - Verify that reaching the exact target count automatically advances the round after a short delay.
  - Verify that completing all 3 rounds opens the Celebration Panel and successfully saves progress.
- **Incorrect Attempts**: Deliberately make wrong choices to verify that the "Try again" feedback is gentle, non-punitive, and allows infinite retries without blocking progress.
- **Reward Sequence**: Verify that growing all three plants unlocks the animal friend and sticker.

## Audio Testing
- Verify that tapping UI elements plays a soft feedback sound (`ChildFriendlyButton` hooks).
- Verify all settings toggles (Music, SFX, Voice) work independently.
- Ensure audio state persists across app restarts.
- Verify audio pauses and resumes correctly when the app is backgrounded.

## Save Data & Persistence
- Complete partial progress (e.g., 1 out of 3 plants grown) and close the app. Reopen to ensure progress is maintained.
- Test the Parent-Gated Reset: verify the gate works, and that confirming the reset completely clears progress and audio settings.
- Verify `save.json` fallback logic by deliberately corrupting the JSON file in `persistentDataPath`. The app should rename the corrupted file to `save.json.bak` and generate a fresh save without crashing.
- Run `SaveSystemTests` EditMode tests via the Unity Test Runner to verify serialization and logic.

## Offline & Safety Checks
- Run the app in Airplane mode to ensure 100% functionality.
- Verify no external links, ads, or data collection requests appear at any point.
