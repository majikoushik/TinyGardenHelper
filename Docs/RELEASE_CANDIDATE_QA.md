# Release Candidate QA Test Plan

Before generating the final `.aab` (Android) or Xcode Project (iOS) for release, the QA tester must manually execute these exact 13 steps on a physical device.

## Prerequisites
- Uninstall any previous version of the app from the test device to ensure a fresh save state.
- Ensure the device is entirely offline (Airplane Mode ON, WiFi OFF).

## The 13-Step Checklist

1. **Fresh Install Boot**: Launch the app. Verify it boots correctly without crashing and reaches the Main Menu.
2. **Main Menu Flow**: Tap the `Play` button. Verify a smooth transition to the Garden scene.
3. **Color Match Test**: Tap the Color Flower spot. Complete the matching game. Verify you are returned to the Garden and the Color Flower is visually grown.
4. **Counting Fruits Test**: Tap the Fruit Tree spot. Complete the counting game. Verify you are returned to the Garden and the Fruit Tree is visually grown.
5. **Shape Sort Test**: Tap the Shape Bush spot. Complete the shape sorting game.
6. **Reward Sequence Validation**: Upon returning to the garden after the 3rd game, verify the Celebration Sequence plays once. Verify Benny Bunny appears in the Garden. Verify the Sticker Book shows the unlocked Star.
7. **Idempotency & Persistence**: Hard-close the app (swipe away in OS). Relaunch the app. Tap Play. Verify you are immediately in the Garden with all plants fully grown, the animal present, and the sticker unlocked, *without* the celebration playing again.
8. **Parental Gate Test**: Return to the Main Menu. Tap `Settings`. Purposely answer the math question incorrectly. Verify entry is denied. Answer correctly (`132`). Verify the Settings Panel opens.
9. **Audio Toggles**: Inside Settings, toggle `Music`, `SFX`, and `Voice` to OFF. Return to the game. Verify complete silence. Toggle them back ON and verify audio returns.
10. **Sensory Safe Test**: Inside Settings, toggle `Sensory Safe Mode` to ON. Return to the game. Verify that scene transitions instantly cut (no fade to black) and buttons no longer bounce when tapped.
11. **Reset Progress**: Inside Settings, tap `Reset Progress`. Return to the Garden. Verify all plants are reset, the animal is gone, and the sticker is locked.
12. **Device Hardware & Screen Layout Check**: Execute this plan on real devices: an Android phone, an Android tablet (if possible), an iPhone, and an iPad (if possible). Verify UI on a notch/safe-area screen and a small screen. Ensure UI elements do not clip off-screen.
13. **Offline & Network Verification**: Confirm the app was launched in airplane mode/offline, and the entire 1-12 loop was successfully completed with absolutely no external network calls or server requests.

**If all 13 steps pass, the build is certified as a Release Candidate.**
