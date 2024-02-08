
using FantasyPLMastermind.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FantasyPLMastermind.Client.Shared;
namespace FantasyPLMastermind.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<List<Manager>>> LeagueData()
        {
            try
            {
                var leagueId = 918532;
                var endpoint = new Uri($"https://fantasy.premierleague.com/api/leagues-classic/{leagueId}/standings/");
                var client = new HttpClient();
                var result = await client.GetAsync(endpoint);
                var json = await result.Content.ReadAsStringAsync();
                var league = JsonSerializer.Deserialize<FplLeague>(json);

                List<Manager> myManagers = new();

                foreach (var manager in league.standings.results)
                {
                    myManagers.Add(new Manager
                    {
                        id = manager.id,
                        gameweekPoints = manager.event_total,
                        playerName = manager.player_name,
                        rank = manager.rank,
                        last_rank = manager.last_rank,
                        rank_sort = manager.rank_sort,
                        totalPoints = manager.total,
                        entry = manager.entry,
                        teamName = manager.entry_name
                    });

                }
                return myManagers;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }
        [HttpGet("{teamId:int}")]
        public async Task<ActionResult<Manager>> GetManagerData(int teamId)
        {
            try
            {
                int leagueId = 918532;
                var endpoint = new Uri($"https://fantasy.premierleague.com/api/entry/{teamId}/");
                var client = new HttpClient();
                var result = await client.GetAsync(endpoint);
                var json = await result.Content.ReadAsStringAsync();
                var managers = JsonSerializer.Deserialize<ManagerInfo>(json);

                var endpoint1 = new Uri($"https://fantasy.premierleague.com/api/leagues-classic/{leagueId}/standings/");
                var client1 = new HttpClient();
                var result1 = await client1.GetAsync(endpoint1);
                var json1 = await result1.Content.ReadAsStringAsync();
                var managers1 = JsonSerializer.Deserialize<FplLeague>(json1);

                var selectedManager = managers1.standings.results.Where(x => x.entry == teamId).FirstOrDefault();

                var myManager = new Manager {
                    id = managers.id,
                    teamName = managers.name,
                    totalPoints = selectedManager.total,
                    gameweekPoints = selectedManager.event_total,
                    rank = selectedManager.rank,
                    last_rank = selectedManager.last_rank,
                    rank_sort = selectedManager.rank_sort,

                };
                
                return myManager;
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            

        }
    }
}
