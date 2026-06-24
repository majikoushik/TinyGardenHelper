using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TinyGarden.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [SerializeField] private float fadeDuration = 0.5f;
        
        private Canvas fadeCanvas;
        private Image fadeImage;
        private bool isLoading = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                CreateFadeUI();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void CreateFadeUI()
        {
            GameObject canvasObj = new GameObject("FadeCanvas");
            canvasObj.transform.SetParent(transform, false);
            fadeCanvas = canvasObj.AddComponent<Canvas>();
            fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            fadeCanvas.sortingOrder = 9999; // Ensure it renders on top
            
            GameObject imageObj = new GameObject("FadeImage");
            imageObj.transform.SetParent(canvasObj.transform, false);
            fadeImage = imageObj.AddComponent<Image>();
            fadeImage.color = new Color(0, 0, 0, 0); // Transparent black
            
            RectTransform rect = fadeImage.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
        }

        public void LoadScene(string sceneName)
        {
            if (isLoading) return;
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            isLoading = true;

            // Fade to black
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Clamp01(timer / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            fadeImage.color = new Color(0, 0, 0, 1);

            // Load scene
            yield return SceneManager.LoadSceneAsync(sceneName);

            // Fade to clear
            timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = 1f - Mathf.Clamp01(timer / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            fadeImage.color = new Color(0, 0, 0, 0);
            isLoading = false;
        }
    }
}
