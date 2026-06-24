using UnityEngine;
using TinyGarden.Rewards;

namespace TinyGarden.MiniGames.Shared
{
    [CreateAssetMenu(fileName = "ActivityDefinition", menuName = "Tiny Garden/MiniGames/Activity Definition")]
    public class ActivityDefinition : ScriptableObject
    {
        public ActivityId activityId;
        public Sprite displayIcon;
        public string plantId;
        
        [Header("Rewards")]
        public string successVoiceLineId;
        public RewardDefinition successReward;
    }
}
