namespace FantasyPLMastermind.Client.Shared
{
    public class Transfer
    {
        public string? playerIn { get; set; }
        public float? PlayerInCost { get; set; }
        public string? PlayerOut { get; set; }
        public float? PlayerOutCost { get; set; }
        public int? entry { get; set; }
        
        public int? gameWeek { get; set; }
        public DateTime? time { get; set; }
    }
}
