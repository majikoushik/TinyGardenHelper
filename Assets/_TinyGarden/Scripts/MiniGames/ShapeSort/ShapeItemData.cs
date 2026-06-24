using UnityEngine;

namespace TinyGarden.MiniGames.ShapeSort
{
    [System.Serializable]
    public class ShapeItemData
    {
        public ShapeType shapeType;
        public string displayName;
        public Sprite shapeSprite;
        public Sprite targetSprite;
        public Color placeholderColor;
        public string voicePromptId;
    }
}
