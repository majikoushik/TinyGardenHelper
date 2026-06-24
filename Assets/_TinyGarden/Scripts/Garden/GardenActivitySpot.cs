using UnityEngine;

namespace TinyGarden.Garden
{
    public class GardenActivitySpot : MonoBehaviour
    {
        [SerializeField] private string activityName = "Activity";
        
        public void OnSpotTapped()
        {
            // Placeholder interaction feedback
            Debug.Log($"Coming next: {activityName}!");
            
            // Simple visual feedback: scale pulse
            StartCoroutine(PulseRoutine());
        }

        private System.Collections.IEnumerator PulseRoutine()
        {
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * 1.1f;
            yield return new WaitForSeconds(0.15f);
            transform.localScale = originalScale;
        }
    }
}
