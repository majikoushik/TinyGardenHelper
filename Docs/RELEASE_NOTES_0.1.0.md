# Release Notes: v0.1.0 (MVP)

**Tiny Garden Helper**  
*Release Candidate 1*

## Overview
This is the foundational MVP release of Tiny Garden Helper, establishing the core game loop, save architecture, and child-safe guardrails. The app provides a calm, offline, ad-free environment for preschoolers to practice basic early-learning concepts.

## MVP Features
- **The Garden Hub**: A central hub where the child's progress is visually represented. 
- **Activity 1: Color Match**: Practice recognizing and sorting primary colors by dragging objects to their matching homes.
- **Activity 2: Counting Fruits**: Practice counting from 1 to 5 by placing apples into a basket. Features a self-correcting mechanic where apples can be removed if the child adds too many.
- **Activity 3: Shape Sort**: Practice spatial reasoning by dragging circle, square, and triangle blocks into silhouettes. Includes an intelligent "Progressive Hint" system to gently guide struggling players without punishing them.
- **Reward System**: Completing all three activities triggers a joyful celebration, unlocking the bouncing "Benny Bunny" animal friend and the "Garden Helper Star" sticker in the Sticker Book.

## Accessibility and Safety
- **Sensory Safe Mode**: A toggle designed for neurodivergent children that disables flashing, bouncy animations, and scene transitions to prevent overstimulation.
- **Robust Parental Gate**: The Settings menu (including the Reset Progress button) is protected by an adult-oriented math question.
- **Strict Data Privacy**: All progress is saved 100% locally to the device (`save.json`). 
- **Zero Distractions**: No ads, no analytics, no IAP, no internet requirement, no external links.

## Known Limitations (v0.1.0)
- **Placeholder Art/Audio**: The app currently utilizes colored geometric shapes and Unity's default UI text. Professional 2D sprites, animations, and voiceover clips are pending final commercial asset handoff (See `ART_DIRECTION.md` and `AUDIO_DIRECTION.md`).

## Post-MVP Roadmap Recommendations
- Implement final commercial 2D sprites and UI elements.
- Implement final voiceover, SFX, and ambient music.
- Add additional Mini-Games (e.g., Tracing letters, size sorting).
- Add seasonal garden themes (Winter, Autumn) to unlock after prolonged play.
- Integrate Unity Localization for multi-language voiceover support.
