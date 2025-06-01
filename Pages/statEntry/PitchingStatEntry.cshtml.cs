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

namespace OIBaseballLeagueAPI.Pages.statEntry
{
    public class PitchingStatEntryModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<PitchingStatEntryModel> _logger;

        public PitchingStatEntryModel(clsDatabase clsDatabaseService, ILogger<PitchingStatEntryModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
            PlayerOptions = new List<SelectListItem>();
        }

        // Pitching statistics input and validation
        [BindProperty]
        [Required(ErrorMessage = "Please select the player.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Player.")] // Ensures a positive ID is selected
        [Display(Name = "Player")]
        public int SelectedPlayerID { get; set; }

        [BindProperty]
        [Range(0, 27, ErrorMessage = "Innings Pitched must be between 0 and 27.")] // Max innings in a standard 9-inning game by one pitcher (extreme case)
        [Display(Name = "Innings Pitched")]
        public decimal InningsPitched { get; set; } = 0.0m;

        [BindProperty]
        [Range(0, 999, ErrorMessage = "Strikes Thrown must be between 0 and 999.")]
        [Display(Name = "Strikes Thrown")]
        public int Strikes { get; set; } = 0;

        [BindProperty]
        [Range(0, 999, ErrorMessage = "Balls Thrown must be between 0 and 999.")]
        [Display(Name = "Balls Thrown")]
        public int Balls { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Walks must be between 0 and 99.")]
        [Display(Name = "Walks")]
        public int Walks { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Strikeouts must be between 0 and 99.")]
        [Display(Name = "Strikeouts")]
        public int Strikeouts { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Hits Allowed must be between 0 and 99.")]
        [Display(Name = "Hits Allowed")]
        public int HitsAllowed { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Earned Runs must be between 0 and 99.")]
        [Display(Name = "Earned Runs")]
        public int EarnedRuns { get; set; } = 0;

        [BindProperty]
        [Range(0, 10, ErrorMessage = "Saves must be between 0 and 10.")]
        [Display(Name = "Saves")]
        public int Saves { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Unearned Runs must be between 0 and 99.")]
        [Display(Name = "Unearned Runs")]
        public int UnearnedRuns { get; set; } = 0;

        [BindProperty]
        [Range(0, 10, ErrorMessage = "Wins must be between 0 and 10.")]
        [Display(Name = "Wins")]
        public int Wins { get; set; } = 0;

        [BindProperty]
        [Range(0, 10, ErrorMessage = "Losses must be between 0 and 10.")]
        [Display(Name = "Losses")]
        public int Losses { get; set; } = 0;

        // Options for player selection dropdown
        public List<SelectListItem> PlayerOptions { get; set; }
        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles GET request, populates player dropdown
        public async Task OnGetAsync()
        {
            PlayerOptions.Add(new SelectListItem { Value = "0", Text = "-- Select Player --" }); // Value "0" for unselected state
            try
            {
                // Fetch players for dropdown
                DataSet dsPlayers = await _clsDatabase.getPlayerID();
                if (dsPlayers != null && dsPlayers.Tables.Count > 0 && dsPlayers.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsPlayers.Tables[0].Rows)
                    {
                        PlayerOptions.Add(new SelectListItem
                        {
                            Value = row["playerID"].ToString(),
                            Text = $"{row["playerLN"]}, {row["playerFN"]}"
                        });
                    }
                }
                else
                {
                    _logger.LogWarning("No players found to populate dropdown for Pitching Stat Entry.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching players for Pitching Stat Entry dropdown.");
            }

            // Check for messages after redirect
            if (TempData["SuccessMessage"] != null)
            {
                Message = TempData["SuccessMessage"].ToString();
            }
        }

        // Handles submission of pitching stats
        public async Task<IActionResult> OnPostSubmitDataAsync()
        {
            // Custom validation for player selection (catches if "0" is explicitly posted, complements [Range])
            if (SelectedPlayerID == 0)
            {
                ModelState.AddModelError("SelectedPlayerID", "Please select a player.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Pitching stat entry form submitted with validation errors.");
                Message = "Please correct the errors highlighted below and try again.";
                await OnGetAsync(); // Repopulate dropdown
                return Page();
            }

            _logger.LogInformation("Pitching stat entry form is valid. Attempting to update pitching stats for PlayerID {PlayerID}", SelectedPlayerID);
            
            // Update pitching stats in database
            int result = _clsDatabase.updatePitchingStats(
                SelectedPlayerID,
                InningsPitched,
                Strikes,
                Balls,
                Walks,
                Strikeouts,
                HitsAllowed,
                EarnedRuns,
                Saves,
                UnearnedRuns,
                Wins,
                Losses
            );

            if (result == 0)
            {
                _logger.LogInformation("Pitching stats for PlayerID {PlayerID} updated successfully.", SelectedPlayerID);
                TempData["SuccessMessage"] = $"Pitching stats for PlayerID {SelectedPlayerID} have been updated successfully!";
                return RedirectToPage();
            }
            else
            {
                _logger.LogError("Failed to update pitching stats for PlayerID {PlayerID}. updatePitchingStats returned {ResultCode}", SelectedPlayerID, result);
                Message = "There was an error saving the pitching stats. Please try again or check the system logs.";
                await OnGetAsync(); // Repopulate dropdown
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