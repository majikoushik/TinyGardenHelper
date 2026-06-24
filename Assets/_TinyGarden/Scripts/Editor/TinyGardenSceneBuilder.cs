#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TinyGarden.Core;
using TinyGarden.UI;
using TinyGarden.Garden;

namespace TinyGarden.Editor
{
    public class TinyGardenSceneBuilder
    {
        private const string SceneFolderPath = "Assets/_TinyGarden/Scenes";
        
        [MenuItem("Tiny Garden/Build Initial Scenes")]
        public static void BuildScenes()
        {
            if (!AssetDatabase.IsValidFolder(SceneFolderPath))
            {
                string[] folders = SceneFolderPath.Split('/');
                string currentPath = folders[0];
                for (int i = 1; i < folders.Length; i++)
                {
                    if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                    {
                        AssetDatabase.CreateFolder(currentPath, folders[i]);
                    }
                    currentPath += "/" + folders[i];
                }
            }

            BuildBootScene();
            BuildMainMenuScene();
            BuildGardenScene();

            UpdateBuildSettings();
            
            Debug.Log("Tiny Garden Scenes built successfully!");
        }

        private static void BuildBootScene()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = SceneNames.Boot;

            CreateEventSystem();
            GameObject canvas = CreateCanvas("Canvas");
            
            // Background
            CreatePanel(canvas, "Background", new Color(0.8f, 0.9f, 1f));
            
            // Title
            CreateText(canvas, "SplashText", "Tiny Garden\nLoading...", 72, Color.black, new Vector2(0, 0), new Vector2(0.5f, 0.5f));

            // Core Managers
            GameObject persistentManagers = new GameObject("PersistentManagers");
            persistentManagers.AddComponent<GameManager>();
            persistentManagers.AddComponent<SceneLoader>();
            persistentManagers.AddComponent<TinyGarden.Audio.AudioManager>();
            persistentManagers.AddComponent<UIManager>();
            persistentManagers.AddComponent<TinyGarden.Rewards.RewardSystem>();
            persistentManagers.AddComponent<TinyGarden.SaveSystem.SaveSystemService>();
            persistentManagers.AddComponent<TinyGarden.Platform.SafeAreaService>();

