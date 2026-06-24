using UnityEngine;
using UnityEngine.UI;
using TinyGarden.Core;

namespace TinyGarden.Garden
{
    public class GardenSceneController : MonoBehaviour
    {
        [SerializeField] private Text animalStatusText;

        private void Start()
        {
            CheckProgressState();
        }

        public void CheckProgressState()
        {
            if (GameManager.Instance == null || GameManager.Instance.SaveSystem == null) return;

            var saveData = GameManager.Instance.SaveSystem.CurrentData;
            if (saveData == null) return;

            // Update all spots visually based on save data
            var spots = FindObjectsOfType<GardenActivitySpot>();
            foreach (var spot in spots)
            {
                bool completed = saveData.Activities.Exists(a => a.ActivityId == spot.activityId && a.Completed);
                spot.UpdateVisualState(completed);
            }

            // Check Animal Friend Unlock
            if (saveData.AnimalFriendUnlocked && animalStatusText != null)
            {
                animalStatusText.text = "Animal Friend\n(Unlocked!)";
                animalStatusText.color = new Color(0.8f, 0.4f, 0.8f); // A happy purple
            }
        }

        public void OnHomeClicked()
        {
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
        
        // Developer Tool for Testing
        public void OnResetProgressClicked()
        {
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                GameManager.Instance.SaveSystem.ResetProgress();
                if (animalStatusText != null)
                {
                    animalStatusText.text = "Animal Friend\n(Locked)";
                    animalStatusText.color = Color.black;
                }
                CheckProgressState();
                Debug.Log("[Developer] Progress Reset.");
            }
        }
    }
}
