using UnityEngine;
using TinyGarden.Core;
using TinyGarden.MiniGames.Shared; // ActivityId is defined here

namespace TinyGarden.MiniGames.ShapeSort
{
    [CreateAssetMenu(fileName = "ShapeSortDefinition", menuName = "TinyGarden/MiniGames/ShapeSortDefinition")]
    public class ShapeSortDefinition : ScriptableObject
    {
        public ActivityId activityId = ActivityId.ShapeSort;
        public string introVoicePromptId = "prompt_find_homes";
        public ShapeItemData[] shapes;
    }
}