            // Boot Controller
            GameObject bootManager = new GameObject("BootManager");
            bootManager.AddComponent<BootController>();

            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.Boot}.unity");
        }

        private static void BuildMainMenuScene()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = SceneNames.MainMenu;

            CreateEventSystem();
            GameObject canvas = CreateCanvas("Canvas");
            
            // Background
            CreatePanel(canvas, "Background", new Color(0.7f, 0.9f, 0.7f));
            
            GameObject safeArea = new GameObject("SafeArea");
            safeArea.transform.SetParent(canvas.transform, false);
            RectTransform safeAreaRect = safeArea.AddComponent<RectTransform>();
            safeAreaRect.anchorMin = Vector2.zero;
            safeAreaRect.anchorMax = Vector2.one;
            safeAreaRect.sizeDelta = Vector2.zero;
            safeArea.AddComponent<SafeAreaFitter>();

            CreateText(safeArea, "TitleText", "Tiny Garden Helper", 80, new Color(0.1f, 0.4f, 0.1f), new Vector2(0, 300), new Vector2(0.5f, 0.5f));

            // Play Button
            Button playBtn = CreateButton(safeArea, "PlayButton", "PLAY", new Vector2(0, 0), new Vector2(0.5f, 0.5f), new Vector2(400, 150));
            playBtn.GetComponent<Image>().color = new Color(0.3f, 0.8f, 0.3f);
            
            // Settings Button
            Button settingsBtn = CreateButton(safeArea, "SettingsButton", "Settings", new Vector2(0, -300), new Vector2(0.5f, 0.5f), new Vector2(300, 100));

            // Parental Gate
            GameObject gatePanel = CreatePanel(safeArea, "ParentalGatePanel", new Color(0, 0, 0, 0.9f));
            gatePanel.SetActive(false);
            Text qText = CreateText(gatePanel, "QuestionText", "For grown-ups: What is 12 x 11?", 50, Color.white, new Vector2(0, 100), new Vector2(0.5f, 0.5f));
            Button correctBtn = CreateButton(gatePanel, "Btn_132", "132", new Vector2(-150, -50), new Vector2(0.5f, 0.5f), new Vector2(150, 100));
            Button incorrectBtn = CreateButton(gatePanel, "Btn_121", "121", new Vector2(150, -50), new Vector2(0.5f, 0.5f), new Vector2(150, 100));

            // Settings Panel
            GameObject settingsPanel = CreatePanel(safeArea, "SettingsPanel", new Color(0.9f, 0.9f, 0.9f, 0.95f));
            settingsPanel.SetActive(false);
            CreateText(settingsPanel, "Title", "Settings", 60, Color.black, new Vector2(0, 500), new Vector2(0.5f, 0.5f));
            
            // Real Toggles
            Toggle musicToggle = CreateToggle(settingsPanel, "MusicToggle", "Music", new Vector2(0, 300), new Vector2(0.5f, 0.5f), new Vector2(400, 100));
            Toggle sfxToggle = CreateToggle(settingsPanel, "SFXToggle", "SFX", new Vector2(0, 150), new Vector2(0.5f, 0.5f), new Vector2(400, 100));
            Toggle voiceToggle = CreateToggle(settingsPanel, "VoiceToggle", "Voice", new Vector2(0, 0), new Vector2(0.5f, 0.5f), new Vector2(400, 100));
            Toggle sensoryToggle = CreateToggle(settingsPanel, "SensoryToggle", "Sensory Safe Mode", new Vector2(0, -150), new Vector2(0.5f, 0.5f), new Vector2(400, 100));
            
            Button resetBtn = CreateButton(settingsPanel, "ResetButton", "Reset Progress", new Vector2(0, -350), new Vector2(0.5f, 0.5f), new Vector2(400, 100));
            resetBtn.GetComponent<Image>().color = new Color(0.9f, 0.4f, 0.4f);
            
            Button closeSettingsBtn = CreateButton(settingsPanel, "CloseButton", "Close", new Vector2(0, -550), new Vector2(0.5f, 0.5f), new Vector2(300, 100));

            // Privacy Info
            CreateText(settingsPanel, "PrivacyText", "Privacy: Progress is saved locally. No ads, no analytics.", 30, Color.gray, new Vector2(0, -750), new Vector2(0.5f, 0.5f));

            // Managers
            GameObject uiManager = new GameObject("MainMenuManager");
            var menuController = uiManager.AddComponent<MainMenuController>();
            var gateController = uiManager.AddComponent<ParentalGate>();
            var settingsController = uiManager.AddComponent<TinyGarden.UI.SettingsController>();

            // Wire up Parental Gate
            var gateField = typeof(ParentalGate).GetField("gatePanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (gateField != null) gateField.SetValue(gateController, gatePanel);
            
            var qTextField = typeof(ParentalGate).GetField("questionText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (qTextField != null) qTextField.SetValue(gateController, qText);

            var menuGateField = typeof(MainMenuController).GetField("parentalGate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (menuGateField != null) menuGateField.SetValue(menuController, gateController);

            var settingsPanelField = typeof(TinyGarden.UI.SettingsController).GetField("settingsPanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (settingsPanelField != null) settingsPanelField.SetValue(settingsController, settingsPanel);

            // Wire up Toggles
            var mToggleField = typeof(TinyGarden.UI.SettingsController).GetField("musicToggle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mToggleField != null) mToggleField.SetValue(settingsController, musicToggle);

            var sToggleField = typeof(TinyGarden.UI.SettingsController).GetField("sfxToggle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (sToggleField != null) sToggleField.SetValue(settingsController, sfxToggle);

            var vToggleField = typeof(TinyGarden.UI.SettingsController).GetField("voiceToggle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (vToggleField != null) vToggleField.SetValue(settingsController, voiceToggle);

            var ssToggleField = typeof(TinyGarden.UI.SettingsController).GetField("sensorySafeToggle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (ssToggleField != null) ssToggleField.SetValue(settingsController, sensoryToggle);

            UnityEditor.Events.UnityEventTools.AddPersistentListener(musicToggle.onValueChanged, settingsController.OnMusicToggled);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(sfxToggle.onValueChanged, settingsController.OnSfxToggled);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(voiceToggle.onValueChanged, settingsController.OnVoiceToggled);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(sensoryToggle.onValueChanged, settingsController.OnSensorySafeToggled);

            // Wire UnityEvents
            UnityEditor.Events.UnityEventTools.AddPersistentListener(playBtn.onClick, menuController.OnPlayClicked);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(settingsBtn.onClick, menuController.OnSettingsClicked);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(correctBtn.onClick, gateController.OnCorrectAnswerClicked);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(incorrectBtn.onClick, gateController.OnIncorrectAnswerClicked);
            
            // Wire Gate to Settings
            var gateEventField = typeof(ParentalGate).GetField("OnGateUnlocked", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (gateEventField != null)
            {
                var ev = (UnityEngine.Events.UnityEvent)gateEventField.GetValue(gateController);
                if (ev == null)
                {
                    ev = new UnityEngine.Events.UnityEvent();
                    gateEventField.SetValue(gateController, ev);
                }
                UnityEditor.Events.UnityEventTools.AddPersistentListener(ev, settingsController.OpenSettings);
            }

            UnityEditor.Events.UnityEventTools.AddPersistentListener(closeSettingsBtn.onClick, settingsController.CloseSettings);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(resetBtn.onClick, settingsController.OnResetProgressClicked);

            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.MainMenu}.unity");
        }

        private static void BuildGardenScene()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = SceneNames.Garden;

            CreateEventSystem();
            GameObject canvas = CreateCanvas("Canvas");
            
            // Background
            CreatePanel(canvas, "Background", new Color(0.6f, 0.8f, 1f)); // Sky blue

            GameObject safeArea = new GameObject("SafeArea");
            safeArea.transform.SetParent(canvas.transform, false);
            RectTransform safeAreaRect = safeArea.AddComponent<RectTransform>();
            safeAreaRect.anchorMin = Vector2.zero;
            safeAreaRect.anchorMax = Vector2.one;
            safeAreaRect.sizeDelta = Vector2.zero;
            safeArea.AddComponent<SafeAreaFitter>();

            CreateText(safeArea, "TitleText", "My Garden", 60, Color.black, new Vector2(0, 400), new Vector2(0.5f, 0.5f));

            // Spots
            Button spot1 = CreateButton(safeArea, "Spot_Color", "Color Flower", new Vector2(-250, 0), new Vector2(0.5f, 0.5f), new Vector2(200, 200));
            Button spot2 = CreateButton(safeArea, "Spot_Fruit", "Fruit Tree", new Vector2(0, 0), new Vector2(0.5f, 0.5f), new Vector2(200, 200));
            Button spot3 = CreateButton(safeArea, "Spot_Shape", "Shape Bush", new Vector2(250, 0), new Vector2(0.5f, 0.5f), new Vector2(200, 200));

            var act1 = spot1.gameObject.AddComponent<GardenActivitySpot>();
            var act2 = spot2.gameObject.AddComponent<GardenActivitySpot>();
            var act3 = spot3.gameObject.AddComponent<GardenActivitySpot>();

            SetActivityId(act1, TinyGarden.MiniGames.Shared.ActivityId.ColorMatch);
            SetActivityId(act2, TinyGarden.MiniGames.Shared.ActivityId.CountingFruits);
            SetActivityId(act3, TinyGarden.MiniGames.Shared.ActivityId.ShapeSort);

            UnityEditor.Events.UnityEventTools.AddPersistentListener(spot1.onClick, act1.OnSpotTapped);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(spot2.onClick, act2.OnSpotTapped);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(spot3.onClick, act3.OnSpotTapped);

            // Animal Spot Placeholder
            CreatePanel(safeArea, "AnimalPlaceholder", new Color(0.9f, 0.9f, 0.9f, 0.5f), new Vector2(0, -300), new Vector2(0.5f, 0.5f), new Vector2(300, 200));
            Text animalTxt = CreateText(safeArea, "AnimalText", "Animal Friend\n(Locked)", 30, Color.black, new Vector2(0, -300), new Vector2(0.5f, 0.5f));

            // Home Button
            Button homeBtn = CreateButton(safeArea, "HomeButton", "Home", new Vector2(-400, 800), new Vector2(0.5f, 0f), new Vector2(150, 100));
            
            GameObject controllerObj = new GameObject("GardenManager");
            var controller = controllerObj.AddComponent<GardenSceneController>();
            
            var backBtn = CreateButton(safeArea, "BackButton", "Back to Menu", new Vector2(-250, 600), new Vector2(0.5f, 0.5f), new Vector2(200, 100));
            UnityEditor.Events.UnityEventTools.AddPersistentListener(backBtn.onClick, controller.OnHomeClicked);

            // Create Benny Bunny (Animal Friend)
            GameObject bennyBunny = CreatePanel(safeArea, "BennyBunny", new Color(0.9f, 0.8f, 0.9f), new Vector2(200, 0), new Vector2(0.5f, 0.5f), new Vector2(120, 160));
            CreateText(bennyBunny, "Label", "Benny", 30, Color.black, Vector2.zero, new Vector2(0.5f, 0.5f));
            var animalController = bennyBunny.AddComponent<TinyGarden.Rewards.AnimalFriendController>();
            var animalFields = typeof(TinyGarden.Rewards.AnimalFriendController).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach(var f in animalFields)
            {
                if (f.Name == "animalVisuals") f.SetValue(animalController, bennyBunny); // Simplification: self is visuals
            }

            // Create Sticker Book
            GameObject stickerBookBtnObj = CreateButton(safeArea, "StickerBookButton", "Sticker Book", new Vector2(-250, -400), new Vector2(0.5f, 0.5f), new Vector2(200, 100)).gameObject;
            
            GameObject stickerBookPanel = CreatePanel(safeArea, "StickerBookPanel", new Color(0.95f, 0.95f, 0.95f, 0.95f));
            CreateText(stickerBookPanel, "Title", "My Sticker Book", 60, Color.black, new Vector2(0, 300), new Vector2(0.5f, 0.5f));
            var closeBookBtn = CreateButton(stickerBookPanel, "CloseBookButton", "Close", new Vector2(0, -300), new Vector2(0.5f, 0.5f), new Vector2(200, 100));
            
            GameObject starSticker = CreatePanel(stickerBookPanel, "StarSticker", Color.yellow, Vector2.zero, new Vector2(0.5f, 0.5f), new Vector2(150, 150));
            CreateText(starSticker, "Label", "Star", 40, Color.black, Vector2.zero, new Vector2(0.5f, 0.5f));
            var starImage = starSticker.GetComponent<UnityEngine.UI.Image>();

            var stickerController = controllerObj.AddComponent<TinyGarden.Rewards.StickerBookController>();
            var stickerFields = typeof(TinyGarden.Rewards.StickerBookController).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach(var f in stickerFields)
            {
                if (f.Name == "stickerBookPanel") f.SetValue(stickerController, stickerBookPanel);
                if (f.Name == "starStickerImage") f.SetValue(stickerController, starImage);
            }
            
            UnityEditor.Events.UnityEventTools.AddPersistentListener(stickerBookBtnObj.GetComponent<UnityEngine.UI.Button>().onClick, stickerController.OpenStickerBook);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(closeBookBtn.onClick, stickerController.CloseStickerBook);
            stickerBookPanel.SetActive(false);

            // Celebration Panel
            GameObject celebrationPanel = CreatePanel(safeArea, "CelebrationPanel", new Color(1f, 1f, 1f, 0.9f));
            var celebText = CreateText(celebrationPanel, "CelebrationText", "A new garden friend!", 70, new Color(0.8f, 0.4f, 0.8f), Vector2.zero, new Vector2(0.5f, 0.5f));
            var celebCanvasGroup = celebrationPanel.AddComponent<CanvasGroup>();
            
            var celebController = controllerObj.AddComponent<TinyGarden.Rewards.RewardCelebrationController>();
            var celebFields = typeof(TinyGarden.Rewards.RewardCelebrationController).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach(var f in celebFields)
            {
                if (f.Name == "celebrationPanel") f.SetValue(celebController, celebrationPanel);
                if (f.Name == "celebrationText") f.SetValue(celebController, celebText.GetComponent<UnityEngine.UI.Text>());
                if (f.Name == "canvasGroup") f.SetValue(celebController, celebCanvasGroup);
            }
            celebrationPanel.SetActive(false);

            // Progress Presenter
            var presenter = controllerObj.AddComponent<TinyGarden.Garden.GardenProgressPresenter>();
            var presenterFields = typeof(TinyGarden.Garden.GardenProgressPresenter).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach(var f in presenterFields)
            {
                if (f.Name == "celebrationController") f.SetValue(presenter, celebController);
                if (f.Name == "animalFriend") f.SetValue(presenter, animalController);
            }

            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.Garden}.unity");
        }

        private static void SetActivityId(GardenActivitySpot spot, TinyGarden.MiniGames.Shared.ActivityId id)
        {
            var field = typeof(GardenActivitySpot).GetField("activityId", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(spot, id);
            }
            
            var imageField = typeof(GardenActivitySpot).GetField("spotImage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (imageField != null)
            {
                imageField.SetValue(spot, spot.GetComponent<Image>());
            }
        } 

        private static GameObject CreateCanvas(string name)
        {
            GameObject canvasGO = new GameObject(name);
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.matchWidthOrHeight = 0.5f;
            canvasGO.AddComponent<GraphicRaycaster>();
            return canvasGO;
        }

        private static void CreateEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
        }

        private static GameObject CreatePanel(GameObject parent, string name, Color color, Vector2 pos = default, Vector2 anchor = default, Vector2 size = default)
        {
            GameObject panel = new GameObject(name);
            panel.transform.SetParent(parent.transform, false);
            RectTransform rect = panel.AddComponent<RectTransform>();
            
            if (size == default)
            {
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
            }
            else
            {
                rect.anchorMin = anchor == default ? new Vector2(0.5f, 0.5f) : anchor;
                rect.anchorMax = rect.anchorMin;
                rect.anchoredPosition = pos;
                rect.sizeDelta = size;
            }

            Image img = panel.AddComponent<Image>();
            img.color = color;
            return panel;
        }

        private static Text CreateText(GameObject parent, string name, string content, int fontSize, Color color, Vector2 pos, Vector2 anchor)
        {
            GameObject textGO = new GameObject(name);
            textGO.transform.SetParent(parent.transform, false);
            RectTransform rect = textGO.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.anchoredPosition = pos;
            rect.sizeDelta = new Vector2(800, 200);

            Text text = textGO.AddComponent<Text>();
            text.text = content;
            text.fontSize = fontSize;
            text.color = color;
            text.alignment = TextAnchor.MiddleCenter;
            
            // Try to set Arial font as default fallback
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            return text;
        }

        private static Button CreateButton(GameObject parent, string name, string label, Vector2 pos, Vector2 anchor, Vector2 size)
        {
            GameObject btnGO = new GameObject(name);
            btnGO.transform.SetParent(parent.transform, false);
            RectTransform rect = btnGO.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.anchoredPosition = pos;
            rect.sizeDelta = size;

            Image img = btnGO.AddComponent<Image>();
            img.color = Color.white;

            Button btn = btnGO.AddComponent<Button>();
            btnGO.AddComponent<ButtonAnimator>(); // Add micro-animation

            CreateText(btnGO, "Text", label, 40, Color.black, Vector2.zero, new Vector2(0.5f, 0.5f));

            return btn;
        }

        private static Toggle CreateToggle(GameObject parent, string name, string label, Vector2 pos, Vector2 anchor, Vector2 size)
        {
            GameObject toggleGO = new GameObject(name);
            toggleGO.transform.SetParent(parent.transform, false);
            RectTransform rect = toggleGO.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.anchoredPosition = pos;
            rect.sizeDelta = size;

            Toggle toggle = toggleGO.AddComponent<Toggle>();

            // Background checkbox
            GameObject bgGO = CreatePanel(toggleGO, "Background", Color.white, new Vector2(-size.x / 2f + 50f, 0), new Vector2(0.5f, 0.5f), new Vector2(50, 50));
            
            // Checkmark
            GameObject checkmarkGO = CreatePanel(bgGO, "Checkmark", new Color(0.2f, 0.8f, 0.2f), Vector2.zero, new Vector2(0.5f, 0.5f), new Vector2(30, 30));
            
            toggle.targetGraphic = bgGO.GetComponent<Image>();
            toggle.graphic = checkmarkGO.GetComponent<Image>();
            
            // Label text
            Text labelText = CreateText(toggleGO, "Label", label, 40, Color.black, new Vector2(50, 0), new Vector2(0.5f, 0.5f));
            labelText.alignment = TextAnchor.MiddleLeft;

            toggle.isOn = true;
            return toggle;
        }

        private static void UpdateBuildSettings()
        {
            var requiredScenes = new string[]
            {
                $"{SceneFolderPath}/{SceneNames.Boot}.unity",
                $"{SceneFolderPath}/{SceneNames.MainMenu}.unity",
                $"{SceneFolderPath}/{SceneNames.Garden}.unity"
            };

            var originalScenes = EditorBuildSettings.scenes;
            var newScenesList = new System.Collections.Generic.List<EditorBuildSettingsScene>();

            foreach (var path in requiredScenes)
            {
                newScenesList.Add(new EditorBuildSettingsScene(path, true));
            }

            foreach (var s in originalScenes)
            {
                if (System.Array.IndexOf(requiredScenes, s.path) == -1)
                {
                    newScenesList.Add(new EditorBuildSettingsScene(s.path, s.enabled));
                }
            }

            EditorBuildSettings.scenes = newScenesList.ToArray();
        }
    }
}
#endif
