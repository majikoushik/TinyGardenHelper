# Prompt 01 — Project Structure, Documentation, and Product Foundation

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
You are a senior Unity mobile game architect and lead developer. Your job in this step is to turn the repository into a clean, commercial-ready Unity project foundation for Tiny Garden Helper. Do **not** implement the full game yet. This step is about architecture, documentation, folder structure, conventions, and build-readiness foundation.

## Task Objective
Inspect the existing repository/project. If this is already a Unity project, preserve existing valid Unity metadata and project settings. If it is not yet a Unity project, create a Unity-compatible repository scaffold as far as possible through text/code files and document any Unity Editor actions needed.

Create a strong baseline that future Codex prompts can build on safely.

## Required Deliverables

### 1. Folder Structure
Create or verify this structure:

```text
Assets/
  _TinyGarden/
    Art/
      Characters/
      Garden/
      UI/
      Rewards/
      Placeholders/
    Audio/
      Music/
      SFX/
      Voice/
    Prefabs/
      Core/
      UI/
      Garden/
      MiniGames/
      Rewards/
    Scenes/
      Boot.unity
      MainMenu.unity
      Garden.unity
      MiniGames/
        ColorMatch.unity
        CountingFruits.unity
        ShapeSort.unity
    Scripts/
      Core/
      UI/
      Audio/
      SaveSystem/
      Rewards/
      MiniGames/
        Shared/
        ColorMatch/
        CountingFruits/
        ShapeSort/
      Platform/
      Editor/
      Utilities/
    ScriptableObjects/
      Activities/
      Rewards/
      Audio/
      Themes/
    Tests/
      EditMode/
      PlayMode/
    Docs/
ProjectSettings/
Packages/
```

If Unity `.unity` scenes cannot be safely generated directly, create a documented plan and add Editor utility scripts in later steps to generate them.

### 2. Documentation Files
Create or update:

```text
README.md
AGENTS.md
Assets/_TinyGarden/Docs/GAME_DESIGN.md
Assets/_TinyGarden/Docs/TECHNICAL_ARCHITECTURE.md
Assets/_TinyGarden/Docs/MOBILE_BUILD_CHECKLIST.md
Assets/_TinyGarden/Docs/CHILD_SAFETY_CHECKLIST.md
Assets/_TinyGarden/Docs/ART_DIRECTION.md
Assets/_TinyGarden/Docs/AUDIO_DIRECTION.md
Assets/_TinyGarden/Docs/QA_TEST_PLAN.md
Assets/_TinyGarden/Docs/ROADMAP.md
```

### 3. README Requirements
The README must explain:
- Game concept
- Target audience
- MVP scope
- Current project status
- How to open the project in Unity
- Recommended Unity version, if detectable
- How to run scenes
- How to test locally
- Android/iOS build notes
- Child safety policy summary

### 4. AGENTS.md Requirements
Create a Codex-friendly AGENTS.md with:
- Project vision
- Audience rules
- Folder conventions
- C# coding conventions
- Unity scene/prefab modification rules
- Documentation update rules
- Prohibited features
- Build/test verification expectations
- “Do not overbuild” instruction
- How to respond after each implementation step

### 5. Game Design Document Requirements
`GAME_DESIGN.md` must include:
- Product pillars: safe, simple, joyful, educational, polished
- Core gameplay loop
- Three mini-games
- Reward model
- Progression model
- Accessibility considerations
- Anti-patterns to avoid
- Future expansion ideas clearly marked as post-MVP

### 6. Technical Architecture Document Requirements
`TECHNICAL_ARCHITECTURE.md` must define:
- Scene architecture
- Manager architecture
- Save data model
- Reward flow
- Mini-game base interfaces/classes
- Audio architecture
- UI architecture
- Safe area strategy
- Platform build strategy
- Testing strategy

### 7. Child Safety Checklist Requirements
Include a checklist that future prompts must satisfy:
- no ads
- no IAP
- no accounts
- no analytics/tracking
- no external links accessible to children
- no social/multiplayer/chat/UGC
- no manipulative retention
- no negative feedback loops
- parental gate for parent/settings area
- offline-first design

### 8. Product Innovation Notes
Add a section called **Commercial Delight Ideas** in GAME_DESIGN.md. Include ideas that can make the MVP feel premium without expanding scope too much:
- Garden reacts softly to progress: butterflies, clouds, sunshine rays, birds.
- “Kindness feedback”: even incorrect attempts animate gently and guide the child.
- Sticker book unlock after completing all three games.
- Animal friend does a short celebration animation.
- Parent-facing “Learning Goals” screen behind parental gate.
- Voice prompt placeholders for non-reading children.
- Sensory-safe mode: reduce animation intensity and sound volume.

## Constraints
- Do not add third-party SDKs.
- Do not add monetization.
- Do not add networking.
- Do not implement all gameplay in this step.
- Keep files organized under `Assets/_TinyGarden/` when possible.

## Validation
Run whatever local checks are available. At minimum:
- Confirm created files exist.
- Confirm no broken references in text files.
- If Unity project files exist, avoid corrupting `.meta` files.
- If tests are available, run them or document why not.

## Final Response Required from Codex
Return:
1. Summary of created/updated files.
2. Architecture decisions made.
3. Any manual Unity Editor steps needed.
4. Risks/assumptions.
5. Next recommended prompt: `Prompt 02 — Boot, Main Menu, and Garden Scene Shell`.
