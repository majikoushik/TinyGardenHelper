namespace TinyGarden.MiniGames.Shared
{
    public class MiniGameResult
    {
        public ActivityId ActivityId { get; set; }
        public bool IsSuccess { get; set; }
        public int Attempts { get; set; }
        
        // Future tracking, like how long it took or what specifically they matched
        public string Metadata { get; set; }
    }
}
