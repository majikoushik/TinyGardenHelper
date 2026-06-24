using System;
using System.Collections.Generic;

namespace TinyGarden.SaveSystem
{
    [Serializable]
    public sealed class GameSaveData
    {
        public int SaveVersion = 1;
        public bool HasCompletedIntro = false;
        public List<ActivityProgressData> Activities = new List<ActivityProgressData>();
        public List<string> UnlockedRewardIds = new List<string>();
        public bool AnimalFriendUnlocked = false;
        public SettingsData Settings = new SettingsData();
    }
}
