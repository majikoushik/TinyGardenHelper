using System;

namespace TinyGarden.SaveSystem
{
    [Serializable]
    public sealed class SettingsData
    {
        public bool MusicEnabled = true;
        public bool SfxEnabled = true;
        public bool VoiceEnabled = true;
        public bool SensorySafeModeEnabled = false;
    }
}
