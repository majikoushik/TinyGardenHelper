# Prompt 08 — Reward Celebration, Animal Unlock, and Sticker Book

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
You are implementing the emotional reward layer that makes the MVP feel delightful and commercially polished while staying age-appropriate and non-manipulative.

## Task Objective
Implement the full MVP reward flow:
1. Each completed activity grows a plant in the garden.
2. Completing all three activities unlocks one cute animal friend.
3. The child receives one sticker reward.
4. Garden state persists locally.
5. Reward UX is joyful but not addictive, monetized, or manipulative.

## Reward Philosophy
For children below 7, rewards should create competence, care, curiosity, and calm delight. Avoid coins, gems, loot boxes, streak pressure, daily login rewards, shop buttons, or currency loops.

## Required Features

### 1. Plant Growth Rewards
- ColorMatch completion grows Color Flower.
- CountingFruits completion grows Fruit Tree.
- ShapeSort completion grows Shape Bush.
Each grown plant should show a larger/grown placeholder, sparkle once on return to garden, and persist after restart.

### 2. Final Animal Friend Unlock
When all three activities are completed:
- Unlock one animal friend, e.g., **Benny Bunny** or **Mina Butterfly**.
- Show celebration panel: “A new garden friend!”
- Animal appears in garden with simple idle animation or soft bounce.
- Animal remains visible after restart.

### 3. Sticker Reward
Create a simple sticker book panel. Unlock one sticker: “Garden Helper Star”. Sticker book may show locked/unlocked placeholders. No marketplace, purchases, sharing, or external links.

### 4. Celebration UX
Short and skippable after a moment: sparkle burst, soft success sound hook, optional voice “You helped the garden grow!”, big visual reward, minimal text.

### 5. RewardSystem Integration
Update/finish:
```text
Assets/_TinyGarden/Scripts/Rewards/RewardSystem.cs
Assets/_TinyGarden/Scripts/Rewards/RewardDefinition.cs
Assets/_TinyGarden/Scripts/Rewards/RewardCelebrationController.cs
Assets/_TinyGarden/Scripts/Rewards/StickerBookController.cs
Assets/_TinyGarden/Scripts/Rewards/AnimalFriendController.cs
Assets/_TinyGarden/Scripts/Garden/GardenProgressPresenter.cs
```

## Data Requirements
Use reward IDs:
```text
plant_color_flower_grown
plant_fruit_tree_grown
plant_shape_bush_grown
animal_benny_bunny
sticker_garden_helper_star
```
Ensure rewards are idempotent: no repeated first-time unlock unless intentionally replayed; replays can show smaller celebrations.

## Garden Visual State Requirements
Garden displays seedlings, grown plants, empty/filled animal area, sticker book button/area, and clear non-intrusive progress indication.

## Commercial Polish Ideas
- Color Flower completion makes butterflies appear.
- Fruit Tree completion makes a bird appear.
- Shape Bush completion adds shape-flower decorations.
- Final animal enters from screen side and waves/hops.
- Garden glow animation when all plants complete.
- Tapping animal triggers a small friendly animation.
- Completed plants allow replay.

## Child Safety Requirements
No social sharing, external links, “buy more stickers”, “come tomorrow” pressure, or user identity/profile.

## Testing Requirements
Test one activity unlocks correct plant reward, all three unlock animal once, sticker unlocks once, rewards persist, and reset clears rewards.

## Documentation Updates
Update `GAME_DESIGN.md`, `TECHNICAL_ARCHITECTURE.md`, `QA_TEST_PLAN.md`, `CHILD_SAFETY_CHECKLIST.md`, and README.

## Constraints
No monetization, daily rewards/streaks, multiple animals beyond one MVP animal, or social sharing.

## Validation
Project compiles; fresh save full loop works; each plant grows; animal unlocks after all three; sticker unlocks; restart retains garden; reset clears state.

## Final Response Required from Codex
Return reward implementation summary, changed files, full MVP test flow, idempotent reward handling, limitations, and next prompt name.
