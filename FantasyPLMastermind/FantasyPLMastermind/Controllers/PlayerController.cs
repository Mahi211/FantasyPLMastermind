using FantasyPLMastermind.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FantasyPLMastermind.Client.Shared;
using FantasyPLMastermind.Shared;
using System.Numerics;
using static FantasyPLMastermind.Client.Shared.Player;

namespace FantasyPLMastermind.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PlayerController : ControllerBase
    {

        [HttpGet]

        public async Task<ActionResult<List<Player>>> GetPlayerData()
        {
            var endpoint = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var json = await result.Content.ReadAsStringAsync();
            var players = JsonSerializer.Deserialize<LeagueData>(json);


            List<Player> playersData = new();
            foreach (var player in players.elements)
            {
                playersData.Add(new Player
                {
                    id = player.id,
                    first_name = player.first_name,
                    second_name = player.second_name,
                    event_points = player.event_points,
                    total_points = player.total_points

                });
            }




            return playersData;

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




            var endpoint1 = new Uri($"https://fantasy.premierleague.com/api/entry/{teamId}/event/{CurrentGameWeek}/picks/");
            var client1 = new HttpClient();
            var result1 = await client1.GetAsync(endpoint1);
            var json1 = await result1.Content.ReadAsStringAsync();
            var managers1 = JsonSerializer.Deserialize<TeamData>(json1);

            List<Player> playersData = new();

            foreach (var player in managers1.picks)
            {
                var selectedPlayer = players.elements.FirstOrDefault(x => x.id == player.element);
                var selectedTeam = players.teams.FirstOrDefault(x => x.id == selectedPlayer.team);
                
                playersData.Add(new Player
                {
                    id = selectedPlayer.id,
                    first_name = selectedPlayer.first_name,
                    second_name = selectedPlayer.second_name,
                    event_points = selectedPlayer.event_points,
                    total_points = selectedPlayer.total_points,
                    position = (Position)selectedPlayer.element_type,
                    web_name = selectedPlayer.web_name,
                    teamName = selectedTeam.name
                   
                });
            }




            return playersData;

        }




    }
}
