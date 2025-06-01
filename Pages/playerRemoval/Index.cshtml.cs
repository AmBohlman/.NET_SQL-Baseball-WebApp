using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OIBaseballLeagueAPI.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Pages
{
    public class PlayerDeletionModel : PageModel
    {
        private readonly clsDatabase _clsDatabase;
        private readonly ILogger<PlayerDeletionModel> _logger;

        public PlayerDeletionModel(clsDatabase clsDatabaseService, ILogger<PlayerDeletionModel> logger)
        {
            _clsDatabase = clsDatabaseService;
            _logger = logger;
            PlayerOptions = new List<SelectListItem>();
        }

        // Options for player selection dropdown
        public List<SelectListItem> PlayerOptions { get; set; }

        // ID of the player selected for deletion
        [BindProperty]
        [Required(ErrorMessage = "Please select a player to delete.")]
        public int PlayerIDToDelete { get; set; }

        // User-facing messages
        public string Message { get; set; } = string.Empty;
        // Controls display of deletion confirmation step
        public bool ShowConfirmation { get; set; } = false;
        // Name of the player for confirmation message
        public string PlayerNameToConfirm { get; set; } = string.Empty;

        // Handles initial page load and populates player list
        public async Task OnGetAsync()
        {
            await PopulatePlayerOptions();
            // Check for messages after redirect
            if (TempData["SuccessMessage"] != null)
            {
                Message = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                Message = TempData["ErrorMessage"].ToString();
            }
        }

        // Populates the dropdown list with players
        private async Task PopulatePlayerOptions()
        {
            PlayerOptions.Clear();
            PlayerOptions.Add(new SelectListItem { Value = "", Text = "-- Select a Player --" });
            try
            {
                // Fetch players from database
                DataSet dsPlayers = await _clsDatabase.getPlayerID();
                if (dsPlayers != null && dsPlayers.Tables.Count > 0 && dsPlayers.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsPlayers.Tables[0].Rows)
                    {
                        PlayerOptions.Add(new SelectListItem
                        {
                            Value = row["playerID"].ToString(),
                            Text = $"{row["playerFN"]} {row["playerLN"]} (ID: {row["playerID"]})"
                        });
                    }
                }
                else
                {
                    _logger.LogWarning("No players found to populate dropdown for deletion.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error fetching players for deletion dropdown.");
                Message = "Error loading player list for deletion.";
            }
        }

        // Handles initial request to delete a player, shows confirmation step
        public async Task<IActionResult> OnPostRequestDeleteAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions();
                return Page();
            }

            try
            {
                // Fetch details of selected player for confirmation
                DataSet dsPlayer = await _clsDatabase.getPlayerInfo(PlayerIDToDelete);
                if (dsPlayer != null && dsPlayer.Tables.Count > 0 && dsPlayer.Tables[0].Rows.Count > 0)
                {
                    DataRow playerRow = dsPlayer.Tables[0].Rows[0];
                    PlayerNameToConfirm = $"{playerRow["playerFN"]} {playerRow["playerLN"]} (ID: {playerRow["playerID"]})";
                    ShowConfirmation = true;
                    Message = $"Are you sure you want to delete {PlayerNameToConfirm}?";
                }
                else
                {
                    Message = "Player not found. Unable to proceed with deletion confirmation.";
                    _logger.LogWarning("Player with ID {PlayerID} not found for deletion confirmation.", PlayerIDToDelete);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error fetching player info for deletion confirmation for PlayerID {PlayerID}.", PlayerIDToDelete);
                Message = "An error occurred while trying to confirm player details for deletion.";
            }
            
            await PopulatePlayerOptions(); // Repopulate options in case of error or to show confirmation
            return Page();
        }

        // Handles final confirmation to delete a player
        public async Task<IActionResult> OnPostConfirmDeleteAsync()
        {
            // Validate PlayerID is present
            if (PlayerIDToDelete == 0)
            {
                ModelState.AddModelError(nameof(PlayerIDToDelete), "Player ID for deletion was not specified.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Player deletion confirmation failed due to validation errors or missing PlayerID.");
                await PopulatePlayerOptions(); // Ensure dropdown is populated if we return to the page
                ShowConfirmation = true; // Keep confirmation context if possible, though player name might be lost if not reposted
                Message = "Could not confirm player to delete. Please ensure a player is effectively selected and try again.";
                return Page();
            }
            
            _logger.LogInformation("Attempting to confirm deletion for player with ID: {PlayerID}", PlayerIDToDelete);
            
            // Attempt to remove player from database
            int result = _clsDatabase.removePlayer(PlayerIDToDelete);

            if (result == 0)
            {
                _logger.LogInformation("Player with ID: {PlayerID} deleted successfully.", PlayerIDToDelete);
                TempData["SuccessMessage"] = $"Player (ID: {PlayerIDToDelete}) has been deleted successfully!";
            }
            else
            {
                _logger.LogError("Failed to delete player with ID: {PlayerID}. clsDatabase.removePlayer returned {ResultCode}", PlayerIDToDelete, result);
                TempData["ErrorMessage"] = $"Error deleting player (ID: {PlayerIDToDelete}). The player may have related records (e.g., stats, game entries) that prevent deletion, or another error occurred.";
            }
            return RedirectToPage(); // Redirect to OnGet to show TempData message and refresh state
        }
    }
}