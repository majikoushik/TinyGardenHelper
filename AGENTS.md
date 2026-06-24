# Tiny Garden Helper Agent Guide

This repository is for a small, polished Unity 2D mobile game for children ages 3-7. Keep all implementation decisions aligned with the MVP: simple, gentle, offline-first, and child-safe.

## Working Principles

- Build a playable MVP before adding breadth.
- Prefer simple Unity-native systems over third-party packages.
- Keep scripts modular but avoid abstract frameworks that the MVP does not need.
- Use ScriptableObjects for reusable configuration, activity data, reward data, and audio references.
- Keep touch targets large and interactions obvious.
- Use encouraging language: "Try again!" instead of "Wrong!".
- Do not add ads, analytics, networking, accounts, purchases, chat, social sharing, or external links.
- Protect any parent/settings-only area with a simple parental gate.

## Repository Conventions

Recommended Unity root layout after project creation:

- `Assets/_TinyGardenHelper/` for all game-specific content.
- `Assets/_TinyGardenHelper/Scripts/` for runtime C#.
- `Assets/_TinyGardenHelper/ScriptableObjects/` for authored data.
- `Assets/_TinyGardenHelper/Scenes/` for Unity scenes.
- `Assets/_TinyGardenHelper/Prefabs/` for reusable objects.
- `Assets/_TinyGardenHelper/Art/Placeholders/` for temporary assets.
- `Assets/_TinyGardenHelper/Audio/Placeholders/` for temporary audio.
- `Docs/` for project planning and production documentation.

## Code Style

- Use clear, small MonoBehaviours with explicit responsibilities.
- Keep data serializable and inspectable in Unity.
- Avoid static global state except for tiny bootstrap-safe service access when needed.
- Use events for activity completion, reward unlocks, and UI updates.
- Keep save data versioned.
- Keep comments concise and only where they clarify non-obvious behavior.

## Testing Expectations

Before calling a feature complete, verify:

- It works with touch input, not only mouse.
- UI fits portrait phone and tablet aspect ratios.
- Safe areas are respected.
- Audio toggles work.
- Progress saves and reloads.
- The child can recover from incorrect choices without punishment.
- The game remains playable without internet.
