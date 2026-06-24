using UnityEngine;
using TinyGarden.MiniGames.Shared;

namespace TinyGarden.SaveSystem
{
    public class SaveSystemService : MonoBehaviour
    {
        private ISaveSystem saveSystemImpl;

        public GameSaveData CurrentData => saveSystemImpl?.CurrentData;

        public void Initialize()
        {
            saveSystemImpl = new LocalJsonSaveSystem();
            saveSystemImpl.Load();
            Debug.Log("[SaveSystemService] Initialized. Data loaded.");
        }

        /// <summary>
        /// Saves the provided GameSaveData to disk immediately.
        /// Use this when callers (RewardSystem, SettingsController) have already mutated CurrentData.
        /// </summary>
        public void Save(GameSaveData data)
        {
            saveSystemImpl?.Save(data);
        }

        public void SaveData()
        {
            if (saveSystemImpl != null && CurrentData != null)
            {
                saveSystemImpl.Save(CurrentData);
            }
        }

        public void ResetProgress()
        {
            saveSystemImpl?.ResetProgress();
        }

        public void MarkActivityCompleted(ActivityId id)
        {
            saveSystemImpl?.MarkActivityCompleted(id);
        }

        public void UnlockReward(string rewardId)
        {
            saveSystemImpl?.UnlockReward(rewardId);
        }

        public void UpdateSettings(SettingsData settings)
        {
            saveSystemImpl?.UpdateSettings(settings);
        }
    }
}
