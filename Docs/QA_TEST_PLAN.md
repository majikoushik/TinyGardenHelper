# QA Test Plan

## Overview
This plan outlines the testing strategy for Tiny Garden Helper to ensure a child-safe, stable, and responsive experience on mobile devices.

## Device & Platform Testing
- **Android**: Test on at least one low/mid-range Android device and one Android tablet. Check safe areas and back button behavior.
- **iOS**: Test on iPhone (notched) and iPad. Verify safe area padding and audio interruption behavior.
- **Orientation**: App must remain locked in portrait mode.

## Core Interaction Testing
- **Touch Responsiveness**: Ensure all buttons and interactive elements have large hit areas.
- **Drag and Drop**: For shape sorting, verify shapes follow the finger smoothly and snap correctly without getting stuck.
- **Multi-Touch Avoidance**: Ensure accidental multi-touch doesn't trigger unexpected behavior or crashes.

## Gameplay & Logic Testing
- **Mini-Games**: Complete each mini-game successfully.
- **Incorrect Attempts**: Deliberately make wrong choices to verify that the "Try again" feedback is gentle, non-punitive, and allows infinite retries without blocking progress.
- **Reward Sequence**: Verify that growing all three plants unlocks the animal friend and sticker.

## Audio Testing
- Verify all settings toggles (Music, SFX, Voice) work independently.
- Ensure audio state persists across app restarts.
- Verify audio pauses and resumes correctly when the app is backgrounded.

## Save Data & Persistence
- Complete partial progress (e.g., 1 out of 3 plants grown) and close the app. Reopen to ensure progress is maintained.
- Test the Parent-Gated Reset: verify the gate works, and that confirming the reset completely clears progress and audio settings.

## Offline & Safety Checks
- Run the app in Airplane mode to ensure 100% functionality.
- Verify no external links, ads, or data collection requests appear at any point.
