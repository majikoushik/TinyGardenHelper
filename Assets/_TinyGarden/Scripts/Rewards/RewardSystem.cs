using System.Collections.Generic;
using UnityEngine;

namespace TinyGarden.Rewards
{
    public class RewardSystem : MonoBehaviour
    {
        public static RewardSystem Instance { get; private set; }

        private List<string> unlockedRewardIds = new List<string>();

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

        public void GrantReward(RewardDefinition reward)
        {
            if (reward == null) return;

            if (!unlockedRewardIds.Contains(reward.rewardId))
            {
                unlockedRewardIds.Add(reward.rewardId);
                Debug.Log($"[RewardSystem] Granted Reward: {reward.rewardId} ({reward.rewardType})");
                
                // Future: Trigger UI celebrations, update save data
            }
        }

        public bool HasReward(string rewardId)
        {
            return unlockedRewardIds.Contains(rewardId);
        }
    }
}
