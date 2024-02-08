﻿namespace FantasyPLMastermind.Client.Shared
{
    public class Player
    {

        public int? chance_of_playing_next_round { get; set; }
        public int? chance_of_playing_this_round { get; set; }
        public int code { get; set; }
        public int cost_change_event { get; set; }
        public int cost_change_event_fall { get; set; }
        public int cost_change_start { get; set; }
        public int cost_change_start_fall { get; set; }
        public int dreamteam_count { get; set; }
        public int element_type { get; set; }
        public string ep_next { get; set; }
        public string ep_this { get; set; }
        public int event_points { get; set; }
        public string first_name { get; set; }
        public string form { get; set; }
        public int id { get; set; }
        public bool in_dreamteam { get; set; }
        public string news { get; set; }
        public DateTime? news_added { get; set; }
        public float now_cost { get; set; }
        public string photo { get; set; }
        public string points_per_game { get; set; }
        public string second_name { get; set; }
        public string selected_by_percent { get; set; }
        public bool special { get; set; }
        public object squad_number { get; set; }
        public string status { get; set; }
        public int team { get; set; }
        public int team_code { get; set; }
        public int total_points { get; set; }
        public int transfers_in { get; set; }
        public int transfers_in_event { get; set; }
        public int transfers_out { get; set; }
        public int transfers_out_event { get; set; }
        public string value_form { get; set; }
        public string value_season { get; set; }
        public string web_name { get; set; }
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public string influence { get; set; }
        public string creativity { get; set; }
        public string threat { get; set; }
        public string ict_index { get; set; }
        public int starts { get; set; }
        public string expected_goals { get; set; }
        public string expected_assists { get; set; }
        public string expected_goal_involvements { get; set; }
        public string expected_goals_conceded { get; set; }
        public int influence_rank { get; set; }
        public int influence_rank_type { get; set; }
        public int creativity_rank { get; set; }
        public int creativity_rank_type { get; set; }
        public int threat_rank { get; set; }
        public int threat_rank_type { get; set; }
        public int ict_index_rank { get; set; }
        public int ict_index_rank_type { get; set; }
        public int? corners_and_indirect_freekicks_order { get; set; }
        public string corners_and_indirect_freekicks_text { get; set; }
        public int? direct_freekicks_order { get; set; }
        public string direct_freekicks_text { get; set; }
        public int? penalties_order { get; set; }
        public string penalties_text { get; set; }
        public double expected_goals_per_90 { get; set; }
        public double saves_per_90 { get; set; }
        public double expected_assists_per_90 { get; set; }
        public double expected_goal_involvements_per_90 { get; set; }
        public double expected_goals_conceded_per_90 { get; set; }
        public double goals_conceded_per_90 { get; set; }
        public int now_cost_rank { get; set; }
        public int now_cost_rank_type { get; set; }
        public int form_rank { get; set; }
        public int form_rank_type { get; set; }
        public int points_per_game_rank { get; set; }
        public int points_per_game_rank_type { get; set; }
        public int selected_rank { get; set; }
        public int selected_rank_type { get; set; }
        public double starts_per_90 { get; set; }
        public double clean_sheets_per_90 { get; set; }
        public Position position { get; set; }

        public string teamName { get; set; }
    }



        public enum Position
        {
            Goalkeeper = 1,
            Defender,
            Midfielder,
            Forward
        }
    
}
