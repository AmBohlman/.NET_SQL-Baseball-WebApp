using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace OIBaseballLeagueAPI.Pages.teamEntry
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

        // Recieve and validate info from the input form
        [BindProperty]
        [Required(ErrorMessage = "Team Name is a required field!")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Team Name must be between 3 and 45 characters.")]
        [RegularExpression(@"^[a-zA-Z'\- ]+$", ErrorMessage = "Team Name can only contain letters, apostrophes, hyphens, or spaces.")]
        public string TeamName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "City Name is a required field!")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "City Name must be between 3 and 45 characters.")]
        [RegularExpression(@"^[a-zA-Z'\- ]+$", ErrorMessage = "City Name can only contain letters, apostrophes, hyphens, or spaces.")]
        public string City { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "State Name is a required field!")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State Name must be exactly 2 characters, please use the standard abbreviation.")]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "State Name can only contain capital letters, please use the standard abbreviation.")]
        public string State { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Address is a required field!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 150 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9'\- ]+$", ErrorMessage = "Address can only contain letters, apostrophes, hyphens, numbers, or spaces.")]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Coach's First Name is a required field!")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "Coach's First Name must be between 1 and 45 characters.")]
        [RegularExpression(@"^[a-zA-Z'\-]+$", ErrorMessage = "Coach's First Name can only contain letters, apostrophes, or hyphens.")]
        public string CoachFirstName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Coach's Last Name is a required field!")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "Coach's Last Name must be between 1 and 45 characters.")]
        [RegularExpression(@"^[a-zA-Z'\-]+$", ErrorMessage = "Coach's Last Name can only contain letters, apostrophes, or hyphens.")]
        public string CoachLastName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Coach's phone number is a required field!")]
        [RegularExpression(@"^\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format (xxx)xxx-xxxx.")]
        public string CoachPhone { get; set; } = string.Empty;

        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles initial page load
        public void OnGet()
        {
            // Check for messages after redirect
            if (TempData["SuccessMessage"] != null)
            {
                Message = TempData["SuccessMessage"].ToString();
            }
        }
        
        // Handles team data submission
        public IActionResult OnPostSubmitData()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Team registration form submitted with validation errors.");
                Message = "Please correct the errors highlighted below and try again.";
                return Page();
            }

            _logger.LogInformation("Team registration form is valid. Attempting to add team: {TeamName}", TeamName);

            //pass info to clsDatabase stored procedure to create team
            int result = _clsDatabase.addTeam(
                TeamName,
                City,
                State,
                Address,
                CoachFirstName,
                CoachLastName,
                CoachPhone
            );

            if (result == 0)
            {
                _logger.LogInformation("Team '{TeamName}' added successfully to the database.", TeamName);
                TempData["SuccessMessage"] = $"Team '{TeamName}' has been registered successfully!";
                return RedirectToPage();
            }
            else
            {
                _logger.LogError("Failed to add team '{TeamName}' to the database. clsDatabase.addTeam returned {ResultCode}", TeamName, result);
                Message = "There was an error saving the team to the database. Please try again or check the system logs.";
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