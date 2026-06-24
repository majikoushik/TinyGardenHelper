# Tiny Garden Helper Game Design

## Vision

Tiny Garden Helper is a gentle educational garden game for children ages 3-7. Children help three magical plants grow by completing simple learning activities. The game should feel warm, safe, and responsive, with cheerful visual rewards and no pressure.

## Audience

- Primary: ages 3-7
- Reading level: minimal to none
- Interaction style: tap, drag, match, count
- Session length: 3-8 minutes
- Emotional tone: encouraging, calm, playful

## Core Loop

1. Child enters the garden.
2. Three small plants are visible.
3. Child taps a plant.
4. A mini activity opens.
5. Child completes the activity.
6. The plant grows with sparkles, sound, and an encouraging voice line.
7. After all three plants grow, a cute animal friend appears.
8. Child can revisit activities or reset progress through a protected parent/settings area.

## MVP Activities

### Color Matching

Goal: Match an object to the same color.

MVP interaction:

- Show one target color at the top or center.
- Show 2-3 large answer choices.
- Child taps the matching color.
- If the child taps another color, give a gentle prompt such as "Try again!" and keep the activity active.

Recommended colors:

- Red
- Blue
- Yellow
- Green

### Counting Fruits 1-5

Goal: Count fruits and choose the matching number.

MVP interaction:

- Show 1-5 fruit sprites.
- Show 2-3 large number buttons.
- Child taps the number that matches the fruit count.
- Use visual count feedback such as small bounce animations on fruits.

Reading avoidance:

- Pair numbers with dots or fruit icons where useful.
- Add optional voice line hook: "How many apples?"

### Shape Sorting

Goal: Sort circle, square, and triangle into matching slots.

MVP interaction:

- Show 3 shape objects.
- Show 3 matching silhouette slots.
- Child drags or taps a shape into the matching slot.
- On correct placement, snap the shape into the slot.
- On incorrect placement, return the shape gently and say "Try again!".

## Rewards

Rewards should be visual, emotional, and age-appropriate.

MVP reward types:

- Plant growth stage change
- Sparkle burst
- Gentle success sound
- Encouraging voice line
- Sticker-style reward badge
- Final animal friend unlock

Avoid:

- Currency
- Streak pressure
- Loss states
- Failure screens
- Timed challenges
- Competitive rankings

## Scenes And Flow

1. Splash
2. Main Menu
3. Garden
4. Activity UI for color, counting, and shape sorting
5. Reward celebration
6. Settings

The mini activities may be separate scenes or modal panels inside the Garden scene. For the MVP, panels inside the Garden scene are acceptable if they reduce loading complexity.

## Visual Direction

- Soft cartoon style
- Rounded shapes
- Friendly plants and animals
- Bright but not harsh colors
- Clear foreground/background contrast
- No scary characters, sharp danger motifs, or sudden aggressive effects

Placeholder art should be simple colored sprites so the game is playable before final art arrives.

## Audio Direction

MVP audio hooks:

- Button tap
- Activity success
- Gentle try-again prompt
- Plant growth sparkle
- Animal unlock
- Garden background music

Audio should be soft, short, and non-startling. All audio must be controllable in settings.

## MVP Non-Goals

- Ads
- In-app purchases
- Analytics
- Online services
- Login/account system
- User-generated content
- Social sharing
- External links available to children
- Procedural curriculum generation
- Large reward economy

## Commercial Delight Ideas

These ideas enhance the MVP to feel premium without expanding scope too much:

- Garden reacts softly to progress: butterflies, clouds, sunshine rays, birds.
- "Kindness feedback": even incorrect attempts animate gently and guide the child.
- Sticker book unlock after completing all three games.
- Animal friend does a short celebration animation.
- Parent-facing "Learning Goals" screen behind parental gate.
- Voice prompt placeholders for non-reading children.
- Sensory-safe mode: reduce animation intensity and sound volume.
