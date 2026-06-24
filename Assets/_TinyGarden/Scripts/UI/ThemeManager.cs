using UnityEngine;

namespace TinyGarden.UI
{
    public class ThemeManager : MonoBehaviour
    {
        public static ThemeManager Instance { get; private set; }

        [SerializeField] private TinyGardenTheme activeTheme;

        public TinyGardenTheme ActiveTheme => activeTheme;

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

        // Future: Methods to dynamically apply colors to registered components
        // For MVP, components can directly reference ThemeManager.Instance.ActiveTheme
    }
}
