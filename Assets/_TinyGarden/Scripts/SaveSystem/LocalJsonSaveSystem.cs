using System;
using System.IO;
using UnityEngine;
using TinyGarden.MiniGames.Shared;

namespace TinyGarden.SaveSystem
{
    public class LocalJsonSaveSystem : ISaveSystem
    {
        private readonly string saveFilePath;
        private readonly string backupFilePath;
        
        public GameSaveData CurrentData { get; private set; }

        public LocalJsonSaveSystem(string customPath = null)
        {
            string basePath = string.IsNullOrEmpty(customPath) ? Application.persistentDataPath : customPath;
            saveFilePath = Path.Combine(basePath, "save.json");
            backupFilePath = Path.Combine(basePath, "save.json.bak");
        }

        public GameSaveData Load()
        {
            if (!File.Exists(saveFilePath))
            {
                CurrentData = new GameSaveData();
                return CurrentData;
            }

            try
            {
                string json = File.ReadAllText(saveFilePath);
                CurrentData = JsonUtility.FromJson<GameSaveData>(json);
                
                if (CurrentData == null)
                {
                    throw new Exception("Deserialized data is null.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to load save file. Creating backup and starting fresh. Error: {e.Message}");
                if (File.Exists(saveFilePath))
                {
                    File.Copy(saveFilePath, backupFilePath, true);
                }
                CurrentData = new GameSaveData();
            }

            return CurrentData;
        }

        public void Save(GameSaveData data)
        {
            CurrentData = data ?? new GameSaveData();
            try
            {
                string json = JsonUtility.ToJson(CurrentData, true);
                File.WriteAllText(saveFilePath, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to write save file. Error: {e.Message}");
            }
        }

        public void ResetProgress()
        {
            CurrentData = new GameSaveData();
            Save(CurrentData);
            Debug.Log("[SaveSystem] Progress reset completely.");
        }

        public void MarkActivityCompleted(ActivityId id)
        {
            if (CurrentData == null) Load();

            var activity = CurrentData.Activities.Find(a => a.ActivityId == id);
            if (activity == null)
            {
                activity = new ActivityProgressData { ActivityId = id };
                CurrentData.Activities.Add(activity);
            }

            activity.Completed = true;
            activity.SuccessfulCompletions++;
            Save(CurrentData);
        }

        public void UnlockReward(string rewardId)
        {
            if (CurrentData == null) Load();

            if (!CurrentData.UnlockedRewardIds.Contains(rewardId))
            {
                CurrentData.UnlockedRewardIds.Add(rewardId);
                Save(CurrentData);
            }
        }

        public void UpdateSettings(SettingsData settings)
        {
            if (CurrentData == null) Load();
            CurrentData.Settings = settings;
            Save(CurrentData);
        }


    }
}
