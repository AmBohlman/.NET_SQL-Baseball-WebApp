using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace OIBaseballLeagueAPI.Pages.statEntry
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // User-facing messages
        public string Message { get; set; } = string.Empty;

        // Handles initial page load
        public void OnGet()
        {
        }

        // Navigates to the Hitting Stats entry page
        public IActionResult OnPostGoToHittingStats()
        {
            _logger.LogInformation("Navigating to Hitting Stats Entry page.");
            return RedirectToPage("/statEntry/HittingStatEntry");
        }

        // Navigates to the Pitching Stats entry page
        public IActionResult OnPostGoToPitchingStats()
        {
            _logger.LogInformation("Navigating to Pitching Stats Entry page.");
            return RedirectToPage("/statEntry/PitchingStatEntry");
        }
    }
}