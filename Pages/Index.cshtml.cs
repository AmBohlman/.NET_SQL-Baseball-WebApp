using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace OIBaseballLeagueAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Handles initial page load
        public void OnGet()
        {
        }
        
        // Send user to appropriate page based on button clicked
        public IActionResult OnPostRegisterTeam()
        {
            _logger.LogInformation("Register Team button clicked. Redirecting to /teamEntry/Index.");
            return RedirectToPage("/teamEntry/Index");
        }
        public IActionResult OnPostRegisterPlayer()
        {
            _logger.LogInformation("Register Player button clicked. Redirecting to /playerEntry/Index.");
            return RedirectToPage("/playerEntry/Index");
        }
        public IActionResult OnPostScoreGame()
        {
            _logger.LogInformation("Score Game button clicked. Redirecting to /gameEntry/Index.");
            return RedirectToPage("/gameEntry/Index");
        }
        public IActionResult OnPostUpdateStats()
        {
            _logger.LogInformation("Enter Stats button clicked. Redirecting to /statEntry/Index.");
            return RedirectToPage("/statEntry/Index");
        }
        
        public IActionResult OnPostRemovePlayer()
        {
            _logger.LogInformation("Delete Player button clicked. Redirecting to /PlayerDeletion."); 
            return RedirectToPage("/PlayerDeletion"); 
        }
    }
}