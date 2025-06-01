using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages.gameEntry
{
    public class IndexModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(clsDatabase clsDatabaseService, ILogger<IndexModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
            TeamOptions = new List<SelectListItem>();
        }

        // Game details input and validation
        [BindProperty]
        [Required(ErrorMessage = "Please select the Home Team.")]
        [Display(Name = "Home Team")]
        public int HomeID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select the Away Team.")]
        [Display(Name = "Away Team")]
        public int AwayID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Home Score is required.")]
        [Range(0, 999, ErrorMessage = "Score must be between 0 and 999.")]
        [Display(Name = "Home Score")]
        public int HomeScore { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Away Score is required.")]
        [Range(0, 999, ErrorMessage = "Score must be between 0 and 999.")]
        [Display(Name = "Away Score")]
        public int AwayScore { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Game Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Game Date")]
        public DateOnly GameDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [BindProperty]
        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Location must be between 3 and 100 characters.")]
        [Display(Name = "Location")]
        public string GameLocation { get; set; } = string.Empty;

        // For team selection dropdowns
        public List<SelectListItem> TeamOptions { get; set; }
        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles GET request, populates team dropdowns
        public async Task OnGetAsync()
        {
            TeamOptions.Add(new SelectListItem { Value = "", Text = "-- Select a Team --" });
            try
            {
                // Fetch teams for dropdowns
                DataSet dsTeams = await _clsDatabase.getTeamID();
                if (dsTeams != null && dsTeams.Tables.Count > 0 && dsTeams.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsTeams.Tables[0].Rows)
                    {
                        TeamOptions.Add(new SelectListItem
                        {
                            Value = row["teamID"].ToString(),
                            Text = row["teamName"].ToString()
                        });
                    }
                }
                else
                {
                    _logger.LogWarning("No teams found to populate dropdowns for game entry.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching teams for game entry dropdowns.");
            }

            // Check for messages after redirect
            if (TempData["SuccessMessage"] != null)
            {
                Message = TempData["SuccessMessage"].ToString();
            }
        }

        // Handles game data submission and updates standings
        public async Task<IActionResult> OnPostSubmitDataAsync()
        {
            // Validate home/away teams are different
            if (HomeID == AwayID)
            {
             ModelState.AddModelError(string.Empty, "Home Team and Away Team cannot be the same.");
            }

            // Validate scores are not tied
            if (HomeScore == AwayScore)
            {
                ModelState.AddModelError(string.Empty, "Scores cannot be tied. Please enter a winning and losing score.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Game entry form submitted with validation errors.");
                Message = "Please correct the errors and try again.";
                await OnGetAsync(); // Repopulate dropdowns
                return Page();
            }

            _logger.LogInformation("Game entry form is valid. Attempting to add game between Home Team {HomeID} and Away Team {AwayID}.", HomeID, AwayID);
            
            // Add game to database
            int gameResultOrNewID = _clsDatabase.addGame(HomeID, AwayID, HomeScore, AwayScore, GameDate, GameLocation);

            if (gameResultOrNewID == 0) // Assuming 0 indicates success from addGame
            {
                _logger.LogInformation("Game between Home Team {HomeID} and Away Team {AwayID} added successfully.", HomeID, AwayID);
                
                int winnerID = 0;
                int loserID = 0;

                // Determine winner and loser for standings (scores cannot be equal here due to validation)
                if (HomeScore > AwayScore)
                {
                    winnerID = HomeID;
                    loserID = AwayID;
                }
                else // Since scores aren't equal, AwayScore must be > HomeScore
                {
                    winnerID = AwayID;
                    loserID = HomeID;
                }
                

                if (winnerID != 0 && loserID != 0) 
                {
                    // Update team standings
                    int standingsResult = _clsDatabase.updateStandings(winnerID, loserID);
                    if (standingsResult == 0)
                    {
                        _logger.LogInformation("Standings updated successfully for winner {WinnerID} and loser {LoserID}.", winnerID, loserID);
                    }
                    else
                    {
                        _logger.LogError("Failed to update standings for winner {WinnerID} and loser {LoserID}. updateStandings returned {StandingsResultCode}", winnerID, loserID, standingsResult);
                    }
                }

                TempData["SuccessMessage"] = $"Game recorded successfully!";
                return RedirectToPage();
            }
            else
            {
                _logger.LogError("Failed to add game. clsDatabase.addGame returned {ResultCode}", gameResultOrNewID);
                Message = "There was an error saving the game. Please try again or check the system logs.";
                await OnGetAsync(); // Repopulate dropdowns
                return Page();
            }
        }

        // Handles form reset
        public IActionResult OnPostResetForm()
        {
            return RedirectToPage();
        }
    }
}