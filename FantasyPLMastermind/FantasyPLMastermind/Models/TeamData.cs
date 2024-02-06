namespace FantasyPLMastermind.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class EntryHistory
    {
        public int @event { get; set; }
        public int points { get; set; }
        public int total_points { get; set; }
        public int rank { get; set; }
        public int rank_sort { get; set; }
        public int overall_rank { get; set; }
        public int percentile_rank { get; set; }
        public int bank { get; set; }
        public int value { get; set; }
        public int event_transfers { get; set; }
        public int event_transfers_cost { get; set; }
        public int points_on_bench { get; set; }
    }

    public class Pick
    {
        public int element { get; set; }
        public int position { get; set; }
        public int multiplier { get; set; }
        public bool is_captain { get; set; }
        public bool is_vice_captain { get; set; }
    }

    public class TeamData
    {
        public object active_chip { get; set; }
        public List<object> automatic_subs { get; set; }
        public EntryHistory entry_history { get; set; }
        public List<Pick> picks { get; set; }
    }


}
