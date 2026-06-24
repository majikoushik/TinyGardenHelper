#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TinyGarden.Core;
using TinyGarden.MiniGames.ColorMatch;

namespace TinyGarden.Editor
{
    public class TinyGardenMiniGameBuilder
    {
        private const string SceneFolderPath = "Assets/_TinyGarden/Scenes";

        [MenuItem("Tiny Garden/Build Mini-Games")]
        public static void BuildMiniGames()
        {
            BuildColorMatchScene();
            BuildCountingFruitsScene();
            BuildShapeSortScene();
            UpdateBuildSettingsWithMiniGames();
            Debug.Log("Tiny Garden Mini-Games built successfully!");
        }

        private static void BuildColorMatchScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = SceneNames.ColorMatch;

            // Camera
            GameObject cameraGO = new GameObject("Main Camera");
            Camera cam = cameraGO.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.8f, 0.9f, 1.0f); // Light sky blue
            cam.orthographic = true;
            cameraGO.tag = "MainCamera";

            // Event System
            CreateEventSystem();

            // Canvas
            GameObject canvas = CreateCanvas("Canvas");

            // Background / Safe Area
            GameObject safeArea = new GameObject("SafeArea");
            safeArea.transform.SetParent(canvas.transform, false);
            RectTransform safeAreaRect = safeArea.AddComponent<RectTransform>();
            safeAreaRect.anchorMin = Vector2.zero;
            safeAreaRect.anchorMax = Vector2.one;
            safeAreaRect.sizeDelta = Vector2.zero;
            safeArea.AddComponent<TinyGarden.UI.SafeAreaFitter>();

            CreateText(safeArea, "TitleText", "Match the Colors!", 60, Color.black, new Vector2(0, 400), new Vector2(0.5f, 0.5f));

            // Targets (Flowers/Pots)
            float spacingX = 250f;
            var targetRed = CreateDropTarget(safeArea, "Target_Red", "color_red", Color.red, new Vector2(-spacingX, 0));
            var targetBlue = CreateDropTarget(safeArea, "Target_Blue", "color_blue", Color.blue, new Vector2(0, 0));
            var targetYellow = CreateDropTarget(safeArea, "Target_Yellow", "color_yellow", Color.yellow, new Vector2(spacingX, 0));

            // Draggables (Butterflies)
            var dragRed = CreateDraggable(safeArea, "Drag_Red", "color_red", Color.red, new Vector2(-spacingX, -400));
            var dragBlue = CreateDraggable(safeArea, "Drag_Blue", "color_blue", Color.blue, new Vector2(0, -400));
            var dragYellow = CreateDraggable(safeArea, "Drag_Yellow", "color_yellow", Color.yellow, new Vector2(spacingX, -400));

            // Celebration Panel
            GameObject celebrationPanel = CreatePanel(safeArea, "CelebrationPanel", new Color(1f, 1f, 1f, 0.9f));
            CreateText(celebrationPanel, "SuccessText", "You did it!", 80, new Color(0.8f, 0.4f, 0.8f), new Vector2(0, 200), new Vector2(0.5f, 0.5f));
            Button backBtn = CreateButton(celebrationPanel, "BackToGardenButton", "Back to Garden", new Vector2(0, -100), new Vector2(0.5f, 0.5f), new Vector2(300, 100));

            celebrationPanel.SetActive(false);

            // Game Controller
            GameObject controllerObj = new GameObject("ColorMatchController");
            var controller = controllerObj.AddComponent<ColorMatchController>();
            
