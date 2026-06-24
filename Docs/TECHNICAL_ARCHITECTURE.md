# Technical Architecture

## Recommended Unity Project Structure

Create the Unity 2D project in this repository, then use this layout:

```text
Assets/
  _TinyGardenHelper/
    Animations/
      PlantGrowth/
      UI/
    Art/
      Placeholders/
      Backgrounds/
      Characters/
      Plants/
      UI/
    Audio/
      Music/
      SFX/
      Voice/
      Placeholders/
    Materials/
    Prefabs/
      Activities/
      Garden/
      Rewards/
      UI/
    Scenes/
      00_Boot.unity
      01_Splash.unity
      02_MainMenu.unity
      03_Garden.unity
      04_Settings.unity
    ScriptableObjects/
      Activities/
      Audio/
      Rewards/
      RuntimeConfig/
    Scripts/
      Activities/
      Audio/
      Bootstrap/
      Garden/
      Rewards/
      Saving/
      Scenes/
      UI/
      Utilities/
    Settings/
    Textures/
```

Keep all game-specific assets inside `Assets/_TinyGardenHelper/` so future Unity-generated folders and packages stay separate.

## Scene List

### 00_Boot

Purpose:

- Initializes persistent systems.
- Loads local save data.
- Applies platform settings such as target frame rate.
- Routes to Splash or Main Menu.

Main objects:

- `GameManager`
- `SceneLoader`
- `AudioManager`
- `SaveSystem`
- `RewardSystem`

### 01_Splash

Purpose:

- Shows studio/game splash briefly.
- Allows skip after a short minimum display time.
- Transitions to Main Menu.

### 02_MainMenu

Purpose:

- Entry point for child-friendly play.
- Has a large Play button.
- Has settings access.
- Avoids external links.

### 03_Garden

Purpose:

- Main playable hub.
- Displays three plants.
- Opens activity panels or loads activity scenes.
- Shows plant growth progress.
- Unlocks animal friend after all activities are complete.

Recommended MVP approach:

- Use activity panels in the Garden scene first.
- Move activities to separate scenes later only if complexity grows.

### 04_Settings

Purpose:

- Audio controls.
- Credits/legal text if needed.
- Parent-only reset progress button behind parental gate.

## Core C# Scripts And Classes

### Bootstrap
- `BootController`: Handles initial splash delay and transitions to Main Menu.
- `GameManager`: Owns high-level game state and service references.
- `AppConfig`: ScriptableObject for app-wide settings.
- `GameState`: Runtime state model for current session.

### Scenes

- `SceneLoader`: Handles scene transitions and loading screens.
- `TransitionView`: Fade in/out and simple scene transition animation.

### Saving

- `ISaveSystem` & `LocalJsonSaveSystem`: Core JSON serialization logic.
- `SaveSystemService`: MonoBehaviour wrapper for GameManager integration.
- `GameSaveData`, `ActivityProgressData`, `SettingsData`: Strongly typed serialization models.

### Audio

- `AudioManager`: Plays music, sound effects, and voice lines.
- `AudioLibrary`: ScriptableObject mapping semantic IDs to clips.
- `AudioSettingsData`: Runtime/settings model for music and sound toggles.

### Garden

- `GardenController`: Coordinates plant selection, activity launch, and completion.
- `PlantController`: Handles individual plant state and growth visuals.
- `PlantDefinition`: ScriptableObject describing plant visuals and activity link.
- `AnimalFriendController`: Handles final unlock reveal.

### Activities

- `MiniGameManager`: Common activity lifecycle and completion reporting.
- `ActivityDefinition`: ScriptableObject for shared activity metadata.
- `ActivityResult`: Simple result object for completed activity.
- `ColorMatchController`: Color matching activity logic.
- `CountingController`: Counting fruits activity logic.
- `ShapeSortingController`: Shape sorting activity logic.
- `ChoiceButton`: Large reusable choice button component.
- `DraggableShape`: Drag/drop shape behavior.
- `DropSlot`: Matching slot for shape sorting.

### Rewards

- `RewardSystem`: Unlocks stickers, plant growth, and animal friend.
- `RewardDefinition`: ScriptableObject describing reward visuals/audio.
- `CelebrationView`: Sparkles, sticker reveal, and success presentation.

### UI

- `UIManager`: Shows/hides panels and child-friendly dialogs.
- `MainMenuView`: Main menu buttons and transitions.
- `SettingsView`: Audio toggles and reset entry.
- `ParentalGateView`: Simple adult gate for protected actions.
- `SafeAreaFitter`: Applies mobile safe area padding.
- `LargeButtonFeedback`: Button scale/sound feedback.

