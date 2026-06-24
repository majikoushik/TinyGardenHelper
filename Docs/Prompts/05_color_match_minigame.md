# Prompt 05 — Color Match Mini-Game

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
You are implementing the first real learning activity. This must feel simple, joyful, forgiving, and polished for children aged 3–7.

## Task Objective
Implement the **Color Match** mini-game. The child matches colored garden objects to the correct target. Completing the activity marks ColorMatch as complete in local save and returns to the garden with a plant-growth reward.

## Learning Goal
The child practices recognizing and matching basic colors. MVP colors: Red, Blue, Yellow. Optional Green only if easy and not scope-expanding.

## Gameplay Design
Scene/panel: `ColorMatch.unity` or generated mini-game panel.

Child-facing flow:
1. Friendly voice/text prompt: “Match the colors!”
2. Show three flowers or pots: red, blue, yellow.
3. Show three draggable items: butterflies, seeds, or watering drops in matching colors.
4. Child drags each colored object to the matching target.
5. Correct match: object snaps into place, sparkles, soft success sound.
6. Incorrect match: object gently returns to start; target gives a subtle hint pulse; no negative sound.
7. After all matches are correct: celebration panel, plant growth reward, return to garden.

## Interaction Requirements
- Support touch and mouse drag in Editor.
- Use large draggable objects and forgiving drop zones.
- Do not require pixel-perfect placement.
- Use visual hints after repeated incorrect attempts.
- No timer, score screen, or fail state.

## Kid-Safe Feedback Rules
Correct feedback: soft sparkle, gentle bounce, success audio hook, optional voice “Great matching!”

Incorrect feedback: never say “wrong”; use “Try again!” behavior, object returns softly, correct target pulses/glows, and after two retries show a stronger hint.

## Required Scripts
Create or update:
```text
Assets/_TinyGarden/Scripts/MiniGames/ColorMatch/ColorMatchController.cs
Assets/_TinyGarden/Scripts/MiniGames/ColorMatch/ColorDraggableItem.cs
Assets/_TinyGarden/Scripts/MiniGames/ColorMatch/ColorDropTarget.cs
Assets/_TinyGarden/Scripts/MiniGames/ColorMatch/ColorMatchDefinition.cs
Assets/_TinyGarden/Scripts/MiniGames/ColorMatch/ColorMatchItemData.cs
Assets/_TinyGarden/Scripts/MiniGames/Shared/DragItemBase.cs
Assets/_TinyGarden/Scripts/MiniGames/Shared/DropTargetBase.cs
```
Reuse and improve shared drag/drop base classes if they already exist.

## Data Design
Use ScriptableObject or serialized config with color id, display color, item sprite placeholder, target sprite placeholder, and voice prompt key placeholder.

## Scene/Prefab Requirements
Create prefabs under `Assets/_TinyGarden/Prefabs/MiniGames/ColorMatch/`: `ColorMatchRoot`, `ColorDraggableItem`, `ColorDropTarget`, and `CelebrationPanel`. If direct prefab generation is hard, create `TinyGardenMiniGameBuilder.cs` to generate the placeholder mini-game scene.

## Visual Requirements
Use flower/pot targets and butterfly/seed/waterdrop draggable items. Add child-friendly prompt icon and soft garden background. Do not rely only on written color names.

## Completion Flow
On completion, call mini-game result with `ActivityId.ColorMatch`, save progress, grant `PlantGrowth` for Color Flower, show short celebration, return to Garden, and show Color Flower grown.

## Commercial Polish Ideas
- Randomize item starting positions while preserving predictability.
- Add tiny butterfly flap animation.
- Add hint sparkle on correct target after retries.
- Add optional voice prompt: “Can you find the red flower?”
- Use a three-dot progress indicator.

## Accessibility/Sensory Requirements
Do not depend only on color: pair red/blue/yellow with simple patterns/icons such as heart/drop/star. Sensory-safe mode should reduce sparkle and bounce intensity. Avoid flashing.

## Testing Requirements
Test correct match, incorrect retry behavior, all matches complete activity, save data update, re-entering completed activity, and no duplicate reward unless intended.

## Documentation Updates
Update `GAME_DESIGN.md`, `TECHNICAL_ARCHITECTURE.md`, `QA_TEST_PLAN.md`, and README.

## Constraints
Do not implement Counting or Shape Sort yet. No final paid art, ads, IAP, analytics, networking, harsh fail sounds, or fail screens.

## Validation
Project compiles; ColorMatch opens from Garden/manual test; drag/drop works; completion persists; returning to Garden shows grown Color Flower; portrait/safe area remain valid.

## Final Response Required from Codex
Return implementation summary, changed files, test steps, retry/hint behavior, limitations, and next prompt name.
