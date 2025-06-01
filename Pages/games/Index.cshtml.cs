using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OIBaseballLeagueAPI.Pages.games
{
    // PageModel for displaying game schedule and results
    public class IndexModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<IndexModel> _logger;

        // Holds the retrieved game data
        public DataTable? GameData { get; private set; }
        // Defines the column headers for the game table
        public Dictionary<string, string> GameHeaders { get; private set; }
        
        // For displaying status or error messages to the user
        public string Message { get; set; } = string.Empty;

        public IndexModel(clsDatabase clsDatabaseService, ILogger<IndexModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
            GameHeaders = new Dictionary<string, string>();
        }

        // Handles the GET request to fetch and display game data
        public async Task OnGetAsync()
        {
            _logger.LogInformation("Fetching game data for Games page.");
            
            try
            {
                // Retrieve game data from the database
                DataSet dsGames = await _clsDatabase.getGames();

                if (dsGames != null && dsGames.Tables.Count > 0 && dsGames.Tables[0].Rows.Count > 0)
                {
                    GameData = dsGames.Tables[0];
                    _logger.LogInformation("Successfully retrieved {RowCount} rows for game data.", GameData.Rows.Count);
                    
                    // Define headers
                    GameHeaders.Add("datePlayed", "Date");
                    GameHeaders.Add("gameLocation", "Location");
                    GameHeaders.Add("teamName", "Home Team");    
                    GameHeaders.Add("city", "Home City");        
                    GameHeaders.Add("homeScore", "Home Score");
                    GameHeaders.Add("teamName1", "Away Team");  
                    GameHeaders.Add("city1", "Away City");      
                    GameHeaders.Add("awayScore", "Away Score");
                }
                else
                {
                    Message = "No game data currently available.";
                    _logger.LogWarning("No data returned by stored procedure for game data page.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching game data.");
                Message = "An error occurred while trying to load game data. Please try again later.";
                GameData = null; // Ensure table isn't shown on error
            }
        }
    }
}