# Child Safety And Store Compliance Checklist

## Core Safety Rules

- No ads.
- No in-app purchases.
- No login or account system.
- No chat.
- No user-generated content.
- No social sharing.
- No external links accessible to children.
- No analytics or tracking in MVP.
- No networking or cloud services in MVP.
- No scary visuals, sounds, or themes.
- No punishment screen.
- No timer pressure.

## Child-Friendly Interaction

- Use large touch targets.
- Use simple tap and drag interactions.
- Avoid text-heavy instructions.
- Use icons, pictures, animation, and voice prompts.
- Allow repeated attempts.
- Use "Try again!" instead of "Wrong!".
- Provide immediate positive feedback for completion.
- Keep sessions short and easy to finish.

## Parent And Settings Area

- Settings can include music, SFX, and voice toggles.
- Reset progress must be protected by a parental gate.
- Any legal, credits, or support information must be behind the parent area if it includes external navigation.
- Do not place child-accessible website, email, or app store links in the play flow.

## Privacy

- Do not collect names, photos, voice recordings, contacts, location, or device identifiers.
- Do not send gameplay data to a server.
- Save only local progress and audio preferences.
- Keep save data anonymous and device-local.

## Store Compliance Preparation

- Review Google Play Families Policy before release.
- Review Apple Kids Category and child-directed app requirements before release.
- Prepare a plain-language privacy policy, even if the app collects no personal data.
- Confirm that any future SDK is child-safe and compliant before adding it.
- Confirm that all final art, music, sound effects, and fonts are licensed for commercial use.

## Content Review

- Visuals are friendly and non-threatening.
- Sounds are soft and not startling.
- Rewards are emotional and visual, not monetized.
- No manipulative retention mechanics.
- [x] **No Harsh Punishment:** Incorrect answers use the "Try Again" fallback without buzzers or failure screens.
- [x] **No Time Limits:** Children can take as long as they need without timers causing stress.
- [x] **Sensory Safe Mode:** A toggle exists for neurodivergent children to disable sudden screen flashes, fades, and bouncy animations.

### 4. Parental Controls & Privacy
- [x] **Parental Gate:** Access to Settings and "Reset Progress" is strictly gated by an adult-oriented math question.
- [x] **Data Privacy:** Explicitly stated in the Settings panel: no ads, no analytics, and all progress saves locally.

## MVP Acceptance Criteria

- A child can play all three activities without reading.
- Incorrect choices never block progress permanently.
- Completing all three activities unlocks the animal friend.
- The app can be played fully offline.
- Closing and reopening the app preserves progress.
- Parent-only reset is gated.
