namespace FantasyPLMastermind.Client.Shared
{

    public class ManagerInfo
    {


        public int id { get; set; }
        public DateTime joined_time { get; set; }
        public int started_event { get; set; }
        public int? favourite_team { get; set; }
        public string player_first_name { get; set; }
        public string player_last_name { get; set; }
        public int player_region_id { get; set; }
        public string player_region_name { get; set; }
        public string player_region_iso_code_short { get; set; }
        public string player_region_iso_code_long { get; set; }
        public int years_active { get; set; }
        public int summary_overall_points { get; set; }
        public int summary_overall_rank { get; set; }
        public int? summary_event_points { get; set; }
        public int? summary_event_rank { get; set; }
        public int current_event { get; set; }
        public League leagues { get; set; }
        public string name { get; set; }
        public bool name_change_blocked { get; set; }
        public List<int> entered_events { get; set; }
        public object kit { get; set; }
        public int last_deadline_bank { get; set; }
        public int last_deadline_value { get; set; }
        public int last_deadline_total_transfers { get; set; }




        public class Cup
        {
            public object[] matches { get; set; }
            public Status status { get; set; }
            public object cup_league { get; set; }
        }

        public class Status
        {
            public object qualification_event { get; set; }
            public object qualification_numbers { get; set; }
            public object qualification_rank { get; set; }
            public object qualification_state { get; set; }
        }

        public class Classic
        {
            public int id { get; set; }
            public string name { get; set; }
            public string short_name { get; set; }
            public DateTime created { get; set; }
            public bool closed { get; set; }
            public object rank { get; set; }
            public object max_entries { get; set; }
            public string league_type { get; set; }
            public string scoring { get; set; }
            public int? admin_entry { get; set; }
            public int start_event { get; set; }
            public bool entry_can_leave { get; set; }
            public bool entry_can_admin { get; set; }
            public bool entry_can_invite { get; set; }
            public bool has_cup { get; set; }
            public int? cup_league { get; set; }
            public bool? cup_qualified { get; set; }
            public int rank_count { get; set; }
            public int entry_rank { get; set; }
            public int entry_last_rank { get; set; }
            public int entry_percentile_rank { get; set; }
        }

        public class Cup_Matches
        {
            public int id { get; set; }
            public int entry_1_entry { get; set; }
            public string entry_1_name { get; set; }
            public string entry_1_player_name { get; set; }
            public int entry_1_points { get; set; }
            public int entry_1_win { get; set; }
            public int entry_1_draw { get; set; }
            public int entry_1_loss { get; set; }
            public int entry_1_total { get; set; }
            public int entry_2_entry { get; set; }
            public string entry_2_name { get; set; }
            public string entry_2_player_name { get; set; }
            public int entry_2_points { get; set; }
            public int entry_2_win { get; set; }
            public int entry_2_draw { get; set; }
            public int entry_2_loss { get; set; }
            public int entry_2_total { get; set; }
            public bool is_knockout { get; set; }
            public int league { get; set; }
            public int? winner { get; set; }
            public object seed_value { get; set; }
            public int _event { get; set; }
            public object tiebreak { get; set; }
            public bool is_bye { get; set; }
            public string knockout_name { get; set; }
        }

    }
}

