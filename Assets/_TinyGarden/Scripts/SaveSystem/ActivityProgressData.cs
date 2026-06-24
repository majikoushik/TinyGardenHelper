using System;
using TinyGarden.MiniGames.Shared;

namespace TinyGarden.SaveSystem
{
    [Serializable]
    public sealed class ActivityProgressData
    {
        public ActivityId ActivityId;
        public bool Completed;
        public int SuccessfulCompletions;
        public int GentleRetryCount;
    }
}
