# Tiny Garden Helper

## Game Concept
Tiny Garden Helper is a gentle, colorful, and offline-first educational Unity 2D mobile game. Children help a magical garden grow by completing simple, calm mini-games. Completing all activities unlocks a cute animal friend. 

## Target Audience
- **Age:** 3-7 years (kindergarten focused).
- **Interaction:** Touch-first, minimal reading required, gentle feedback loops, no pressure or timers.

## MVP Scope
- **Scenes:** Boot, Splash, Main Menu, Garden (hub), Settings.
- **Mini-Games:**
  1. Color Match
  2. Counting Fruits (1-5)
  3. Shape Sort
- **Rewards:** Plant growth, sparkles, sticker/animal friend unlock.

## Current Project Status
The repository contains the foundational UI architecture, core persistent managers, a privacy-compliant local Save System, and all three fully playable MVP Mini-Games: **Color Match**, **Counting Fruits**, and **Shape Sort**. 
The game features a resilient drag-and-drop framework tailored for children.
Completing all three activities unlocks the final MVP reward flow: **Benny Bunny** the animal friend, and the **Garden Helper Star** sticker in the Sticker Book!
The MVP is now feature-complete!

## Opening the Project
1. Open **Unity Hub**.
2. Select **Add project from disk** and choose the `TinyGardenHelper` folder.
3. Recommended Unity version: **Unity 2022.3 LTS** (or newer LTS).
4. Use the **2D Core** project template if creating from scratch.

## Generating and Running the Scenes
1. Open the project in the Unity Editor.
2. From the top menu, click **Tiny Garden > Build Initial Scenes**. This will safely generate the Canvas, UI, and Scene structures.
3. Open `Assets/_TinyGarden/Scenes/Boot.unity`.
4. Press **Play** in the Unity Editor to flow through to the Main Menu and Garden scenes.

## Local Testing
- Ensure the Unity Editor is set to a portrait mobile resolution (e.g., 1080x1920).
- Test touch interactions using the Device Simulator if available, or mouse clicks.

## Android / iOS Build Notes
- **Android:** Target ARM64. Ensure offline capability and handle the back button safely. Output as AAB for store or APK for local testing.
- **iOS:** Ensure Safe Area components are used to handle notches. iOS builds generate an Xcode project requiring a Mac to compile.

## Child Safety Policy Summary
- **No Ads or IAP** in the MVP.
- **No Analytics, Tracking, or Data Collection.**
- **No External Links** accessible to the child.
- **Parental Gate** required to access the Settings/Reset progress screen.
- **Completely Offline:** The game works 100% without an internet connection.
