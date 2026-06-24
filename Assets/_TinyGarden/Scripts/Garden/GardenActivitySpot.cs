using UnityEngine;
using UnityEngine.UI;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Core;

namespace TinyGarden.Garden
{
    public class GardenActivitySpot : MonoBehaviour
    {
        public ActivityId activityId = ActivityId.None;
        [SerializeField] private Image spotImage;
        
        private bool isCompleted = false;

        public void UpdateVisualState(bool completed)
        {
            isCompleted = completed;
            if (spotImage != null)
            {
                // Placeholder visual growth: Turn bright green if completed
                spotImage.color = completed ? Color.green : Color.white;
            }
        }

        public void OnSpotTapped()
        {
            if (isCompleted)
            {
                Debug.Log($"[Garden] {activityId} is already completed. Plant is happy!");
                StartCoroutine(PulseRoutine());
                return;
            }

            if (activityId == ActivityId.ColorMatch)
            {
                Debug.Log("[Garden] Loading Color Match mini-game...");
                SceneLoader.Instance.LoadScene(SceneNames.ColorMatch);
                return;
            }
            if (activityId == ActivityId.CountingFruits)
            {
                Debug.Log("[Garden] Loading Counting Fruits mini-game...");
                SceneLoader.Instance.LoadScene(SceneNames.CountingFruits);
                return;
            }
            if (activityId == ActivityId.ShapeSort)
            {
                Debug.Log("[Garden] Loading Shape Sort mini-game...");
                SceneLoader.Instance.LoadScene(SceneNames.ShapeSort);
                return;
            }

            Debug.Log($"[Garden] Simulating {activityId} activity completion for MVP testing.");
            
            // SIMULATING MVP COMPLETION FOR OTHERS:
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                GameManager.Instance.SaveSystem.MarkActivityCompleted(activityId);
            }
            
            // Update visual immediately and pulse
            UpdateVisualState(true);
            StartCoroutine(PulseRoutine());
            
            // Notify controller to check for Animal Unlock
            FindObjectOfType<GardenSceneController>()?.CheckProgressState();
        }

        private System.Collections.IEnumerator PulseRoutine()
        {
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * 1.2f;
            yield return new WaitForSeconds(0.15f);
            transform.localScale = originalScale;
        }
    }
}
