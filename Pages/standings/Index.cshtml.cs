using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages.standings
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

        // Holds retrieved team standings data
        public DataTable? StandingsData { get; private set; }
        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles GET request to fetch and display team standings
        public async Task OnGetAsync()
        {
            _logger.LogInformation("Fetching team standings.");
            try
            {
                // Attempt to fetch team standings from database
                DataSet dsStandings = await _clsDatabase.getTeamStandings();

                if (dsStandings != null && dsStandings.Tables.Count > 0 && dsStandings.Tables[0].Rows.Count > 0)
                {
                    StandingsData = dsStandings.Tables[0];
                    _logger.LogInformation("Successfully retrieved {RowCount} rows for team standings.", StandingsData.Rows.Count);
                }
                else
                {
                    Message = "No standings data currently available.";
                    _logger.LogWarning("No data returned by getTeamStandings for standings page.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching team standings.");
                Message = "An error occurred while trying to load standings. Please try again later.";
                StandingsData = null;
            }
        }
    }
}