### Utilities
- `TinyGardenSceneBuilder`: Unity Editor script to safely auto-generate scenes and UI canvases.
- `SerializableGuid` or string IDs for stable data references.
- `ScreenUtility`: Orientation and device helpers if needed.
- `TweenLite`: Tiny local animation helper only if Unity animation clips are insufficient.

## Data Model

### ActivityDefinition

Fields:

- `activityId`
- `activityType`
- `displayIcon`
- `plantId`
- `successVoiceLineId`
- `successRewardId`

Activity-specific data can be separate ScriptableObject types:

- `ColorMatchActivityData`
- `CountingActivityData`
- `ShapeSortingActivityData`

### ColorMatchActivityData

Fields:

- `targetColor`
- `choices`
- `targetSprite`
- `choiceSprites`

### CountingActivityData

Fields:

- `fruitCount`
- `fruitSprite`
- `choiceNumbers`
- `voicePromptId`

### ShapeSortingActivityData

Fields:

- `shapes`
- `slots`
- `shapeSprites`
- `slotSprites`

### RewardDefinition

Fields:

- `rewardId`
- `rewardType`
- `displaySprite`
- `sfxId`
- `voiceLineId`
- `unlockCondition`

Reward types:

- `PlantGrowth`
- `Sticker`
- `AnimalFriend`

### SaveData

Suggested JSON model:

```json
{
  "version": 1,
  "completedActivityIds": [],
  "grownPlantIds": [],
  "unlockedRewardIds": [],
  "animalFriendUnlocked": false,
  "musicEnabled": true,
  "sfxEnabled": true,
  "voiceEnabled": true
}
```

For MVP, Unity `JsonUtility` plus `Application.persistentDataPath` is enough. PlayerPrefs may be acceptable for audio toggles, but file-based JSON is cleaner for versioned progress.

## MVP Implementation Roadmap

### Milestone 1: Unity Project Foundation

- Create Unity 2D project.
- Add folder structure.
- Configure portrait orientation.
- Add Boot, Splash, Main Menu, Garden, and Settings scenes.
- Add placeholder font, UI buttons, colors, and scene transition.

### Milestone 2: Core Systems

- Implement `GameManager`, `SceneLoader`, `SaveSystem`, `AudioManager`, and `UIManager`.
- Add local save/load.
- Add audio toggles.
- Add safe area handling.

### Milestone 3: Garden Hub

- Build garden background with three plant prefabs.
- Add small and grown plant states.
- Tap each plant to open its assigned activity.
- Save plant growth state.

### Milestone 4: Color Matching

- Implement large answer buttons.
- Add gentle try-again feedback.
- Complete activity and grow plant.

### Milestone 5: Counting Fruits

- Show 1-5 fruit sprites.
- Add number choices.
- Complete activity and grow plant.

### Milestone 6: Shape Sorting

- Add drag/drop or tap-to-place shapes.
- Snap correct shapes into matching slots.
- Complete activity and grow plant.

### Milestone 7: Rewards And Polish

- Add celebration view.
- Add sparkles and simple animations.
- Add animal friend unlock.
- Add placeholder voice and SFX hooks.

### Milestone 8: Mobile Hardening

- Test screen sizes and safe areas.
- Test saving after app quit/relaunch.
- Build Android and iOS development builds.
- Profile memory and frame pacing.

## Asset Placeholder Plan

The game should run before final art is available.

Placeholder assets:

- Garden background: flat color sky/grass with simple hills.
- Plants: three simple sprite stages per plant.
- Fruits: simple circles or vector-like sprites for apple, berry, orange.
- Shapes: colored circle, square, triangle.
- Animal friend: simple smiling bunny/cat/bird placeholder.
- UI: rounded buttons, icon-style settings/play/back buttons.
- Sparkles: small star sprites with scale/fade animation.
- Audio: short generated or recorded placeholder clips named by semantic purpose.

Placeholder naming:

- `ph_garden_bg`
- `ph_plant_01_small`
- `ph_plant_01_grown`
- `ph_fruit_apple`
- `ph_shape_circle`
- `ph_reward_sparkle`
- `ph_animal_friend`

## Clear Next Implementation Steps

1. Generate the Unity 2D project in this repository.
2. Create `Assets/_TinyGardenHelper/` with the recommended folders.
3. Add the five MVP scenes to Build Settings.
4. Implement the bootstrap systems with empty placeholder UI.
5. Create ScriptableObject definitions for activities, rewards, plants, and audio.
6. Build the Garden scene with three tappable plants.
7. Implement one mini activity fully before starting the next.
