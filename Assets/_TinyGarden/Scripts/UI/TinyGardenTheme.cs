using UnityEngine;

namespace TinyGarden.UI
{
    [CreateAssetMenu(fileName = "TinyGardenTheme", menuName = "Tiny Garden/UI/Theme")]
    public class TinyGardenTheme : ScriptableObject
    {
        [Header("Backgrounds")]
        public Color primaryBackground = new Color(0.85f, 0.95f, 0.85f);
        public Color secondaryBackground = new Color(0.95f, 0.9f, 0.8f);

        [Header("UI Panels")]
        public Color panelColor = new Color(1f, 1f, 1f, 0.95f);
        
        [Header("Buttons")]
        public Color buttonNormal = Color.white;
        public Color buttonPositive = new Color(0.3f, 0.8f, 0.3f);
        public Color buttonWarning = new Color(0.9f, 0.4f, 0.3f);

        [Header("Text")]
        public Color textDark = new Color(0.2f, 0.3f, 0.2f);
        public Color textLight = Color.white;
    }
}
