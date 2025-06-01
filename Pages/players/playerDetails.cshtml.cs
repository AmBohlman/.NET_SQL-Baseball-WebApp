using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages.players
{
    public class playerDetailsModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<playerDetailsModel> _logger;

        public playerDetailsModel(clsDatabase clsDatabaseService, ILogger<playerDetailsModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
        }

        // Holds the DataRow for the player's details
        public DataRow? PlayerDetail { get; private set; }
        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles GET request to fetch and display details for a specific player
        public async Task OnGetAsync(int id)
        {
            _logger.LogInformation("Fetching player details for PlayerID: {PlayerID}.", id);
            try
            {
                // Attempt to fetch player details from database
                DataSet dsPlayer = await _clsDatabase.getPlayerInfo(id);

                if (dsPlayer != null && dsPlayer.Tables.Count > 0 && dsPlayer.Tables[0].Rows.Count > 0)
                {
                    PlayerDetail = dsPlayer.Tables[0].Rows[0];
                    _logger.LogInformation("Successfully retrieved details for PlayerID: {PlayerID}.", id);
                }
                else
                {
                    Message = $"No details found for Player ID {id}.";
                    _logger.LogWarning("No data returned by getPlayerInfo for PlayerID: {PlayerID}.", id);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching details for PlayerID: {PlayerID}.", id);
                Message = "An error occurred while trying to load player details. Please try again later.";
                PlayerDetail = null;
            }
        }
    }
}