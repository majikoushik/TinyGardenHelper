using TinyGarden.MiniGames.Shared;

namespace TinyGarden.SaveSystem
{
    public interface ISaveSystem
    {
        GameSaveData Load();
        void Save(GameSaveData data);
        void ResetProgress();
        void MarkActivityCompleted(ActivityId id);
        void UnlockReward(string rewardId);
        void UpdateSettings(SettingsData settings);
        
        GameSaveData CurrentData { get; }
    }
}
