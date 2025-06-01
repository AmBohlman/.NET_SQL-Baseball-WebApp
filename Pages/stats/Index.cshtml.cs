using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OIBaseballLeagueAPI.Pages.stats
{
    public class IndexModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<IndexModel> _logger;

        // Holds retrieved hitting stats data
        public DataTable? HittingStatsData { get; private set; }
        // Column headers for hitting stats table
        public Dictionary<string, string> HittingStatHeaders { get; private set; }

        // Holds retrieved pitching stats data
        public DataTable? PitchingStatsData { get; private set; }
        // Column headers for pitching stats table
        public Dictionary<string, string> PitchingStatHeaders { get; private set; }
        
        // User-facing messages
        public string Message { get; set; } = string.Empty;

        public IndexModel(clsDatabase clsDatabaseService, ILogger<IndexModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
            HittingStatHeaders = new Dictionary<string, string>();
            PitchingStatHeaders = new Dictionary<string, string>();
        }

        // Handles GET request to fetch and display hitting and pitching stats
        public async Task OnGetAsync()
        {
            _logger.LogInformation("Fetching player stats.");
            string hittingMsg = string.Empty;
            string pitchingMsg = string.Empty;
            
            // Attempt to fetch hitting stats
            try
            {
                DataSet dsHittingStats = await _clsDatabase.getHittingStats();

                if (dsHittingStats != null && dsHittingStats.Tables.Count > 0 && dsHittingStats.Tables[0].Rows.Count > 0)
                {
                    HittingStatsData = dsHittingStats.Tables[0];
                    _logger.LogInformation("Successfully retrieved {RowCount} rows for hitting stats.", HittingStatsData.Rows.Count);
                    //setup datatables with standard statistical naming conventions
                    HittingStatHeaders.Add("teamName", "Team");
                    HittingStatHeaders.Add("playerFN", "First Name");
                    HittingStatHeaders.Add("playerLN", "Last Name");
                    HittingStatHeaders.Add("atBats", "AB");
                    HittingStatHeaders.Add("plateAppearances", "PA");
                    HittingStatHeaders.Add("hits", "H");
                    HittingStatHeaders.Add("runs", "R");
                    HittingStatHeaders.Add("walks", "BB");
                    HittingStatHeaders.Add("stolenBases", "SB");
                    HittingStatHeaders.Add("strikeouts", "SO");
                    HittingStatHeaders.Add("doubles", "2B");
                    HittingStatHeaders.Add("triples", "3B");
                    HittingStatHeaders.Add("homeruns", "HR");
                    HittingStatHeaders.Add("hitByPitch", "HBP");
                    HittingStatHeaders.Add("AVG", "AVG");
                    HittingStatHeaders.Add("OBP", "OBP");
                    HittingStatHeaders.Add("SLG", "SLG");
                    HittingStatHeaders.Add("OPS", "OPS");
                }
                else
                {
                    hittingMsg = "No hitting stats data currently available.";
                    _logger.LogWarning("No data returned by stored procedure for hitting stats page.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching hitting stats.");
                hittingMsg = "An error occurred while trying to load hitting stats. Please try again later.";
                HittingStatsData = null;
            }

            // Attempt to fetch pitching stats
            try
            {
                DataSet dsPitchingStats = await _clsDatabase.getPitchingStats();

                if (dsPitchingStats != null && dsPitchingStats.Tables.Count > 0 && dsPitchingStats.Tables[0].Rows.Count > 0)
                {
                    PitchingStatsData = dsPitchingStats.Tables[0];
                    _logger.LogInformation("Successfully retrieved {RowCount} rows for pitching stats.", PitchingStatsData.Rows.Count);
                    //setup datatables with standard statistical naming conventions
                    PitchingStatHeaders.Add("teamName", "Team");
                    PitchingStatHeaders.Add("playerFN", "First Name");
                    PitchingStatHeaders.Add("playerLN", "Last Name");
                    PitchingStatHeaders.Add("inningsPitched", "IP");
                    PitchingStatHeaders.Add("walks", "BB");
                    PitchingStatHeaders.Add("strikeouts", "SO");
                    PitchingStatHeaders.Add("hitsAllowed", "H");
                    PitchingStatHeaders.Add("earnedRuns", "ER");
                    PitchingStatHeaders.Add("ERA", "ERA");
                    PitchingStatHeaders.Add("WHIP", "WHIP");
                    PitchingStatHeaders.Add("saves", "SV");
                    PitchingStatHeaders.Add("chargedWins", "W");
                    PitchingStatHeaders.Add("chargedLosses", "L");
                }
                else
                {
                    pitchingMsg = "No pitching stats data currently available.";
                    _logger.LogWarning("No data returned by stored procedure for pitching stats page.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching pitching stats.");
                pitchingMsg = "An error occurred while trying to load pitching stats. Please try again later.";
                PitchingStatsData = null;
            }

            // Consolidate messages from data fetching operations
            if (!string.IsNullOrEmpty(hittingMsg) && !string.IsNullOrEmpty(pitchingMsg))
            {
                Message = $"{hittingMsg} {pitchingMsg}";
            }
            else if (!string.IsNullOrEmpty(hittingMsg))
            {
                Message = hittingMsg;
            }
            else if (!string.IsNullOrEmpty(pitchingMsg))
            {
                Message = pitchingMsg;
            }
            else if (HittingStatsData == null && PitchingStatsData == null) // Only set this if both are null and no specific errors occurred
            {
                Message = "No stats data available at the moment.";
            }
        }
    }
}