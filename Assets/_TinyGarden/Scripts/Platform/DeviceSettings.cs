using UnityEngine;

namespace TinyGarden.Platform
{
    public static class DeviceSettings
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void ApplySettings()
        {
            Application.targetFrameRate = 60;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.orientation = ScreenOrientation.Portrait;
            
            Debug.Log("[DeviceSettings] Applied mobile orientation and framerate limits.");
        }
    }
}
