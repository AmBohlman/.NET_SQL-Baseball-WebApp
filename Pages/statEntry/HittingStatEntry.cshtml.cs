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
    public class HittingStatEntryModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<HittingStatEntryModel> _logger;

        public HittingStatEntryModel(clsDatabase clsDatabaseService, ILogger<HittingStatEntryModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
            PlayerOptions = new List<SelectListItem>();
        }

        // Hitting statistics input and validation
        [BindProperty]
        [Required(ErrorMessage = "Please select the player.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Player.")]
        [Display(Name = "Player")]
        public int SelectedPlayerID { get; set; }

        [BindProperty]
        [Range(0, 99, ErrorMessage = "At Bats must be between 0 and 99.")]
        [Display(Name = "At Bats")]
        public int AtBats { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Plate Appearances must be between 0 and 99.")]
        [Display(Name = "Plate Appearances")]
        public int PlateAppearances { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Hits must be between 0 and 99.")]
        [Display(Name = "Hits")]
        public int Hits { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Runs must be between 0 and 99.")]
        [Display(Name = "Runs")]
        public int Runs { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Strikeouts must be between 0 and 99.")]
        [Display(Name = "Strikeouts")]
        public int Strikeouts { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Walks must be between 0 and 99.")]
        [Display(Name = "Walks")]
        public int Walks { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Hit By Pitch must be between 0 and 99.")]
        [Display(Name = "Hit By Pitch")]
        public int HitByPitch { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Stolen Bases must be between 0 and 99.")]
        [Display(Name = "Stolen Bases")]
        public int StolenBases { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Doubles must be between 0 and 99.")]
        [Display(Name = "Doubles")]
        public int Doubles { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Triples must be between 0 and 99.")]
        [Display(Name = "Triples")]
        public int Triples { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Home Runs must be between 0 and 99.")]
        [Display(Name = "Home Runs")]
        public int HomeRuns { get; set; } = 0;

        [BindProperty]
        [Range(0, 99, ErrorMessage = "Runs Batted In must be between 0 and 99.")]
        [Display(Name = "Runs Batted In")]
        public int RunsBattedIn { get; set; } = 0;

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
                    _logger.LogWarning("No players found to populate dropdown for Hitting Stat Entry.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching players for Hitting Stat Entry dropdown.");
            }

            // Check for messages after redirect
            if (TempData["SuccessMessage"] != null)
            {
                Message = TempData["SuccessMessage"].ToString();
            }
        }

        // Handles submission of hitting stats
        public async Task<IActionResult> OnPostSubmitDataAsync()
        {
            
            if (SelectedPlayerID == 0)
            {
                ModelState.AddModelError("SelectedPlayerID", "Please select a player.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Hitting stat entry form submitted with validation errors.");
                Message = "Please correct the errors highlighted below and try again.";
                await OnGetAsync(); // Repopulate dropdown
                return Page();
            }

            _logger.LogInformation("Hitting stat entry form is valid. Attempting to update hitting stats for PlayerID {PlayerID}", SelectedPlayerID);

            // Update hitting stats in database
            int result = _clsDatabase.updateHittingStats(
                SelectedPlayerID,
                AtBats,
                PlateAppearances,
                Runs,
                Strikeouts,
                Walks,
                HitByPitch,
                StolenBases,
                Doubles,
                Triples,
                HomeRuns,
                RunsBattedIn,
                Hits
            );

            if (result == 0)
            {
                _logger.LogInformation("Hitting stats for PlayerID {PlayerID} updated successfully.", SelectedPlayerID);
                TempData["SuccessMessage"] = $"Hitting stats for PlayerID {SelectedPlayerID} have been updated successfully!";
                return RedirectToPage();
            }
            else
            {
                _logger.LogError("Failed to update hitting stats for PlayerID {PlayerID}. updateHittingStats returned {ResultCode}", SelectedPlayerID, result);
                Message = "There was an error saving the hitting stats. Please try again or check the system logs.";
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