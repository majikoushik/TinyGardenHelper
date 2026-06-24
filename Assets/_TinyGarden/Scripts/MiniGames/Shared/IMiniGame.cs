using System;

namespace TinyGarden.MiniGames.Shared
{
    public interface IMiniGame
    {
        ActivityId GetActivityId();
        void StartGame();
        void EndGame(bool success);
        event Action<MiniGameResult> OnGameCompleted;
    }
}
