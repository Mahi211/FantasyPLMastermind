namespace FantasyPLMastermind.Client.Shared
{
    public class Manager
    {               
            public int id { get; set; }
            public int? gameweekPoints { get; set; }
            public string playerName { get; set; }
            public int rank { get; set; }
            public int last_rank { get; set; }
            public int rank_sort { get; set; }
            public int totalPoints { get; set; }
            public int entry { get; set; }
            public string teamName { get; set; }
            public int CurrentGameweek { get; set; }
            
            public float bank { get; set; }
            public float value { get; set; }
            public int event_transfers { get; set; }
            public int event_transfers_cost { get; set; }
            public int points_on_bench { get; set; }





    }

}
