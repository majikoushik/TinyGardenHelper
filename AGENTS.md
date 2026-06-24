# Tiny Garden Helper Agent Guide

## Project Vision
Tiny Garden Helper is a calm, colorful, reward-based educational game for children aged 3-7. The goal is to make a small game feel polished, safe, delightful, and commercially credible.

## Audience Rules
- Target age: 3-7 years.
- Use large touch targets, simple gestures, and minimal text. Do not rely on reading.
- No punishment, failure screens, scary content, timers, or pressure.
- Give gentle feedback ("Try again!" instead of "Wrong!").
- Keep sessions short and rewarding.

## Folder Conventions
All game-specific content should be inside `Assets/_TinyGarden/`.
Key subfolders:
- `Art/` (Characters, Garden, UI, Rewards, Placeholders)
- `Audio/` (Music, SFX, Voice)
- `Prefabs/` (Core, UI, Garden, MiniGames, Rewards)
- `Scenes/`
- `Scripts/`
- `ScriptableObjects/`
- `Docs/`

## C# Coding Conventions
- Use modular systems and avoid unnecessary abstract frameworks.
- Prefer ScriptableObjects for configuration, activity data, and audio references.
- Keep components small and focused.
- Use events for decoupled interactions (e.g., activity completion, UI updates).

## Unity Scene/Prefab Modification Rules
- Use prefabs for reusable elements.
- Keep scene hierarchy clean and logical.
- If modifying scenes or prefabs programmatically is not feasible, clearly document the required manual Editor steps for the user.

## Documentation Update Rules
- Keep `README.md` and documents in `Docs/` up to date if architecture or systems change.
- Never delete the architectural documents; augment them as the project evolves.

## Prohibited Features
- No ads, IAP, accounts, login systems, chat, UGC, or social sharing.
- No analytics or third-party SDKs unless explicitly approved.
- No external links available to children.

## Build/Test Verification Expectations
- Always verify touch interactions, safe areas, and portrait orientation handling.
- Verify progress saving and offline play functionality.
- Ensure audio toggles work correctly.

## Do Not Overbuild
Focus strictly on the MVP scope: Boot, Main Menu, Garden hub, and three mini-games. Do not add complex frameworks or procedural generation that the MVP does not need.

## How to Respond After Each Implementation Step
1. Summary of created/updated files.
2. Architecture decisions made.
3. Any manual Unity Editor steps needed.
4. Risks/assumptions.
5. Provide a verification plan.
6. Mention the next recommended prompt.
