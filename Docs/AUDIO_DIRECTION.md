# Audio Direction

## Vision
Audio in Tiny Garden Helper should be gentle, encouraging, and soothing. It provides crucial feedback for children who may not be able to read yet, while ensuring the experience never feels overwhelming, startling, or punitive.

## Core Principles
- **Soft and Short**: Sound effects should be brief and use soft instruments (e.g., marimbas, bells, soft synths).
- **Encouraging**: Voice lines and feedback sounds should always be positive. 
- **No Startling Noises**: Avoid loud crashes, buzzers, or sudden loud volume spikes.
- **Total Control**: All audio must be controllable (on/off) via the settings menu.

## MVP Audio Hooks

### UI Sound Effects
- **Button Tap**: A soft, pleasant click or pop.
- **Menu Transition**: A gentle swoosh.

### Gameplay Sound Effects
- **Activity Success**: A cheerful, bright chime or short melodic flourish.
- **Activity Incorrect**: A very gentle, non-punitive "boop" or soft bounce sound. It must *not* sound like a buzzer or failure.
- **Plant Growth**: Magical sparkles or a rising chime sequence.
- **Animal Unlock**: A happy, soft celebratory sound or cute animal noise.

### Voice Lines (Optional but Recommended)
- "Try again!" (Gentle and encouraging)
- "You did it!" / "Great job!"
- "How many?" / "Match the color!" 

### Music
- **Garden Background Music**: A single looping track that is calm, cheerful, and ambient. It should sit quietly in the background and not demand attention.

## Placeholder Audio Strategy
Use short, simple synthesized tones or placeholder voice recordings to map out the audio logic before final assets are integrated.
- `sfx_ui_tap`
- `sfx_game_success`
- `sfx_game_try_again`
- `music_bg_garden`
