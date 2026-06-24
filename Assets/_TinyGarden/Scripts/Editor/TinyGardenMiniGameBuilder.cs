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
            safeArea.AddComponent<TinyGarden.Platform.SafeAreaFitter>();

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
            bool foundColorMatch = false;

            string cmPath = $"{SceneFolderPath}/{SceneNames.ColorMatch}.unity";

            foreach (var s in originalScenes)
            {
                if (s.path == cmPath) foundColorMatch = true;
            }

            if (!foundColorMatch)
            {
                var newScenes = new EditorBuildSettingsScene[originalScenes.Length + 1];
                System.Array.Copy(originalScenes, newScenes, originalScenes.Length);
                newScenes[originalScenes.Length] = new EditorBuildSettingsScene(cmPath, true);
                EditorBuildSettings.scenes = newScenes;
            }
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
