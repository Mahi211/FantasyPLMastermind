using FantasyPLMastermind.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FantasyPLMastermind.Client.Shared;


namespace FantasyPLMastermind.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PlayerController : ControllerBase
    {

        [HttpGet]

        public async Task<ActionResult<List<Player>>> GetPlayerData()
        {
            try
            {
                LeagueData? players = await GetAllPlayerData();               


                List<Player> playersData = new();
                foreach (var player in players.elements)
                {
                    float price = player.now_cost;
                    float playerPrice = (float)player.now_cost / 10;
                    var fullName = $"{player.first_name} {player.second_name}";
                    var myTier = player.ep_next;
                    float tierRank = float.Parse(myTier.Replace(".", ","));

                    var selectedTeam = players.teams.FirstOrDefault(x => x.id == player.team);
                    playersData.Add(new Player
                    {
                        id = player.id,
                        web_name = player.web_name,
                        position = (Position)player.element_type,
                        first_name = player.first_name,
                        second_name = player.second_name,
                        event_points = player.event_points,
                        total_points = player.total_points,
                        ep_this = player.ep_this,
                        ep_next = tierRank,
                        now_cost = playerPrice,
                        team_code = player.team_code,
                        selected_by_percent = player.selected_by_percent,
                        teamName = selectedTeam.name
                    });
                }
                return playersData;
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        

        [HttpGet("{teamId}")]
        public async Task<ActionResult<List<Player>>> PlayersForTeam(int teamId)
        {
            var endpoint = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var json = await result.Content.ReadAsStringAsync();
            var players = JsonSerializer.Deserialize<LeagueData>(json);
            var CurrentGameWeek = 0;

            foreach (var gameweek in players.events)
            {
                if (gameweek.is_current)
                {
                    CurrentGameWeek += gameweek.id;
                }



            }

            TeamData? managers = await GetCurrentGameWeekData(teamId, CurrentGameWeek);

            Manager manager = new Manager
            {
                bank = managers.entry_history.bank,
                value = managers.entry_history.value,
                event_transfers = managers.entry_history.event_transfers,
                event_transfers_cost = managers.entry_history.event_transfers_cost,
                points_on_bench = managers.entry_history.points_on_bench

            };

            List<Player> playersData = new();


            foreach (var player in managers.picks)
            {
                var selectedPlayer = players.elements.FirstOrDefault(x => x.id == player.element);
                var selectedTeam = players.teams.FirstOrDefault(x => x.id == selectedPlayer.team);
                var fullName = $"{selectedPlayer.first_name} {selectedPlayer.second_name}";
                float price = selectedPlayer.now_cost;
                float playerPrice = price / 10;


                playersData.Add(new Player
                {
                    id = selectedPlayer.id,
                    first_name = selectedPlayer.first_name,
                    second_name = selectedPlayer.second_name,
                    event_points = selectedPlayer.event_points,
                    total_points = selectedPlayer.total_points,
                    position = (Position)selectedPlayer.element_type,
                    web_name = fullName,
                    teamName = selectedTeam.name,
                    now_cost = playerPrice,
                    transfers_in = selectedPlayer.transfers_in,
                    selected_by_percent = selectedPlayer.selected_by_percent




                });
            }




            return playersData;

        }

        [HttpGet("myplayer/{playerId}")]

        public async Task<ActionResult<Player>> GetPlayer(int playerId)
        {
            try
            {


                LeagueData? players = await GetAllPlayerData();

                var player = players.elements.Where(x => x.id == playerId).FirstOrDefault();
                float price = player.now_cost;
                float playerPrice = (float)player.now_cost / 10;
                var fullName = $"{player.first_name} {player.second_name}";
                var myTier = player.ep_next;
                float tierRank = float.Parse(myTier.Replace(".", ","));
                var selectedTeam = players.teams.FirstOrDefault(x => x.id == player.team);
                // string photoName = Path.GetFileNameWithoutExtension(player.photo); Chat Gpts Lösning
                string mySolution = player.photo.Replace("jpg", "png"); // min lösning




                Player myPlayer = new Player
                {
                    id = player.id,
                    web_name = player.web_name,
                    position = (Position)player.element_type,
                    first_name = player.first_name,
                    second_name = player.second_name,
                    event_points = player.event_points,
                    total_points = player.total_points,
                    ep_this = player.ep_this,
                    ep_next = tierRank,
                    now_cost = playerPrice,
                    team_code = player.team_code,
                    selected_by_percent = player.selected_by_percent,
                    teamName = selectedTeam.name,
                    photo = mySolution,

                };
                return myPlayer;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        private static async Task<TeamData?> GetCurrentGameWeekData(int teamId, int CurrentGameWeek)
        {
            var endpoint = new Uri($"https://fantasy.premierleague.com/api/entry/{teamId}/event/{CurrentGameWeek}/picks/");
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var json = await result.Content.ReadAsStringAsync();
            var managers = JsonSerializer.Deserialize<TeamData>(json);
            return managers;
        }

        private static async Task<LeagueData> GetAllPlayerData()
        {
            var endpoint = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var json = await result.Content.ReadAsStringAsync();
            var players = JsonSerializer.Deserialize<LeagueData>(json);
            return players;
        }



    }
}
