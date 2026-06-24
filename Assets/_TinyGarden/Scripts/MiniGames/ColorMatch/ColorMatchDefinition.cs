using UnityEngine;
using TinyGarden.MiniGames.Shared;

namespace TinyGarden.MiniGames.ColorMatch
{
    [System.Serializable]
    public class ColorMatchItemData
    {
        public string colorId;
        public Color colorValue;
        public Sprite itemSprite;
        public Sprite targetSprite;
    }

    [CreateAssetMenu(fileName = "ColorMatchDefinition", menuName = "TinyGarden/MiniGames/ColorMatchDefinition")]
    public class ColorMatchDefinition : ScriptableObject
    {
        public ActivityId activityId = ActivityId.ColorMatch;
        public string voicePromptId = "prompt_match_colors";
        public ColorMatchItemData[] items;
    }
}
