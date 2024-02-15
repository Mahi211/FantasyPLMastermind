
using FantasyPLMastermind.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FantasyPLMastermind.Client.Shared;
using FantasyPLMastermind.Models;
using System.Transactions;
using Microsoft.Extensions.Localization;
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
                int CurrentGameWeek = managers.current_event;
                TeamData? currentWeekData = await GetCurrentGameWeekData(teamId, CurrentGameWeek);
                int myValue = currentWeekData.entry_history.value;
                float value = myValue / 10.0f;
                float bank = currentWeekData.entry_history.bank / 10.0f; // converting bank to float so its 104,1 instead of 1041 for example


                var myManager = new Manager
                {
                    id = teamId,
                    teamName = managers.name,
                    bank = bank,
                    event_transfers = currentWeekData.entry_history.event_transfers,
                    value = value,
                    event_transfers_cost = currentWeekData.entry_history.event_transfers_cost,
                    points_on_bench = currentWeekData.entry_history.points_on_bench,
                    CurrentGameweek = CurrentGameWeek,
                    gameweekPoints = managers.summary_event_points,
                };

                return myManager;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        [HttpGet("transfers/{teamId:int}")]
        public async Task<ActionResult<List<Transfer>>> GetTransferData(int teamId)
        {
            try
            {
                var endpoint = new Uri($"https://fantasy.premierleague.com/api/entry/{teamId}/transfers/");
                var client = new HttpClient();
                var result = await client.GetAsync(endpoint);
                var json = await result.Content.ReadAsStringAsync();
                var transfers = JsonSerializer.Deserialize<List<TransferData>>(json);
                LeagueData? players = await GetEverything();

                List<Transfer> myTransfers = new();
                foreach (var transfer in transfers)
                {
                    var myPlayer = players.elements.FirstOrDefault(x => x.id == transfer.element_in);
                    var myPlayerOut = players.elements.FirstOrDefault(x => x.id == transfer.element_out);
                    float costIn = transfer.element_in_cost;
                    float MyCostIn = costIn / 10.0f;
                    float costOut = transfer.element_out_cost;
                    float MyCostOut = costOut / 10.0f;

                    myTransfers.Add(new Transfer
                    {
                        playerIn = myPlayer.web_name,
                        PlayerInCost = MyCostIn,
                        PlayerOut = myPlayerOut.web_name,
                        PlayerOutCost = MyCostOut,
                        time = transfer.time,
                        gameWeek = transfer.@event

                    });
                }
                return myTransfers;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

        private static async Task<LeagueData?> GetEverything()
        {
            var endpoint = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var json = await result.Content.ReadAsStringAsync();
            var players = JsonSerializer.Deserialize<LeagueData>(json);
            return players;
        }
        /*
        private static async Task<List<Transfer?>> GetTransferData(int teamId)
        {
            var endpoint = new Uri($"https://fantasy.premierleague.com/api/entry/{teamId}/transfers/");
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            var json = await result.Content.ReadAsStringAsync();
            var transfers = JsonSerializer.Deserialize<List<TransferData>>(json);
            List<Transfer> myTransfers = new();
            foreach (var transfer in transfers) 
            {
                myTransfers.Add(new Transfer
                {
                    playerIn = transfer.element_in,
                    PlayerInCost = transfer.element_in_cost,
                    PlayerOut = transfer.element_out_cost,
                    PlayerOutCost = transfer.element_out_cost,
                    time = transfer.time,
                    @event = transfer.@event

                });
            }
            return myTransfers; 
            
        } */



    }
}       
