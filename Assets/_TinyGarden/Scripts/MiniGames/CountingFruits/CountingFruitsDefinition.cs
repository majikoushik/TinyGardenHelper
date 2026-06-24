using UnityEngine;
using TinyGarden.Core;
using TinyGarden.MiniGames.Shared; // ActivityId is defined here

namespace TinyGarden.MiniGames.CountingFruits
{
    [System.Serializable]
    public class CountingRoundData
    {
        public int targetCount;
        public string voicePromptId;
    }

    [CreateAssetMenu(fileName = "CountingFruitsDefinition", menuName = "TinyGarden/MiniGames/CountingFruitsDefinition")]
    public class CountingFruitsDefinition : ScriptableObject
    {
        public ActivityId activityId = ActivityId.CountingFruits;
        public CountingRoundData[] rounds = new CountingRoundData[]
        {
            new CountingRoundData { targetCount = 1, voicePromptId = "prompt_count_1" },
            new CountingRoundData { targetCount = 3, voicePromptId = "prompt_count_3" },
            new CountingRoundData { targetCount = 5, voicePromptId = "prompt_count_5" }
        };
    }
}
