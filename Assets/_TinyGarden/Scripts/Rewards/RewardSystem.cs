using System.Collections.Generic;
using UnityEngine;
using TinyGarden.Core;

namespace TinyGarden.Rewards
{
    public class RewardSystem : MonoBehaviour
    {
        public static RewardSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GrantReward(string rewardId)
        {
            if (string.IsNullOrEmpty(rewardId)) return;
            if (GameManager.Instance == null || GameManager.Instance.SaveSystem == null) return;

            var saveData = GameManager.Instance.SaveSystem.CurrentData;
            if (saveData == null) return;

            if (!saveData.UnlockedRewardIds.Contains(rewardId))
            {
                saveData.UnlockedRewardIds.Add(rewardId);
                
                // Keep backward compatibility for AnimalFriendUnlocked flag
                if (rewardId == "animal_benny_bunny")
                {
                    saveData.AnimalFriendUnlocked = true;
                }

                GameManager.Instance.SaveSystem.Save(saveData);
                Debug.Log($"[RewardSystem] Granted and Saved Reward: {rewardId}");
            }
        }

        public void GrantReward(RewardDefinition reward)
        {
            if (reward == null) return;
            GrantReward(reward.rewardId);
        }

        public bool HasReward(string rewardId)
        {
            if (GameManager.Instance == null || GameManager.Instance.SaveSystem == null) return false;
            var saveData = GameManager.Instance.SaveSystem.CurrentData;
            if (saveData == null) return false;

            return saveData.UnlockedRewardIds.Contains(rewardId);
        }
    }
}
