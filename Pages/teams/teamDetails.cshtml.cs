using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages.teams
{
    public class teamDetailsModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<teamDetailsModel> _logger;

        public teamDetailsModel(clsDatabase clsDatabaseService, ILogger<teamDetailsModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
        }

        public DataRow? TeamDetail { get; private set; }
        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync(int id)
        {
            _logger.LogInformation("Fetching team details for teamID: {teamid}.", id);
            try
            {
                DataSet dsTeam = await _clsDatabase.getTeamInfo(id);

                if (dsTeam != null && dsTeam.Tables.Count > 0 && dsTeam.Tables[0].Rows.Count > 0)
                {
                    TeamDetail = dsTeam.Tables[0].Rows[0];
                    _logger.LogInformation("Successfully retrieved details for teamID: {teamID}.", id);
                }
                else
                {
                    Message = $"No details found for team ID {id}.";
                    _logger.LogWarning("No data returned by stored procedure for teamID: {teamID}.", id);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching details for teamID: {teamID}.", id);
                Message = "An error occurred while trying to load team details. Please try again later.";
                TeamDetail = null;
            }
        }
    }
}