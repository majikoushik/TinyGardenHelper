# Prompt 07 — Shape Sort Mini-Game

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
You are implementing the third learning activity: basic shape recognition and categorization for children aged 3–7.

## Task Objective
Implement the **Shape Sort** mini-game. The child drags circle, square, and triangle objects into matching homes/slots. Completing the activity marks ShapeSort as complete in local save and returns to the garden with a plant-growth reward.

## Learning Goal
The child recognizes and sorts basic shapes: Circle, Square, and Triangle.

## Gameplay Design
Scene/panel: `ShapeSort.unity` or generated mini-game panel.

Child-facing flow:
1. Friendly prompt: “Find each shape’s home!”
2. Show three large target homes/slots: circle, square, triangle.
3. Show three draggable shape characters or blocks.
4. Child drags each shape to the matching target.
5. Correct: shape snaps in, target glows, friendly sound.
6. Incorrect: shape gently returns and matching target pulses as hint.
7. Complete all three to finish activity.
8. Celebration, save progress, plant grows, return to garden.

## Interaction Requirements
- Touch/mouse drag.
- Large shapes and forgiving drop zones.
- No timer, score, or fail state.
- Objects should be recognizable even if colors are similar.

## Visual Requirements
Make shapes into simple tiny characters with eyes/smile if possible. Target homes can be garden holes, toy boxes, or leafy frames. Use shape outlines in target slots. Avoid visually busy backgrounds.

## Required Scripts
Create or update:
```text
Assets/_TinyGarden/Scripts/MiniGames/ShapeSort/ShapeSortController.cs
Assets/_TinyGarden/Scripts/MiniGames/ShapeSort/ShapeDraggableItem.cs
Assets/_TinyGarden/Scripts/MiniGames/ShapeSort/ShapeDropTarget.cs
Assets/_TinyGarden/Scripts/MiniGames/ShapeSort/ShapeSortDefinition.cs
Assets/_TinyGarden/Scripts/MiniGames/ShapeSort/ShapeItemData.cs
Assets/_TinyGarden/Scripts/MiniGames/ShapeSort/ShapeType.cs
```
Reuse shared drag/drop infrastructure from previous mini-games.

## Data Design
Represent shapes with a `ShapeType` enum: Circle, Square, Triangle. Include display name for parent/docs only, sprite placeholder, target placeholder, optional voice key, and optional animation key.

## Completion Flow
On all shapes sorted, call result with `ActivityId.ShapeSort`, save progress, grant `PlantGrowth` for Shape Bush, show celebration, return to Garden, and show Shape Bush grown.

## Gentle Hint Behavior
First incorrect attempt returns item softly. Second incorrect attempt pulses the correct target. Third delay/attempt shows a faint dotted path or sparkle trail. Do not add a big “wrong” icon or buzzer.

## Commercial Polish Ideas
- Circle rolls/bounces lightly.
- Square hops once.
- Triangle wiggles gently.
- Shape entry plays a soft chime.
- Shape Bush blooms with mixed shape flowers.
- Voice placeholders: “Circle!”, “Square!”, “Triangle!”

## Accessibility/Sensory Requirements
Do not depend on color; geometry must be enough. Use slow predictable motion. Sensory-safe mode reduces movement intensity. No flashing.

## Testing Requirements
Test matching shape, non-matching retry, all three completions, save data update, and no duplicate final reward on replay unless intended.

## Documentation Updates
Update `GAME_DESIGN.md`, `TECHNICAL_ARCHITECTURE.md`, `QA_TEST_PLAN.md`, and README.

## Constraints
Do not implement final animal/sticker reward yet. No ads, IAP, analytics, networking, fail screens, timers, or harsh feedback.

## Validation
Project compiles; ShapeSort opens from Garden; drag/drop works; completion persists; returning to Garden shows Shape Bush grown; all three mini-games can be completed independently.

## Final Response Required from Codex
Return implementation summary, changed files, sorting/retry test steps, shared drag/drop refactors, limitations, and next prompt name.
