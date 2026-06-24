# Prompt 10 — Android/iOS Build Readiness, Store Compliance, and Release Candidate QA

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
You are preparing Tiny Garden Helper for mobile release-candidate quality. Your job is to make the Unity project build-ready for Android and iOS, verify child-safety constraints, and produce practical release documentation.

## Task Objective
Prepare the project for Android and iOS build validation. Do not add new gameplay features. Focus on build settings, package hygiene, performance, policy checks, QA, and release readiness.

## Platform Build Context
- Unity can build Android apps for APK/AAB distribution.
- Unity iOS builds generate an Xcode project, which then needs Xcode/macOS signing and deployment.
- Android Play Store generally expects App Bundle (`.aab`) for release.
- iOS release requires Apple Developer account, bundle identifier, signing, icons, launch screen, privacy details, and App Store review compliance.

## Required Build Settings Review

### Common Unity Settings
- Product Name: Tiny Garden Helper
- Company Name: appropriate placeholder or current owner if already set
- Version: `0.1.0` or current semantic version
- Portrait orientation only
- Scene order: Boot, MainMenu, Garden, ColorMatch, CountingFruits, ShapeSort
- Splash/loading behavior
- Scripting backend recommendations: Android IL2CPP for release if supported; iOS IL2CPP standard/required
- Managed stripping: conservative/safe setting
- Minimum OS versions documented

### Android Settings
Document/configure:
- Package name placeholder such as `com.yourstudio.tinygardenhelper`
- Minimum API recommendation
- Target API notes
- Build App Bundle for Google Play release
- Keystore/signing steps documented, not hard-coded
- Internet permission should not be required for MVP
- No ad/analytics SDK dependencies
- Adaptive icon placeholder requirements

### iOS Settings
Document/configure:
- Bundle identifier placeholder
- Team/signing handled in Xcode/manual steps
- Portrait orientation
- iPhone/iPad support decision
- Launch screen/icon requirements
- Privacy manifest/privacy nutrition labels documentation requirements
- Xcode project export flow

## Store Compliance Review
Create/update:
```text
Assets/_TinyGarden/Docs/STORE_COMPLIANCE_CHECKLIST.md
Assets/_TinyGarden/Docs/ANDROID_RELEASE_CHECKLIST.md
Assets/_TinyGarden/Docs/IOS_RELEASE_CHECKLIST.md
Assets/_TinyGarden/Docs/PRIVACY_AND_CHILD_SAFETY_NOTES.md
Assets/_TinyGarden/Docs/RELEASE_CANDIDATE_QA.md
```

Checklist must verify no ads, IAP, third-party analytics/tracking, personal data collection, accounts, external links accessible to children, unsafe content, manipulative retention, push notifications, multiplayer/chat/UGC. Verify parent/settings is protected by parental gate and progress is local-only.

## Build Automation / Validation Scripts
If feasible, add:
```text
Assets/_TinyGarden/Scripts/Editor/BuildReadinessChecker.cs
Assets/_TinyGarden/Scripts/Editor/TinyGardenBuildProfiles.cs
```

BuildReadinessChecker should validate required scenes, managers/prefabs, portrait orientation, no forbidden SDK folder names (`Ads`, `Analytics`, `Firebase`, `Facebook`, `Adjust`, `AppsFlyer`, etc.), audio warnings, SaveSystem existence, and child safety docs.

If actual builds cannot be run, document exact Unity Editor steps.

## Performance QA
Check/document lightweight app design, no heavy textures, no excessive particles, no unnecessary physics, no network calls, no high-cost update loops, smooth UI. Add profiling checklist for low/mid Android phone, iPhone/iPad simulator/device, scene load times, memory, touch responsiveness, audio toggles, and safe area.

## Release Candidate QA Flow
Create a manual test plan:
1. Fresh install/new save.
2. Boot → Main Menu → Garden.
3. Complete Color Match; verify Color Flower grown.
4. Complete Counting Fruits; verify Fruit Tree grown.
5. Complete Shape Sort; verify Shape Bush grown.
6. Verify animal unlock and sticker unlock.
7. Close/reopen; progress persists.
8. Test settings behind parental gate.
9. Toggle music/SFX/voice/sensory-safe mode.
10. Reset progress with confirmation.
11. Test common screen sizes.
12. Test offline/no internet.
13. Verify no external links/ads/IAP/analytics.

## Commercial Release Notes
Create/update `Assets/_TinyGarden/Docs/RELEASE_NOTES_0.1.0.md` with MVP features, learning goals, safety/privacy summary, known limitations, and post-MVP improvements.

## Post-MVP Recommendations
Document only: more gardens/seasons, animals, counting ranges, local-only parent dashboard, more voice languages, professional art/audio, and optional one-time premium purchase only if child-safe and parent-gated in future.

## Constraints
Do not add features, monetization, SDKs, internet/network requirements, signing keys, passwords, certificates, private credentials, or claims that store approval is guaranteed.

## Validation
Run tests if configured, run BuildReadinessChecker if implemented, confirm docs exist, confirm full MVP loop works, attempt Android build if possible, and document iOS export steps requiring Mac/Xcode.

## Final Response Required from Codex
Return mobile build readiness summary, changed files, Android instructions, iOS export instructions, store compliance findings, QA status, remaining manual tasks, and risks/assumptions.
