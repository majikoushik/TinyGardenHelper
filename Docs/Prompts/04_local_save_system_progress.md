# Prompt 04 — Local Save System and Progress Model

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
You are implementing safe, offline-first local progress for a children’s mobile game. The save system must be simple, transparent, and privacy-preserving.

## Task Objective
Implement a local-only save system that records garden progress, completed activities, unlocked stickers, animal friend unlock state, and simple settings. It must not collect personal data.

## Privacy Requirements
The save data must not include child name, age, birth date, location, email, device identifiers, advertising identifiers, analytics identifiers, cloud identifiers, or any personally identifiable information. It may include only anonymous local game state.

## Required Data Model
Create:
```text
Assets/_TinyGarden/Scripts/SaveSystem/GameSaveData.cs
Assets/_TinyGarden/Scripts/SaveSystem/ActivityProgressData.cs
Assets/_TinyGarden/Scripts/SaveSystem/SettingsData.cs
Assets/_TinyGarden/Scripts/SaveSystem/ISaveSystem.cs
Assets/_TinyGarden/Scripts/SaveSystem/LocalJsonSaveSystem.cs
Assets/_TinyGarden/Scripts/SaveSystem/SaveSystemService.cs
```

Suggested model:
```csharp
public sealed class GameSaveData
{
    public int SaveVersion;
    public bool HasCompletedIntro;
    public List<ActivityProgressData> Activities;
    public List<string> UnlockedRewardIds;
    public bool AnimalFriendUnlocked;
    public SettingsData Settings;
}
```

Suggested activity progress:
```csharp
public sealed class ActivityProgressData
{
    public string ActivityId;
    public bool Completed;
    public int SuccessfulCompletions;
    public int GentleRetryCount;
}
```

Suggested settings:
```csharp
public sealed class SettingsData
{
    public bool MusicEnabled;
    public bool SfxEnabled;
    public bool VoiceEnabled;
    public bool SensorySafeModeEnabled;
}
```

## SaveSystem Requirements
- Use Unity persistent data path.
- Save JSON locally.
- Load safely if file missing.
- Handle corrupted JSON by backing it up and starting a default save.
- Version save data for future migrations.
- Provide `Load()`, `Save(GameSaveData data)`, `ResetProgress()`, `MarkActivityCompleted(ActivityId id)`, `UnlockReward(string rewardId)`, and setting update methods.
- Do not block the UI for long operations; small synchronous JSON file operations are acceptable.
- Avoid encryption because no PII is stored, unless project conventions require it.

## Progress Model Requirements
- Three activities: ColorMatch, CountingFruits, ShapeSort.
- Each activity can be incomplete or completed.
- Plant growth in Garden should reflect completion.
- Animal friend unlocks when all three activities are completed.
- Sticker reward can be unlocked after final completion.
- Reset progress should be behind parental gate later.

## Integration Requirements
- Connect SaveSystem to `GameManager`.
- Update Garden scene shell to reflect saved activity progress.
- If an activity is completed, its plant should appear grown using placeholder visual state.
- If all activities are completed, show animal placeholder/unlocked state.
- Add temporary developer controls only if hidden or Editor-only.

## Settings Requirements
Prepare settings for Music, SFX, Voice, and Sensory-Safe Mode. Do not build a full settings screen unless simple; make it possible for Prompt 09 to finish polish.

## Testing Requirements
Add EditMode tests if possible for default saves, completion persistence, all-complete logic, corrupted-save fallback, and reset behavior. Abstract file path provider if needed.

## Documentation Updates
Update `TECHNICAL_ARCHITECTURE.md`, `CHILD_SAFETY_CHECKLIST.md`, `QA_TEST_PLAN.md`, and README.

## Constraints
No cloud save, accounts, analytics, personal data, internet permission, or mini-game gameplay.

## Validation
Project compiles, save file is created, progress persists after scene reload/restart in Editor where practical, Garden visual state updates, and reset works only through code or parent-gated flow.

## Final Response Required from Codex
Return implementation summary, save fields/privacy explanation, changed files, save/load/reset test steps, risks/assumptions, and next prompt name.
