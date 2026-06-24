# Prompt 03 — Core Managers and Shared Systems

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
You are building the reusable foundation that makes the game maintainable and scalable: scene loading, state, audio hooks, UI flow, rewards plumbing, safe area, and platform-safe helpers.

## Task Objective
Implement the core systems needed by all future game features. Keep systems simple, production-friendly, and testable. Do not implement full mini-games yet.

## Required Systems

### 1. GameManager
Create `Assets/_TinyGarden/Scripts/Core/GameManager.cs`.
Responsibilities:
- Singleton or bootstrap-managed persistent object.
- Store current session state.
- Expose read-only access to progress/save systems later.
- Provide events for scene loaded, activity selected, and activity completed placeholder.
- Avoid overusing global state.

### 2. SceneLoader
Create `SceneLoader.cs` and `SceneNames.cs`.
Responsibilities:
- Load scenes by enum/string constants.
- Optional fade transition.
- Prevent double-tap duplicate loading.
- Provide child-safe loading screen behavior.
- No network/loading spinners that imply online dependency.

### 3. UIManager / Modal System
Create:
```text
Assets/_TinyGarden/Scripts/UI/UIManager.cs
Assets/_TinyGarden/Scripts/UI/ModalPanel.cs
Assets/_TinyGarden/Scripts/UI/ChildFriendlyButton.cs
```
Responsibilities:
- Show simple modal messages.
- Standardize button feedback: scale on press, soft click sound hook, disabled state.
- Avoid tiny close buttons; use large child-friendly buttons.

### 4. AudioManager
Create:
```text
Assets/_TinyGarden/Scripts/Audio/AudioManager.cs
Assets/_TinyGarden/Scripts/Audio/AudioCue.cs
Assets/_TinyGarden/Scripts/Audio/AudioCatalog.cs
```
Responsibilities:
- Music, SFX, and voice channels.
- Volume controls prepared for future settings.
- Gracefully handle missing clips.
- Provide `PlaySfx`, `PlayVoice`, `PlayMusic`, `StopMusic`.
- Use ScriptableObject catalog if useful.

### 5. RewardSystem Skeleton
Create:
```text
Assets/_TinyGarden/Scripts/Rewards/RewardSystem.cs
Assets/_TinyGarden/Scripts/Rewards/RewardDefinition.cs
Assets/_TinyGarden/Scripts/Rewards/RewardType.cs
```
Define reward types: `PlantGrowth`, `Sparkle`, `Sticker`, `AnimalFriend`. Do not implement full final reward flow yet.

### 6. MiniGame Shared Contracts
Create:
```text
Assets/_TinyGarden/Scripts/MiniGames/Shared/IMiniGame.cs
Assets/_TinyGarden/Scripts/MiniGames/Shared/MiniGameBase.cs
Assets/_TinyGarden/Scripts/MiniGames/Shared/MiniGameResult.cs
Assets/_TinyGarden/Scripts/MiniGames/Shared/ActivityId.cs
Assets/_TinyGarden/Scripts/MiniGames/Shared/ActivityDefinition.cs
```
Completion should return activity id, success flag, attempts, and optional learning metadata. Incorrect attempts should trigger guidance, not failure.

### 7. Parental Gate
Create `Assets/_TinyGarden/Scripts/UI/ParentalGate.cs`.
Use a simple gate: hold button for 3 seconds, or simple arithmetic like “3 + 2”. For MVP, avoid external links entirely.

### 8. Platform Helpers
Create:
```text
Assets/_TinyGarden/Scripts/Platform/DeviceSettings.cs
Assets/_TinyGarden/Scripts/Platform/SafeAreaService.cs
```
Handle portrait orientation notes, safe area calculations, and optional minimal haptics wrappers disabled by default.

## Architecture Requirements
Use clear dependency boundaries, null-safe code, minimal noisy logs, no repeated costly `FindObjectOfType`, and no duplicate persistent managers across scenes.

## Testing Requirements
Add EditMode tests where feasible for non-empty scene constants, mini-game result creation, reward definition validation, and missing audio clip safety.

## Documentation Updates
Update `TECHNICAL_ARCHITECTURE.md`, `QA_TEST_PLAN.md`, `README.md`, and `AGENTS.md`.

## Constraints
Do not implement SaveSystem here unless absolutely needed. Do not implement gameplay. No third-party packages, analytics, tracking, networking, ads, IAP, or cloud.

## Validation
Existing shell still works; no duplicate managers appear after scene transitions; parent gate does not open anything external; missing audio clips do not crash the game.

## Final Response Required from Codex
Return summary of systems, changed files, initialization flow, testing steps, risks/assumptions, and next prompt name.
