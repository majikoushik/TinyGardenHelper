# Prompt 06 — Counting Fruits Mini-Game

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
You are implementing the second learning activity: an early numeracy game for kindergarten children. It must be tactile, visual, and forgiving.

## Task Objective
Implement the **Counting Fruits** mini-game. The child places the requested number of fruits into a basket. Completing the activity marks CountingFruits as complete in local save and returns to the garden with a plant-growth reward.

## Learning Goal
The child practices counting quantities from **1 to 5**.

## Gameplay Design
Scene/panel: `CountingFruits.unity` or generated mini-game panel.

Child-facing flow:
1. Show a friendly prompt: “Put 3 apples in the basket.”
2. Display the target number visually as a large numeral and dots/finger-count icons.
3. Show a tree or tray with draggable fruits.
4. Child drags fruits into a basket.
5. Basket count updates visually: filled dots, gentle bounce, number display.
6. If too few, prompt gently: “Add more.”
7. If too many, do not punish; allow child to remove a fruit or gently return extra fruit.
8. When basket count equals target, complete automatically after a short delay or via a large friendly check action.
9. Celebration, save progress, return to garden.

## MVP Counting Values
Use three rounds: count 1, count 3, and count 5. Alternative: random targets 1–5 across 3 rounds, but make testing deterministic.

## Interaction Requirements
- Touch/mouse drag fruits into basket.
- Large fruit objects and forgiving basket drop zone.
- Child can remove fruit from basket by dragging it out or tapping it, if feasible.
- No timer, score, fail state, or text-only instruction dependency.

## Visual Requirements
Use one fruit type for clarity, e.g., apples. Show target number with a large numeral, matching dots, optional basket slots, and actual fruits. Use a sunny orchard/garden background and playful fruit bounce animation.

## Required Scripts
Create or update:
```text
Assets/_TinyGarden/Scripts/MiniGames/CountingFruits/CountingFruitsController.cs
Assets/_TinyGarden/Scripts/MiniGames/CountingFruits/FruitDraggableItem.cs
Assets/_TinyGarden/Scripts/MiniGames/CountingFruits/FruitBasketDropZone.cs
Assets/_TinyGarden/Scripts/MiniGames/CountingFruits/CountingRoundDefinition.cs
Assets/_TinyGarden/Scripts/MiniGames/CountingFruits/CountingFruitsDefinition.cs
Assets/_TinyGarden/Scripts/MiniGames/CountingFruits/CountVisualIndicator.cs
```
Reuse shared drag/drop infrastructure from Color Match where possible.

## Round Logic Requirements
For each round, initialize target count, spawn enough fruits, track fruits inside basket, update visual indicator, complete when count equals target, celebrate briefly, and proceed. After all rounds, complete the activity.

## Gentle Error Handling
If too many fruits are added, do not show failure. Basket gently wiggles, extra fruit returns or prompt says “Let’s keep 3!”, and target dots highlight. If child pauses, show a subtle hint: one fruit glows and basket pulses.

## Completion Flow
On all rounds complete, call result with `ActivityId.CountingFruits`, save progress, grant `PlantGrowth` for Fruit Tree, show celebration, return to Garden, and show Fruit Tree grown.

## Commercial Polish Ideas
- Soft plop sound when fruit enters basket.
- Basket fills visually with fruit icons.
- Large numeral smiles/bounces on correct count.
- Tiny bird lands on tree after count 5.
- Voice placeholder: “One, two, three!” as fruits are added.
- Optional haptics only if safe and easy to disable.

## Accessibility/Sensory Requirements
Do not rely only on numerals; use dots and actual fruits. Voice prompts optional. Sensory-safe mode reduces bounce/sparkles. Avoid rapid repeated sounds.

## Testing Requirements
Test basket count updates, target count completes round, too many fruits do not complete incorrectly, three rounds complete activity, and save data records completion.

## Documentation Updates
Update `GAME_DESIGN.md`, `TECHNICAL_ARCHITECTURE.md`, `QA_TEST_PLAN.md`, and README.

## Constraints
Do not implement Shape Sort yet. No ads, IAP, analytics, networking, fail screens, score pressure, or timers. Keep count range 1–5 only.

## Validation
Project compiles; CountingFruits opens from Garden; dragging works; correct count completes each round; activity persists; returning to Garden shows Fruit Tree grown.

## Final Response Required from Codex
Return implementation summary, changed files, test steps, too-many/too-few handling, limitations, and next prompt name.
