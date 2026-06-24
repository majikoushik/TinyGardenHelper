# Mobile Build Checklist

## Project Settings

- Unity LTS version selected.
- 2D project template or equivalent renderer setup.
- Portrait orientation locked.
- Package name and bundle identifier defined.
- Version and build number configured.
- Minimum Android and iOS versions chosen.
- App icon placeholders added.
- Splash screen configured.
- Managed stripping level tested.
- Target frame rate set appropriately, such as 30 or 60 FPS after testing.

## Android

- Android Build Support installed.
- SDK, NDK, and JDK configured through Unity Hub.
- Package name uses reverse-DNS format.
- ARM64 enabled.
- Development build tested on at least one mid-range Android phone.
- App works offline.
- Back button behavior is child-safe and predictable.
- Audio continues or pauses appropriately when the app is backgrounded.
- Save data survives app restart.

## iOS

- iOS Build Support installed.
- Bundle identifier configured.
- Xcode project exports successfully.
- Portrait orientation confirmed in Xcode.
- Safe area works on notched devices.
- App works offline.
- Audio interruption behavior tested.
- Save data survives app restart.
- No privacy-sensitive permissions requested for MVP.

## Performance

- Stable frame rate in Garden scene.
- No large texture memory spikes.
- Sprite atlases considered once final art arrives.
- Audio clips compressed appropriately.
- No unnecessary Update loops.
- No network calls.
- No third-party SDK initialization.

## UI And Device Fit

- All buttons are large enough for young children.
- UI respects safe areas.
- Text does not overlap or require reading for core play.
- Works on small phones, large phones, tablets, and iPads.
- Activity panels remain usable in portrait orientation.

## Testing Checklist

### Touch

- All core interactions work with touch input.
- Plant taps are responsive and use large hit areas.
- Dragged shapes do not get stuck under the finger.
- Buttons provide visual and audio feedback.
- Accidental taps do not trigger destructive actions.

### Screen Sizes

- Garden scene fits common phone aspect ratios.
- Garden scene fits tablet and iPad aspect ratios.
- Safe areas are respected on notched devices.
- Activity panels remain centered and usable.
- No UI text or controls overlap.

### Audio

- Music toggle works.
- SFX toggle works.
- Voice toggle works.
- Background music volume is gentle.
- Success and try-again sounds are not startling.
- Audio state persists after app restart.

### Saving

- Completed activities remain completed after restart.
- Grown plants remain grown after restart.
- Animal friend unlock persists.
- Settings persist.
- Parent-gated reset clears progress correctly.
- Corrupt or missing save data falls back to a safe default.

### Gameplay

- Each plant opens the correct activity.
- Color matching can be completed without reading.
- Counting supports fruit counts from 1 to 5.
- Shape sorting supports circle, square, and triangle.
- Incorrect choices show encouraging feedback and allow retry.
- Completing all three activities unlocks the animal friend.
- The full loop works offline.

## Release Readiness

- App icon final or acceptable placeholder.
- Store screenshots planned.
- Privacy policy prepared if required by store listing.
- Child-directed app declarations reviewed.
- No ads, purchases, analytics, accounts, or external links in MVP.
