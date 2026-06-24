# Tiny Garden Helper

Tiny Garden Helper is a planned Unity 2D educational mobile game for Android and iOS. The MVP is a portrait-mode, offline-first garden game for children ages 3-7, focused on three simple activities:

- Color matching
- Counting fruits from 1 to 5
- Shape sorting with circle, square, and triangle

Children help three magical garden plants grow by completing one activity per plant. When all three plants grow, they unlock a cute animal friend. The experience should be gentle, cheerful, and age-appropriate: no ads, no in-app purchases, no account system, no chat, no timers, no punishment screens, and no child-accessible external links.

## Current Repository Status

This repository is currently at startup-planning stage. No Unity project files have been generated yet. The next implementation step is to create a Unity 2D project in this repository and add the folder structure, scenes, prefabs, ScriptableObject definitions, and placeholder assets described in the docs.

## Documentation

- [Game Design](Docs/GAME_DESIGN.md)
- [Technical Architecture](Docs/TECHNICAL_ARCHITECTURE.md)
- [Mobile Build Checklist](Docs/MOBILE_BUILD_CHECKLIST.md)
- [Child Safety Checklist](Docs/CHILD_SAFETY_CHECKLIST.md)
- [Agent Notes](AGENTS.md)

## MVP Scope

The MVP should include:

- Splash flow
- Main menu
- Garden scene with three plants
- Three activity panels or scenes
- Plant growth reward feedback
- Final animal friend unlock
- Settings with audio toggles and protected parent area
- Local progress saving
- Placeholder art and audio hooks that can be replaced later

The MVP should not include analytics, ads, networking, cloud saves, purchases, leaderboards, accounts, user-generated content, or social features.

## Target Platforms

- Android phones and tablets
- iPhone and iPad
- Portrait orientation only
- Offline-first
- Lightweight 2D rendering suitable for mid-range devices

## Recommended Unity Baseline

- Unity 2022.3 LTS or newer LTS
- 2D Core project template
- C# scripts
- Built-in Unity UI or UI Toolkit only if the project standardizes on it early
- No third-party SDKs for MVP unless a specific need is approved

## Next Implementation Steps

1. Create the Unity 2D project in this repository.
2. Add the folder structure from `Docs/TECHNICAL_ARCHITECTURE.md`.
3. Create placeholder sprites and simple UI prefabs.
4. Implement boot, scene loading, save, audio, reward, and activity orchestration systems.
5. Build the garden loop with three plants and local progress.
6. Add the three mini activities one at a time.
7. Test on multiple screen sizes before replacing placeholders with final art.
