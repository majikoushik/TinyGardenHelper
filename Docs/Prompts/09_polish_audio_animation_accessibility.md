# Prompt 09 — Polish, Audio, Animation, UX, Settings, and Accessibility

# Shared Product Context for Every Codex Prompt

You are working on **Tiny Garden Helper**, a professional-quality Unity 2D mobile educational game for children aged **3–7**, especially kindergarten children.

## Product Vision
Tiny Garden Helper is a calm, colorful, reward-based learning game where a child helps a magical garden grow by completing very simple early-learning activities. The MVP has one garden, three learning activities, and one animal unlock. The goal is not to make a huge game; the goal is to make a small game feel polished, safe, delightful, and commercially credible.

## MVP Learning Activities
1. **Color Match** — drag the correct colored butterfly/seed/watering drop to the matching flower.
2. **Counting Fruits** — place 1–5 fruits into a basket based on visual/audio instruction.
3. **Shape Sort** — sort circle, square, and triangle objects into matching homes.

## Core Gameplay Loop
1. Child starts from a friendly main menu.
2. Child enters the magical garden.
3. Garden shows three tiny plants/activity spots.
4. Child taps a spot to open a mini-game.
5. Completing a mini-game grows that plant.
6. The garden becomes more colorful after every success.
7. Completing all three activities unlocks one cute animal friend and a sticker reward.
8. Progress is saved locally and works offline.

## Child Audience Rules
- Target age: 3–7 years.
- Use large touch targets, simple gestures, and minimal text.
- Do not rely on reading; support visual cues and audio hooks.
- No punishment, failure screen, scary content, timers, pressure, streak addiction, or pay-to-win design.
- Give gentle feedback: use “Try again!” behavior, not “Wrong!” behavior.
- Keep sessions short and satisfying.
- Reward with garden growth, sparkles, animal friends, stickers, cheerful sounds, and positive animation.
- Avoid coins, shops, gambling-like loops, loot boxes, manipulative retention, and dark patterns.

## Child Safety and Store Policy Guardrails
- No ads in MVP.
- No in-app purchases in MVP.
- No login/account system.
- No chat, UGC, social features, multiplayer, external links, webviews, or third-party SDKs.
- No analytics, tracking, device identifiers, or data transmission to third parties.
- Any parent/settings area must be protected by a simple parental gate.
- Keep all content age-appropriate for below 7 years.

## Mobile Requirements
- Unity 2D.
- Android and iOS target.
- Portrait orientation.
- Safe area support for notches and rounded corners.
- Touch-first input.
- Offline-first.
- Local save only.
- Lightweight performance for mid-range Android phones and iPhones.
- No third-party packages unless absolutely necessary and approved in documentation.

## Professional Product Quality Bar
- Clean folder structure.
- Modular C# architecture.
- Runtime stability.
- Smooth UI transitions.
- Consistent soft cartoon visual direction.
- Simple animation/micro-interactions.
- Polished placeholder assets if final art is unavailable.
- Clear documentation after every step.
- Build-readiness checks for Android/iOS.
- Tests or validation scripts where practical.

## Technical Architecture Preferences
Use modular systems:
- `GameManager`
- `SceneLoader`
- `AudioManager`
- `SaveSystem`
- `RewardSystem`
- `MiniGameManager`
- `UIManager`
- `SafeAreaFitter`
- `ParentalGate`
- `HapticsManager` only if implemented safely and optionally disabled

Use ScriptableObjects where useful for mini-game configuration, reward definitions, audio catalogs, visual theme settings, and activity definitions.

## Current Research / Platform References to Keep in Mind
- Unity Android builds can produce APK/AAB for Android distribution.
- Unity iOS builds generate an Xcode project for iOS deployment.
- Apple Kids Category expects age-appropriate content, child-data protection, and parental gates; Kids Category apps should not include third-party advertising or third-party analytics.
- Google Play Families policy has special requirements for apps targeting children/families, including monetization and advertising restrictions.
- Child UX research emphasizes designing differently for young children, considering cognitive development, motor control, touch interaction, attention span, and emotional safety.
- UNICEF digital play guidance emphasizes safety, autonomy, competence, well-being, and avoiding manipulative sensory mechanics that pressure children to keep playing.

