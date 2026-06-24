using UnityEngine;
using TinyGarden.Core;
using TinyGarden.MiniGames.Shared; // ActivityId is defined here
using TinyGarden.Rewards;

namespace TinyGarden.Garden
{
    public class GardenProgressPresenter : MonoBehaviour
    {
        [SerializeField] private RewardCelebrationController celebrationController;
        [SerializeField] private AnimalFriendController animalFriend;

        private void Start()
        {
            EvaluateGardenState();
        }

        public void EvaluateGardenState()
        {
            if (GameManager.Instance == null || GameManager.Instance.SaveSystem == null) return;
            var saveData = GameManager.Instance.SaveSystem.CurrentData;
            if (saveData == null) return;

            // Update individual plants
            var spots = FindObjectsOfType<GardenActivitySpot>();
            int completedCount = 0;
            foreach (var spot in spots)
            {
                bool completed = saveData.Activities.Exists(a => a.ActivityId == spot.activityId && a.Completed);
                spot.UpdateVisualState(completed);
                if (completed && spot.activityId != ActivityId.None)
                {
                    completedCount++;
                }
            }

            // Explicitly check for MVP completion rather than completedCount
            bool colorMatchDone = saveData.Activities.Exists(a => a.ActivityId == ActivityId.ColorMatch && a.Completed);
            bool countingDone = saveData.Activities.Exists(a => a.ActivityId == ActivityId.CountingFruits && a.Completed);
            bool shapeSortDone = saveData.Activities.Exists(a => a.ActivityId == ActivityId.ShapeSort && a.Completed);

            bool allMvpComplete = colorMatchDone && countingDone && shapeSortDone;

            if (allMvpComplete)
            {
                if (!RewardSystem.Instance.HasReward("animal_benny_bunny"))
                {
                    // Trigger the big celebration sequence!
                    if (celebrationController != null)
                    {
                        celebrationController.PlayAnimalUnlockSequence(
                            "animal_benny_bunny", 
                            "sticker_garden_helper_star", 
                            OnCelebrationComplete
                        );
                    }
                    else
                    {
                        // Fallback if no celebration controller
                        RewardSystem.Instance.GrantReward("animal_benny_bunny");
                        RewardSystem.Instance.GrantReward("sticker_garden_helper_star");
                        OnCelebrationComplete();
                    }
                }
                else
                {
                    // Already unlocked, just show the animal
                    if (animalFriend != null)
                    {
                        animalFriend.ShowAnimal();
                    }
                }
            }
            else
            {
                if (animalFriend != null)
                {
                    animalFriend.HideAnimal();
                }
            }
        }

        private void OnCelebrationComplete()
        {
            if (animalFriend != null)
            {
                animalFriend.ShowAnimal(true); // show with a pop/bounce
            }
        }
    }
}
