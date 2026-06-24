using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace TinyGarden.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }
        
        [SerializeField] private float fadeDuration = 0.5f;
        
        private Canvas fadeCanvas;
        private Image fadeImage;
        private bool isTransitioning = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                CreateFadeCanvas();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void CreateFadeCanvas()
        {
            GameObject canvasObj = new GameObject("FadeCanvas");
            canvasObj.transform.SetParent(transform);
            
            fadeCanvas = canvasObj.AddComponent<Canvas>();
            fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            fadeCanvas.sortingOrder = 999; // Topmost
            
            GameObject imgObj = new GameObject("FadeImage");
            imgObj.transform.SetParent(canvasObj.transform, false);
            fadeImage = imgObj.AddComponent<Image>();
            fadeImage.color = new Color(0, 0, 0, 0); // Transparent black
            
            RectTransform rect = fadeImage.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            
            fadeImage.raycastTarget = false;
        }

        public void LoadScene(string sceneName)
        {
            if (isTransitioning) return;
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            isTransitioning = true;
            fadeImage.raycastTarget = true;

            bool sensorySafe = IsSensorySafeModeEnabled();

            if (!sensorySafe)
            {
                // Fade out
                float t = 0;
                while (t < fadeDuration)
                {
                    t += Time.deltaTime;
                    fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / fadeDuration));
                    yield return null;
                }
            }
            else
            {
                fadeImage.color = Color.black;
                yield return null;
            }

            // Load scene
            yield return SceneManager.LoadSceneAsync(sceneName);

            if (!sensorySafe)
            {
                // Fade in
                float t = fadeDuration;
                while (t > 0)
                {
                    t -= Time.deltaTime;
                    fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / fadeDuration));
                    yield return null;
                }
            }

            fadeImage.color = new Color(0, 0, 0, 0);
            fadeImage.raycastTarget = false;
            isTransitioning = false;
        }

        private bool IsSensorySafeModeEnabled()
        {
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                var data = GameManager.Instance.SaveSystem.CurrentData;
                if (data != null && data.Settings != null)
                {
                    return data.Settings.SensorySafeModeEnabled;
                }
            }
            return false;
        }
    }
}
