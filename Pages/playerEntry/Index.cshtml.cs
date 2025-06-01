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

namespace OIBaseballLeagueAPI.Pages.playerEntry
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

        //Accept and validate inputs from user
        [BindProperty]
        [Required(ErrorMessage = "Player's First Name is a required field!")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Player's First Name must be between 3 and 45 characters.")]
        [RegularExpression(@"^[a-zA-Z'\- ]+$", ErrorMessage = "Player's First Name can only contain letters, apostrophes, hyphens, or spaces.")]
        public string PlayerFN { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Player's Last Name is a required field!")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Player's Last Name must be between 3 and 45 characters.")]
        [RegularExpression(@"^[a-zA-Z'\- ]+$", ErrorMessage = "Player's Last Name can only contain letters, apostrophes, hyphens, or spaces.")]
        public string PlayerLN { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Date of Birth is a required field!")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [BindProperty]
        [Required(ErrorMessage = "Please select a Team, this is a requirment!")]
        public int TeamID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Jersey Number is a required field!")]
        [Range(0, 99, ErrorMessage = "If provided, Jersey Number must be between 0 and 99.")]
        public int JerseyNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Position is a required field!")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Position must be between 1 and 2 characters, please enter an abbreviation (i.e. 2B).")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Position can only contain letters and numbers.")]
        public string Position { get; set; } = string.Empty;

        // For team selection dropdown
        public List<SelectListItem> TeamOptions { get; set; }
        // Displays status messages on the page
        public string Message { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            //populate dropdown list
            TeamOptions.Clear();
            TeamOptions.Add(new SelectListItem { Value = "", Text = "-- Select a Team --" });
            try
            {
                // Get teams for the dropdown
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
                    _logger.LogWarning("No teams found to populate dropdown using getTeamID method.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching teams for dropdown using getTeamID method.");
                Message = "Error loading team data for selection.";
            }

            // Check for messages from redirect
            if (TempData["SuccessMessage"] != null)
            {
                Message = TempData["SuccessMessage"].ToString();
            }
            else if (TempData["ErrorMessage"] != null)
            {
                Message = TempData["ErrorMessage"].ToString();
            }
        }

        public async Task<IActionResult> OnPostSubmitData()
        {
            //Attempt stored procedure call with users inputs
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Player registration form submitted with validation errors.");
                await OnGetAsync(); // Repopulate dropdown
                Message = "Please correct the errors highlighted below and try again.";
                return Page();
            }

            _logger.LogInformation("Player registration form is valid. Attempting to add player: {PlayerFN} {PlayerLN}", PlayerFN, PlayerLN);
            int result = _clsDatabase.addPlayer(
                PlayerFN,
                PlayerLN,
                DateOfBirth,
                TeamID,
                JerseyNumber,
                Position
            );

            if (result == 0)
            {
                _logger.LogInformation("Player '{PlayerFN} {PlayerLN}' added successfully to the database.", PlayerFN, PlayerLN);
                TempData["SuccessMessage"] = $"Player '{PlayerFN} {PlayerLN}' has been registered successfully!";
                return RedirectToPage();
            }
            else
            {
                _logger.LogError("Failed to add player '{PlayerFN} {PlayerLN}' to the database. clsDatabase.addPlayer returned {ResultCode}", PlayerFN, PlayerLN, result);
                Message = "There was an error saving the player to the database. Please try again or check the system logs.";
                await OnGetAsync(); // Repopulate dropdown
                return Page();
            }
        }

        public IActionResult OnPostResetForm()
        {
            return RedirectToPage();
        }
    }
}
