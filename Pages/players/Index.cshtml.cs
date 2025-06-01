using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages.players
{
    public class IndexModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(clsDatabase clsDatabaseService, ILogger<IndexModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
        }

        // Holds retrieved player data
        public DataTable? PlayersData { get; private set; }
        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles GET request to fetch and display player list
        public async Task OnGetAsync()
        {
            _logger.LogInformation("Fetching players list.");
            try
            {
                // Attempt to fetch player data from database
                DataSet dsPlayers = await _clsDatabase.getPlayerID();

                if (dsPlayers != null && dsPlayers.Tables.Count > 0 && dsPlayers.Tables[0].Rows.Count > 0)
                {
                    PlayersData = dsPlayers.Tables[0];
                    _logger.LogInformation("Successfully retrieved {RowCount} players.", PlayersData.Rows.Count);
                }
                else
                {
                    Message = "No players found in the league.";
                    _logger.LogWarning("No data returned by getPlayerID for players list page.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the players list.");
                Message = "An error occurred while trying to load the players list. Please try again later.";
                PlayersData = null; 
            }
        }
    }
}