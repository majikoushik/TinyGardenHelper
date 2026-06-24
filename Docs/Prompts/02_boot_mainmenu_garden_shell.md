# Prompt 02 — Boot, Main Menu, and Garden Scene Shell

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
You are implementing the first playable shell of a professional mobile Unity game. This step should produce a navigable experience: Boot → Main Menu → Garden. Do not implement the three mini-games yet.

## Task Objective
Create the initial scene flow, placeholder UI, safe area support, and a visually pleasant garden shell that can later connect to the mini-games.

## Required User Experience
The player should be able to:
1. Launch the game into a Boot scene.
2. Transition automatically to Main Menu.
3. Tap a large **Play** button.
4. Arrive in the Garden scene.
5. See three activity spots/plants: Color Flower, Fruit Tree, and Shape Bush.
6. Tap each spot and receive a friendly placeholder message such as “Coming next: Color Match!” until mini-games are implemented.
7. Access a Settings/Parents button that is visually present but protected by a parental gate placeholder.

## Scene Requirements

### Boot Scene
- Initialize persistent systems later.
- Show a short soft splash/loading panel.
- Transition to Main Menu after a brief non-blocking delay.
- Include `BootController`.
- If Unity scenes cannot be generated safely, create `TinyGardenSceneBuilder` editor script and document how to run it.

### Main Menu Scene
- First child-facing screen.
- Minimal reading.
- Big Play button.
- Friendly character/mascot placeholder.
- Parent/settings area behind gate.
- Portrait layout.
- Game title: “Tiny Garden Helper”.
- Soft background with sky/grass placeholders.

### Garden Scene
- Main hub.
- Three tappable plant spots with large hit areas.
- Each spot must have a label for developers/parents, but child interaction should rely on image/icon.
- Show unfinished seedling state initially.
- Show a bottom home/menu button.
- Show an empty animal area for Prompt 08.
- Add simple background elements: sun, cloud, grass, garden bed, butterflies as static placeholders.

## Visual Direction
Use placeholder art that still looks intentional: soft pastel colors, rounded UI, clear hierarchy, minimal text, and no clutter. If no art exists, create generated placeholder sprites or UI primitives and document limitations.

## Technical Requirements
Create or update:
```text
Assets/_TinyGarden/Scripts/Core/BootController.cs
Assets/_TinyGarden/Scripts/Core/SceneNames.cs
Assets/_TinyGarden/Scripts/Core/SimpleSceneLoader.cs
Assets/_TinyGarden/Scripts/UI/SafeAreaFitter.cs
Assets/_TinyGarden/Scripts/UI/MainMenuController.cs
Assets/_TinyGarden/Scripts/Garden/GardenSceneController.cs
Assets/_TinyGarden/Scripts/Garden/GardenActivitySpot.cs
Assets/_TinyGarden/Scripts/UI/ParentalGatePlaceholder.cs
Assets/_TinyGarden/Scripts/Editor/TinyGardenSceneBuilder.cs
```

## Interaction Requirements
- Buttons must work with touch/mouse in Editor.
- Activity spots should have clear tap feedback: scale pulse, sparkle placeholder, or soft bounce.
- Tapping incomplete activities should not navigate yet unless a placeholder scene exists.
- Parent/settings button should show a simple parental gate placeholder such as “For grown-ups: hold for 3 seconds” or “Solve 3 + 2”.

## Mobile Layout Requirements
- Canvas Scaler: `Scale With Screen Size`.
- Reference resolution: 1080×1920 or 720×1280.
- Add safe area handling for top/bottom UI.
- Avoid placing key buttons near screen edges.
- Use large touch targets, preferably 96 px+ at 1080×1920 reference resolution.

## Professional Polish Requirements
- Add soft transition placeholder between Main Menu and Garden if simple.
- Use fade-in or simple scale animation for Play button and garden spots.
- Make the garden feel alive with very small non-distracting motion.
- Keep animation reducible later through sensory-safe mode.

## Documentation Updates
Update README, `TECHNICAL_ARCHITECTURE.md`, `GAME_DESIGN.md`, and `QA_TEST_PLAN.md` with scene flow and testing steps.

## Constraints
- Do not implement mini-game mechanics yet.
- Do not add final art assumptions.
- Do not add third-party animation libraries.
- Avoid networking, analytics, ads, IAP.

## Validation
Check project compiles, Boot → MainMenu → Garden navigation works, parent/settings placeholder cannot lead to external links, UI works in portrait aspect ratios, and no missing script references exist if scenes/prefabs are generated.

## Final Response Required from Codex
Return what scene shell was created, how to generate/open scenes if an Editor script is required, files changed, how to test Boot → Main Menu → Garden, known limitations, and the next prompt name.