            // Assign Celebration panel via reflection since it's private
            var panelField = typeof(ColorMatchController).GetField("celebrationPanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (panelField != null) panelField.SetValue(controller, celebrationPanel);

            UnityEditor.Events.UnityEventTools.AddPersistentListener(backBtn.onClick, controller.ReturnToGarden);

            // Save Scene
            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.ColorMatch}.unity");
        }

        private static void BuildCountingFruitsScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = SceneNames.CountingFruits;

            // Camera
            GameObject cameraGO = new GameObject("Main Camera");
            Camera cam = cameraGO.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.7f, 0.9f, 0.7f); // Light green sky/orchard
            cam.orthographic = true;
            cameraGO.tag = "MainCamera";

            CreateEventSystem();
            GameObject canvas = CreateCanvas("Canvas");

            GameObject safeArea = new GameObject("SafeArea");
            safeArea.transform.SetParent(canvas.transform, false);
            RectTransform safeAreaRect = safeArea.AddComponent<RectTransform>();
            safeAreaRect.anchorMin = Vector2.zero;
            safeAreaRect.anchorMax = Vector2.one;
            safeAreaRect.sizeDelta = Vector2.zero;
            safeArea.AddComponent<TinyGarden.UI.SafeAreaFitter>();

            CreateText(safeArea, "TitleText", "Put apples in the basket!", 50, Color.black, new Vector2(0, 700), new Vector2(0.5f, 0.5f));

            // Visual Indicator
            GameObject visualIndicator = new GameObject("VisualIndicator");
            visualIndicator.transform.SetParent(safeArea.transform, false);
            var indicatorRect = visualIndicator.AddComponent<RectTransform>();
            indicatorRect.anchorMin = new Vector2(0.5f, 0.5f);
            indicatorRect.anchorMax = new Vector2(0.5f, 0.5f);
            indicatorRect.anchoredPosition = new Vector2(0, 500);

            var numberText = CreateText(visualIndicator, "TargetNumber", "3", 120, Color.white, new Vector2(-150, 0), new Vector2(0.5f, 0.5f));
            
            GameObject dotsContainer = new GameObject("DotsContainer");
            dotsContainer.transform.SetParent(visualIndicator.transform, false);
            var dotsRect = dotsContainer.AddComponent<RectTransform>();
            dotsRect.anchorMin = new Vector2(0.5f, 0.5f);
            dotsRect.anchorMax = new Vector2(0.5f, 0.5f);
            dotsRect.anchoredPosition = new Vector2(100, 0);
            var hzLayout = dotsContainer.AddComponent<HorizontalLayoutGroup>();
            hzLayout.childAlignment = TextAnchor.MiddleLeft;
            hzLayout.spacing = 20;
            hzLayout.childControlWidth = false;
            hzLayout.childControlHeight = false;

            // Dot prefab: create under a hidden GO so transform.SetParent doesn't throw on null
            GameObject dotPrefabHolder = new GameObject("DotPrefabHolder");
            dotPrefabHolder.transform.SetParent(safeArea.transform, false);
            dotPrefabHolder.SetActive(false);
            GameObject dotPrefab = CreatePanel(dotPrefabHolder, "DotPrefab", Color.gray, Vector2.zero, new Vector2(0.5f, 0.5f), new Vector2(50, 50));

            var viScript = visualIndicator.AddComponent<TinyGarden.MiniGames.CountingFruits.CountVisualIndicator>();
            var viFields = typeof(TinyGarden.MiniGames.CountingFruits.CountVisualIndicator).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach (var f in viFields)
            {
                if (f.Name == "targetNumberText") f.SetValue(viScript, numberText);
                if (f.Name == "dotsContainer") f.SetValue(viScript, dotsContainer.transform);
                if (f.Name == "dotPrefab") f.SetValue(viScript, dotPrefab);
                if (f.Name == "activeDotColor") f.SetValue(viScript, Color.green);
                if (f.Name == "inactiveDotColor") f.SetValue(viScript, Color.gray);
            }

            // Basket (Drop Zone)
            GameObject basket = CreatePanel(safeArea, "Basket", new Color(0.8f, 0.6f, 0.4f), new Vector2(0, -500), new Vector2(0.5f, 0.5f), new Vector2(400, 300));
            var basketDrop = basket.AddComponent<TinyGarden.MiniGames.CountingFruits.FruitBasketDropZone>();
            CreateText(basket, "BasketLabel", "Basket", 40, Color.white, Vector2.zero, new Vector2(0.5f, 0.5f));

            // Apples (Draggables)
            GameObject treeArea = new GameObject("TreeArea");
            treeArea.transform.SetParent(safeArea.transform, false);
            var treeRect = treeArea.AddComponent<RectTransform>();
            treeRect.anchorMin = new Vector2(0.5f, 0.5f);
            treeRect.anchorMax = new Vector2(0.5f, 0.5f);
            treeRect.anchoredPosition = new Vector2(0, 100);

            for (int i = 0; i < 5; i++)
            {
                float x = (i - 2) * 150f;
                float y = (i % 2 == 0) ? 0 : 100f;
                GameObject apple = CreatePanel(treeArea, $"Apple_{i}", Color.red, new Vector2(x, y), new Vector2(0.5f, 0.5f), new Vector2(100, 100));
                apple.AddComponent<CanvasGroup>();
                apple.AddComponent<TinyGarden.MiniGames.CountingFruits.FruitDraggableItem>();
            }

            // Celebration Panel
            GameObject celebrationPanel = CreatePanel(safeArea, "CelebrationPanel", new Color(1f, 1f, 1f, 0.9f));
            CreateText(celebrationPanel, "SuccessText", "Great Counting!", 80, new Color(0.2f, 0.8f, 0.2f), new Vector2(0, 200), new Vector2(0.5f, 0.5f));
            Button backBtn = CreateButton(celebrationPanel, "BackToGardenButton", "Back to Garden", new Vector2(0, -100), new Vector2(0.5f, 0.5f), new Vector2(300, 100));

            celebrationPanel.SetActive(false);

            // Game Controller
            GameObject controllerObj = new GameObject("CountingFruitsController");
            var controller = controllerObj.AddComponent<TinyGarden.MiniGames.CountingFruits.CountingFruitsController>();
            
            var def = ScriptableObject.CreateInstance<TinyGarden.MiniGames.CountingFruits.CountingFruitsDefinition>();
            
            var cFields = typeof(TinyGarden.MiniGames.CountingFruits.CountingFruitsController).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach (var f in cFields)
            {
                if (f.Name == "definition") f.SetValue(controller, def);
                if (f.Name == "visualIndicator") f.SetValue(controller, viScript);
                if (f.Name == "basket") f.SetValue(controller, basketDrop);
                if (f.Name == "celebrationPanel") f.SetValue(controller, celebrationPanel);
            }

            UnityEditor.Events.UnityEventTools.AddPersistentListener(backBtn.onClick, controller.ReturnToGarden);

            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.CountingFruits}.unity");
        }

        private static void BuildShapeSortScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            scene.name = SceneNames.ShapeSort;

            // Camera
            GameObject cameraGO = new GameObject("Main Camera");
            Camera cam = cameraGO.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.95f, 0.9f, 0.8f); // Warm paper/sand color
            cam.orthographic = true;
            cameraGO.tag = "MainCamera";

            CreateEventSystem();
            GameObject canvas = CreateCanvas("Canvas");

            GameObject safeArea = new GameObject("SafeArea");
            safeArea.transform.SetParent(canvas.transform, false);
            RectTransform safeAreaRect = safeArea.AddComponent<RectTransform>();
            safeAreaRect.anchorMin = Vector2.zero;
            safeAreaRect.anchorMax = Vector2.one;
            safeAreaRect.sizeDelta = Vector2.zero;
            safeArea.AddComponent<TinyGarden.UI.SafeAreaFitter>();

            CreateText(safeArea, "TitleText", "Find each shape's home!", 50, Color.black, new Vector2(0, 700), new Vector2(0.5f, 0.5f));

            // Targets (Homes)
            float spacingX = 300f;
            var targetCircle = CreateShapeTarget(safeArea, "Target_Circle", "Circle", Color.gray, new Vector2(-spacingX, 200));
            var targetSquare = CreateShapeTarget(safeArea, "Target_Square", "Square", Color.gray, new Vector2(0, 200));
            var targetTriangle = CreateShapeTarget(safeArea, "Target_Triangle", "Triangle", Color.gray, new Vector2(spacingX, 200));

            // Draggables (Shapes)
            var dragCircle = CreateShapeDraggable(safeArea, "Drag_Circle", "Circle", new Color(0.9f, 0.3f, 0.3f), new Vector2(spacingX, -300), TinyGarden.MiniGames.ShapeSort.ShapeType.Circle);
            var dragSquare = CreateShapeDraggable(safeArea, "Drag_Square", "Square", new Color(0.3f, 0.3f, 0.9f), new Vector2(-spacingX, -300), TinyGarden.MiniGames.ShapeSort.ShapeType.Square);
            var dragTriangle = CreateShapeDraggable(safeArea, "Drag_Triangle", "Triangle", new Color(0.3f, 0.9f, 0.3f), new Vector2(0, -300), TinyGarden.MiniGames.ShapeSort.ShapeType.Triangle);

            // Celebration Panel
            GameObject celebrationPanel = CreatePanel(safeArea, "CelebrationPanel", new Color(1f, 1f, 1f, 0.9f));
            CreateText(celebrationPanel, "SuccessText", "Super Sorting!", 80, new Color(0.8f, 0.5f, 0.2f), new Vector2(0, 200), new Vector2(0.5f, 0.5f));
            Button backBtn = CreateButton(celebrationPanel, "BackToGardenButton", "Back to Garden", new Vector2(0, -100), new Vector2(0.5f, 0.5f), new Vector2(300, 100));
            celebrationPanel.SetActive(false);

            // Game Controller
            GameObject controllerObj = new GameObject("ShapeSortController");
            var controller = controllerObj.AddComponent<TinyGarden.MiniGames.ShapeSort.ShapeSortController>();
            
            var def = ScriptableObject.CreateInstance<TinyGarden.MiniGames.ShapeSort.ShapeSortDefinition>();
            var cFields = typeof(TinyGarden.MiniGames.ShapeSort.ShapeSortController).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach (var f in cFields)
            {
                if (f.Name == "definition") f.SetValue(controller, def);
                if (f.Name == "celebrationPanel") f.SetValue(controller, celebrationPanel);
            }

            // Wire up draggables to controller
            dragCircle.GetComponent<TinyGarden.MiniGames.ShapeSort.ShapeDraggableItem>().Setup(new TinyGarden.MiniGames.ShapeSort.ShapeItemData { shapeType = TinyGarden.MiniGames.ShapeSort.ShapeType.Circle }, controller);
            dragSquare.GetComponent<TinyGarden.MiniGames.ShapeSort.ShapeDraggableItem>().Setup(new TinyGarden.MiniGames.ShapeSort.ShapeItemData { shapeType = TinyGarden.MiniGames.ShapeSort.ShapeType.Square }, controller);
            dragTriangle.GetComponent<TinyGarden.MiniGames.ShapeSort.ShapeDraggableItem>().Setup(new TinyGarden.MiniGames.ShapeSort.ShapeItemData { shapeType = TinyGarden.MiniGames.ShapeSort.ShapeType.Triangle }, controller);

            UnityEditor.Events.UnityEventTools.AddPersistentListener(backBtn.onClick, controller.ReturnToGarden);

            EditorSceneManager.SaveScene(scene, $"{SceneFolderPath}/{SceneNames.ShapeSort}.unity");
        }

        private static GameObject CreateShapeTarget(GameObject parent, string name, string matchId, Color col, Vector2 pos)
        {
            GameObject go = CreatePanel(parent, name, new Color(col.r, col.g, col.b, 0.3f), pos, new Vector2(0.5f, 0.5f), new Vector2(200, 200));
            var target = go.AddComponent<TinyGarden.MiniGames.ShapeSort.ShapeDropTarget>();
            
            var data = new TinyGarden.MiniGames.ShapeSort.ShapeItemData { shapeType = (TinyGarden.MiniGames.ShapeSort.ShapeType)System.Enum.Parse(typeof(TinyGarden.MiniGames.ShapeSort.ShapeType), matchId) };
            target.Setup(data);
            
            // Add visual shape icon
            string shapeText = matchId == "Circle" ? "●" : (matchId == "Square" ? "■" : "▲");
            CreateText(go, "ShapeIcon", shapeText, 150, new Color(col.r, col.g, col.b, 0.5f), Vector2.zero, new Vector2(0.5f, 0.5f));

            return go;
        }

        private static GameObject CreateShapeDraggable(GameObject parent, string name, string matchId, Color col, Vector2 pos, TinyGarden.MiniGames.ShapeSort.ShapeType type)
        {
            GameObject go = CreatePanel(parent, name, new Color(1f, 1f, 1f, 0f), pos, new Vector2(0.5f, 0.5f), new Vector2(150, 150)); // Transparent bg for drag hit box
            go.AddComponent<CanvasGroup>();
            var draggable = go.AddComponent<TinyGarden.MiniGames.ShapeSort.ShapeDraggableItem>();

            // Add visual shape icon
            string shapeText = matchId == "Circle" ? "●" : (matchId == "Square" ? "■" : "▲");
            CreateText(go, "ShapeIcon", shapeText, 150, col, Vector2.zero, new Vector2(0.5f, 0.5f));

            return go;
        }

        private static GameObject CreateDropTarget(GameObject parent, string name, string matchId, Color col, Vector2 pos)
        {
            GameObject go = CreatePanel(parent, name, new Color(col.r, col.g, col.b, 0.5f), pos, new Vector2(0.5f, 0.5f), new Vector2(150, 150));
            var target = go.AddComponent<ColorDropTarget>();
            
            // Generate temporary data
            var data = new ColorMatchItemData { colorId = matchId, colorValue = col };
            target.Setup(data);
            
            return go;
        }

        private static GameObject CreateDraggable(GameObject parent, string name, string matchId, Color col, Vector2 pos)
        {
            GameObject go = CreatePanel(parent, name, col, pos, new Vector2(0.5f, 0.5f), new Vector2(100, 100));
            
            // Need a CanvasGroup for drag behavior
            go.AddComponent<CanvasGroup>();
            
            var draggable = go.AddComponent<ColorDraggableItem>();
            
            var data = new ColorMatchItemData { colorId = matchId, colorValue = col };
            draggable.Setup(data);
            
            return go;
        }

        private static void UpdateBuildSettingsWithMiniGames()
        {
            var originalScenes = EditorBuildSettings.scenes;
            var newScenesList = new System.Collections.Generic.List<EditorBuildSettingsScene>();
            newScenesList.AddRange(originalScenes);

            string[] newScenesPaths = new string[] 
            {
                $"{SceneFolderPath}/{SceneNames.ColorMatch}.unity",
                $"{SceneFolderPath}/{SceneNames.CountingFruits}.unity",
                $"{SceneFolderPath}/{SceneNames.ShapeSort}.unity"
            };

            foreach (var path in newScenesPaths)
            {
                bool found = false;
                foreach (var s in originalScenes)
                {
                    if (s.path == path) found = true;
                }

                if (!found)
                {
                    newScenesList.Add(new EditorBuildSettingsScene(path, true));
                }
            }

            EditorBuildSettings.scenes = newScenesList.ToArray();
        }

        // --- Helper methods duplicated from TinyGardenSceneBuilder for simplicity ---
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
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            return text;
        }

        private static Button CreateButton(GameObject parent, string name, string textContent, Vector2 pos, Vector2 anchor, Vector2 size)
        {
            GameObject btnGO = CreatePanel(parent, name, Color.white, pos, anchor, size);
            Button btn = btnGO.AddComponent<Button>();
            CreateText(btnGO, "Text", textContent, 40, Color.black, Vector2.zero, new Vector2(0.5f, 0.5f));
            var childFriendly = btnGO.AddComponent<TinyGarden.UI.ChildFriendlyButton>();
            return btn;
        }
    }
}
#endif