## Non-Negotiable Definition of Done for Every Step
After completing the requested step:
1. The Unity project must compile without C# errors.
2. No unrelated features should be introduced.
3. Existing behavior must not be broken.
4. Any manual Unity Editor steps must be documented clearly.
5. Any generated assets/placeholders must be organized under `Assets/_TinyGarden/`.
6. Update relevant docs in `Docs/` and `README.md` if the step changes behavior or setup.
7. Provide a concise summary of changed files and how to test the milestone.


## Your Role
You are now the product polish lead. The game is functionally playable; this step must make it feel like a real commercial mobile game while staying small, safe, lightweight, and child-friendly.

## Task Objective
Add production polish across UI, animation, audio hooks, settings, sensory-safe mode, accessibility, and parent-facing information. Do not expand the MVP beyond three activities and one final reward.

## Polish Goals
The game should feel warm, soft, clear, responsive, visually consistent, safe for very young children, and intentionally designed rather than prototype-like.

## Required Areas

### 1. Visual Polish
Improve placeholder visuals while keeping them lightweight:
- Consistent color palette.
- Rounded UI components.
- Soft gradient/layered background if feasible.
- Better button states.
- Consistent cartoon garden elements.
- Better spacing and portrait hierarchy.
- No clutter.

Create/update:
```text
Assets/_TinyGarden/Scripts/UI/ThemeManager.cs
Assets/_TinyGarden/Scripts/UI/TinyGardenTheme.cs
Assets/_TinyGarden/Scripts/UI/ButtonAnimator.cs
Assets/_TinyGarden/Scripts/UI/SimpleTween.cs
```
Avoid third-party tween libraries.

### 2. Animation Polish
Add micro-animations: button press scale, plant growth bounce, sparkle fade, animal idle bounce/wave, draggable snap, and scene fade transition. No flashing. Keep motion slow enough for children. Sensory-safe mode reduces intensity.

### 3. Audio Polish
Use audio hooks for music, SFX, voice, button tap, correct answer, gentle retry, and reward. If actual clips are missing, create silent placeholders or document placement. Do not add copyrighted audio.

Voice prompt placeholders:
- “Let’s help the garden!”
- “Match the colors!”
- “Put apples in the basket!”
- “Find each shape’s home!”
- “You helped the garden grow!”

### 4. Settings Screen Behind Parental Gate
Add/finish a parent/settings screen protected by parental gate. Include Music on/off, Sound effects on/off, Voice on/off, Sensory-safe mode on/off, and Reset progress with confirmation.

Parent-facing info:
- Learning goals: colors, counting 1–5, shapes.
- Privacy statement: local-only progress, no ads, no analytics, no account.
Do not add external links in MVP.

### 5. Accessibility and Preschool UX
Implement/improve large touch targets, audio+visual cues, shape matching not relying only on color, minimal text, readable parent text, safe area support, reduced animation mode, clear back/home navigation, and no accidental exits due to tiny buttons.

### 6. Commercial Product Details
Add if feasible: mascot blink/idle on main menu, occasional cloud/butterfly ambience, positive hint pulse, replay affordance for completed plants, first-run intro panel, parent-screen learning recap.

## Performance Requirements
Keep UI simple, avoid excessive particles, avoid unnecessary physics and high-frequency Update loops, pool sparkles if repeated, keep texture placeholders small, and avoid repeated allocations during drag.

## Required Review Areas
Review/improve scripts under UI, Audio, Rewards, Garden, MiniGames, and Platform. Add scripts only if they reduce duplication or improve product quality.

## Documentation Updates
Update `ART_DIRECTION.md`, `AUDIO_DIRECTION.md`, `QA_TEST_PLAN.md`, `CHILD_SAFETY_CHECKLIST.md`, and README.

## QA Checklist
Verify large buttons, non-reading main path, sensory-safe behavior, persistent settings, parent-gated reset, no external links in child UI, and no ads/analytics/network/SDKs.

## Constraints
No monetization, new mini-games, external SDKs/assets without licensing docs, social sharing, notifications, or dark patterns.

## Validation
Project compiles; full MVP loop still works; settings persist; missing audio clips do not throw; sensory-safe mode affects major animations/effects; UI works in common portrait sizes.

## Final Response Required from Codex
Return polish summary, changed files, before/after behavior improvements, test steps for settings/sensory/audio/full gameplay, manual art/audio remaining, and next prompt name.
