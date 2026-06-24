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
            GameObject gatePanel = CreatePanel(safeArea, "ParentalGatePanel", new Color(0, 0, 0, 0.8f));
            gatePanel.SetActive(false);
            Text qText = CreateText(gatePanel, "QuestionText", "For grown-ups: What is 12 x 11?", 50, Color.white, new Vector2(0, 100), new Vector2(0.5f, 0.5f));
            Button correctBtn = CreateButton(gatePanel, "Btn_132", "132", new Vector2(-150, -50), new Vector2(0.5f, 0.5f), new Vector2(150, 100));
            Button incorrectBtn = CreateButton(gatePanel, "Btn_121", "121", new Vector2(150, -50), new Vector2(0.5f, 0.5f), new Vector2(150, 100));

            // Managers
            GameObject uiManager = new GameObject("MainMenuManager");
            var menuController = uiManager.AddComponent<MainMenuController>();
            var gateController = uiManager.AddComponent<ParentalGatePlaceholder>();

            // Wire up Parental Gate
            var gateField = typeof(ParentalGatePlaceholder).GetField("gatePanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (gateField != null) gateField.SetValue(gateController, gatePanel);
            
            var qTextField = typeof(ParentalGatePlaceholder).GetField("questionText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (qTextField != null) qTextField.SetValue(gateController, qText);

            var menuGateField = typeof(MainMenuController).GetField("parentalGate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (menuGateField != null) menuGateField.SetValue(menuController, gateController);

            // Wire UnityEvents
            UnityEditor.Events.UnityEventTools.AddPersistentListener(playBtn.onClick, menuController.OnPlayClicked);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(settingsBtn.onClick, menuController.OnSettingsClicked);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(correctBtn.onClick, gateController.OnCorrectAnswerClicked);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(incorrectBtn.onClick, gateController.OnIncorrectAnswerClicked);

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

            SetActivityName(act1, "Color Match");
            SetActivityName(act2, "Counting Fruits");
            SetActivityName(act3, "Shape Sort");

            UnityEditor.Events.UnityEventTools.AddPersistentListener(spot1.onClick, act1.OnSpotTapped);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(spot2.onClick, act2.OnSpotTapped);
            UnityEditor.Events.UnityEventTools.AddPersistentListener(spot3.onClick, act3.OnSpotTapped);

            // Animal Spot Placeholder
            CreatePanel(safeArea, "AnimalPlaceholder", new Color(0.9f, 0.9f, 0.9f, 0.5f), new Vector2(0, -300), new Vector2(0.5f, 0.5f), new Vector2(300, 200));
            CreateText(safeArea, "AnimalText", "Animal Friend\n(Locked)", 30, Color.black, new Vector2(0, -300), new Vector2(0.5f, 0.5f));

            // Home Button
            Button homeBtn = CreateButton(safeArea, "HomeButton", "Home", new Vector2(-400, 800), new Vector2(0.5f, 0f), new Vector2(150, 100));
            
            GameObject uiManager = new GameObject("GardenManager");
            var gardenController = uiManager.AddComponent<GardenSceneController>();
            UnityEditor.Events.UnityEventTools.AddPersistentListener(homeBtn.onClick, gardenController.OnHomeClicked);

            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.Garden}.unity");
        }

        private static void SetActivityName(GardenActivitySpot spot, string name)
        {
            var field = typeof(GardenActivitySpot).GetField("activityName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null) field.SetValue(spot, name);
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

            CreateText(btnGO, "Text", label, 40, Color.black, Vector2.zero, new Vector2(0.5f, 0.5f));

            return btn;
        }

        private static void UpdateBuildSettings()
        {
            var scenes = new EditorBuildSettingsScene[]
            {
                new EditorBuildSettingsScene($"{SceneFolderPath}/{SceneNames.Boot}.unity", true),
                new EditorBuildSettingsScene($"{SceneFolderPath}/{SceneNames.MainMenu}.unity", true),
                new EditorBuildSettingsScene($"{SceneFolderPath}/{SceneNames.Garden}.unity", true)
            };
            EditorBuildSettings.scenes = scenes;
        }
    }
}
#endif
