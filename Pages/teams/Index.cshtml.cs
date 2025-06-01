using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages.teams
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

        // Holds retrieved team data
        public DataTable? TeamsData { get; private set; }
        // User-facing messages
        public string Message { get; set; } = string.Empty;
        
        //request and handle loading of teams via clsDatabase
        public async Task OnGetAsync()
        {
            _logger.LogInformation("Fetching teams list.");
            try
            {
                // Attempt to fetch team data from database
                DataSet dsTeams = await _clsDatabase.getTeamID();

                if (dsTeams != null && dsTeams.Tables.Count > 0 && dsTeams.Tables[0].Rows.Count > 0)
                {
                    TeamsData = dsTeams.Tables[0];
                    _logger.LogInformation("Successfully retrieved {RowCount} teams.", TeamsData.Rows.Count);
                }
                else
                {
                    Message = "No players found in the league."; 
                    _logger.LogWarning("No data returned by stored procedure for teams list page.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the teams list.");
                Message = "An error occurred while trying to load the teams list. Please try again later.";
                TeamsData = null;
            }
        }
    }
}