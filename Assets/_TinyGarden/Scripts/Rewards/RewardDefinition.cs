using UnityEngine;
using TinyGarden.Audio;

namespace TinyGarden.Rewards
{
    [CreateAssetMenu(fileName = "RewardDefinition", menuName = "Tiny Garden/Rewards/Reward Definition")]
    public class RewardDefinition : ScriptableObject
    {
        public string rewardId;
        public RewardType rewardType;
        public Sprite displaySprite;
        public string sfxCue = AudioCue.Success;
        public string voiceLineCue;
        
        [TextArea]
        public string unlockConditionDescription;
    }
}
