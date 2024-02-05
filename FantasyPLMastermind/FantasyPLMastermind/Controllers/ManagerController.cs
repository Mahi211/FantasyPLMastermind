
using FantasyPLMastermind.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FantasyPLMastermind.Client.Shared;
using System.Xml.Linq;
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
                var league = JsonSerializer.Deserialize<Leagues>(json);
                List<Manager> myManagers = new();

                foreach (var manager in league.standings.results)
                {
                    myManagers.Add(new Manager
                    {
                        id = manager.id,
                        event_total = manager.event_total,
                        player_name = manager.player_name,
                        rank = manager.rank,
                        last_rank = manager.last_rank,
                        rank_sort = manager.rank_sort,
                        total = manager.total,
                        entry = manager.entry,
                        entry_name = manager.entry_name
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
        public async Task<ActionResult<ManagerInfo>> GetManager(int teamId)
        {
            try
            {

                var endpoint = new Uri($"https://fantasy.premierleague.com/api/entry/{teamId}/");
                var client = new HttpClient();
                var result = await client.GetAsync(endpoint);
                var json = await result.Content.ReadAsStringAsync();
                var managers = JsonSerializer.Deserialize<ManagerInfo>(json);




                var myManager = new ManagerInfo {
                    id = managers.id,
                    name = managers.name,
                    summary_event_rank = managers.summary_event_rank,
                    summary_overall_rank = managers.summary_overall_rank,
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
