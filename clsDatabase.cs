using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;

namespace OIBaseballLeagueAPI.Services
{
    public class clsDatabase
    {
        private readonly string _connectionString;
        private readonly ILogger<clsDatabase> _logger;
        public clsDatabase(IConfiguration configuration, ILogger<clsDatabase> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;

            if (string.IsNullOrEmpty(_connectionString))
            {
                _logger.LogCritical("Connection string 'DefaultConnection' not found.");
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }
        }
        //////////////////////////
        private MySqlConnection AcquireConnection()
        {
            MySqlConnection cnMySQL = new MySqlConnection(_connectionString);
            try
            {
                cnMySQL.Open();
                _logger.LogInformation("Successfully acquired and opened a MySQL connection.");
                return cnMySQL;
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Error acquiring MySQL connection. MySQL Error Code: {ErrorCode}", ex.Number);
                cnMySQL.Dispose();
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Generic error acquiring MySQL connection.");
                cnMySQL.Dispose();
                return null;
            }
        }
        ///////////////////////////////// add / insert functions
        public int addTeam(string teamName, string city, string state, string fieldAddress, string coachFN, string coachLN, string coachPhone)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("addTeam: Could not acquire database connection.");
                    return -1;
                }
                string spName = "enterTeamSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_teamName", teamName);
                    cmdMySQL.Parameters.AddWithValue("p_city", city);
                    cmdMySQL.Parameters.AddWithValue("p_state", state);
                    cmdMySQL.Parameters.AddWithValue("p_fieldAddress", fieldAddress);
                    cmdMySQL.Parameters.AddWithValue("p_coachFN", coachFN);
                    cmdMySQL.Parameters.AddWithValue("p_coachLN", coachLN);
                    cmdMySQL.Parameters.AddWithValue("p_coachPhone", coachPhone);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("{spName} successful for record: {teamName}", spName, teamName);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} call for record: {TeamName}. MySQL Error Code: {ErrorCode}", spName, teamName, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} for record: {TeamName}", spName, teamName);
                        return -1;
                    }
                }

            }

        }

        ////////////////////////////////////// 

        public int addPlayer(string firstName, string lastName, DateOnly DOB, int teamID, int jerseyNumber, string position)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("addPlayer: Could not acquire database connection.");
                    return -1;
                }
                string spName = "enterPlayerSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_playerFN", firstName);
                    cmdMySQL.Parameters.AddWithValue("p_playerLN", lastName);
                    DateTime dobAsDateTime = DOB.ToDateTime(TimeOnly.MinValue);
                    cmdMySQL.Parameters.AddWithValue("p_dateOfBirth", dobAsDateTime);

                    cmdMySQL.Parameters.AddWithValue("p_teamID", teamID);
                    cmdMySQL.Parameters.AddWithValue("p_jerseyNum", jerseyNumber);
                    cmdMySQL.Parameters.AddWithValue("p_position", position);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("{spName} successful for record: {firstName} {lastName}", spName, firstName, lastName);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} call for record: {firstName} {lastName}. MySQL Error Code: {ErrorCode}", spName, firstName, lastName, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} for record: {firstName} {lastName}", spName, firstName, lastName);
                        return -1;
                    }
                }

            }

        }
        //////////////////////////////////////
        public int addGame(int homeID, int awayID, int homeScore, int awayScore, DateOnly date, string location)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("addGame: Could not acquire database connection.");
                    return -1;
                }
                string spName = "enterGameSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_homeID", homeID);
                    cmdMySQL.Parameters.AddWithValue("p_awayID", awayID);
                    cmdMySQL.Parameters.AddWithValue("p_homeScore", homeScore);
                    cmdMySQL.Parameters.AddWithValue("p_awayScore", awayScore);
                    DateTime dobAsDateTime = date.ToDateTime(TimeOnly.MinValue);
                    cmdMySQL.Parameters.AddWithValue("p_date", dobAsDateTime);
                    cmdMySQL.Parameters.AddWithValue("p_location", location);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("{spName} successful for record: {homeID} vs {awayID}", spName, homeID, awayID);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} call for record: {homeID} vs {awayID}. MySQL Error Code: {ErrorCode}", spName, homeID, awayID, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} for record: {homeID} vs {awayID}", spName, homeID, awayID);
                        return -1;
                    }
                }

            }

        }
        ////////////////////////////////////// Get / Display functions 
        public async Task<DataSet> getTeamStandings()
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getTeamStandings: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "displayTeamStandingsSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.", spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        ///////////////////////////
        public async Task<DataSet> getHittingStats()
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getHittingStats: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "displayHittingStatsSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.", spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        ///////////////////////////
        public async Task<DataSet> getPitchingStats()
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getPitchingStats: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "displayPitchingStatsSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.", spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        ////////////////////////

        public async Task<DataSet> getTeamInfo(int teamID)
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getTeamInfo: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "displayTeamInfoSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_teamID", teamID);

                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.", spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error in {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error in {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        /////////////////////////////////
        public async Task<DataSet> getPlayerInfo(int playerID)
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getPlayerInfo: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "displayPlayerInfoSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_playerID", playerID);

                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {StoredProcedureName}. It contains {TableCount} table(s), first table has {RowCount} rows.",
                                    spName, dsSQL.Tables.Count, dsSQL.Tables[0].Rows.Count);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {StoredProcedureName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error in {StoredProcedureName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error in {StoredProcedureName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        ///////////////////////////////////////
        public async Task<DataSet> getTeamID()
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getteamID: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "getTeamIDSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.", spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error in {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error in {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        /////////////////////////////////////////
        public async Task<DataSet> getPlayerID()
        {
            DataSet dsSQL = new DataSet();

            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getPlayerID: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "getPlayerIDSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.", spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error in {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error in {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }
        /////////////////////////////
        public async Task<DataSet> getGames()
        {
            DataSet dsSQL = new DataSet();

             using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    _logger.LogWarning("getGames: Could not acquire database connection.");
                    return dsSQL;
                }

                string spName = "displayGamesSTPD";
                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        using (MySqlDataAdapter daMySQL = new MySqlDataAdapter(cmdMySQL))
                        {
                            await Task.Run(() => daMySQL.Fill(dsSQL));

                            if (dsSQL.Tables.Count > 0 && dsSQL.Tables[0] != null)
                            {
                                _logger.LogInformation("Successfully filled DataSet from {spName}.",spName);
                            }
                            else
                            {
                                _logger.LogInformation("Executed {spName}, DataSet filled but contains no tables or first table is null.", spName);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error in {spName} when filling DataSet. MySQL Error Code: {ErrorCode}", spName, ex.Number);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error in {spName} when filling DataSet.", spName);
                    }
                }
            }
            return dsSQL;
        }   

        ////////////////////// Update functions
        public int updateStandings(int winnerID, int loserID)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    return -1;
                }
                string spName = "updateStandingsSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_winnerID", winnerID);
                    cmdMySQL.Parameters.AddWithValue("p_loserID", loserID);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("{spName} successful for record: {winnerID} vs {loserID}", spName, winnerID, loserID);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName} for {winnerID} vs {loserID}. MySQL Error Code: {ErrorCode}", spName, winnerID, loserID, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} for record: {winnerID} vs {loserID}", spName, winnerID, loserID);
                        return -1;
                    }
                }

            }
        }
        /////////////////////////
        public int updateHittingStats(int playerID, int atBat, int plateAppearances, int runs, int strikouts, int walks, int hitByPitch, int stolenBases, int doubles, int triples, int homeRuns, int runsBattedIn, int hits)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    return -1;
                }
                string spName = "updateHittingStatsSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_playerID", playerID);
                    cmdMySQL.Parameters.AddWithValue("p_ab", atBat);
                    cmdMySQL.Parameters.AddWithValue("p_pa", plateAppearances);
                    cmdMySQL.Parameters.AddWithValue("p_runs", runs);
                    cmdMySQL.Parameters.AddWithValue("p_ks", strikouts);
                    cmdMySQL.Parameters.AddWithValue("p_bbs", walks);
                    cmdMySQL.Parameters.AddWithValue("p_hbp", hitByPitch);
                    cmdMySQL.Parameters.AddWithValue("p_sb", stolenBases);
                    cmdMySQL.Parameters.AddWithValue("p_2b", doubles);
                    cmdMySQL.Parameters.AddWithValue("p_3b", triples);
                    cmdMySQL.Parameters.AddWithValue("p_hr", homeRuns);
                    cmdMySQL.Parameters.AddWithValue("p_rbi", runsBattedIn);
                    cmdMySQL.Parameters.AddWithValue("p_hits", runsBattedIn);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("{spName} successful for record: {playerID}", spName, playerID);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName}: {playerID}. MySQL Error Code: {ErrorCode}", spName, playerID, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} for record: {playerID}", spName, playerID);
                        return -1;
                    }
                }

            }
        }
        /////////////////////////
        public int updatePitchingStats(int playerID, decimal inningsPitched, int strikes, int balls, int walks, int strikeouts, int hitsAllowed, int earnedRuns, int saves, int unearnedRuns, int wins, int losses)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    return -1;
                }
                string spName = "updatePitchingStatsSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_playerID", playerID);
                    cmdMySQL.Parameters.AddWithValue("p_ip", inningsPitched);
                    cmdMySQL.Parameters.AddWithValue("p_strikes", strikes);
                    cmdMySQL.Parameters.AddWithValue("p_balls", balls);
                    cmdMySQL.Parameters.AddWithValue("p_bbs", walks);
                    cmdMySQL.Parameters.AddWithValue("p_ks", strikeouts);
                    cmdMySQL.Parameters.AddWithValue("p_hitsAllowed", hitsAllowed);
                    cmdMySQL.Parameters.AddWithValue("p_earnedRuns", earnedRuns);
                    cmdMySQL.Parameters.AddWithValue("p_saves", saves);
                    cmdMySQL.Parameters.AddWithValue("p_unearnedRuns", unearnedRuns);
                    cmdMySQL.Parameters.AddWithValue("p_wins", wins);
                    cmdMySQL.Parameters.AddWithValue("p_losses", losses);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("{spName} successful for record: {playerID}", spName, playerID);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error executing {spName}: {playerID}. MySQL Error Code: {ErrorCode}", spName, playerID, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error executing {spName} for record: {playerID}", spName, playerID);
                        return -1;
                    }
                }

            }
        }
        
    ///////////////// removal functions
    public int removePlayer(int playerID)
        {
            using (MySqlConnection cnMySQL = AcquireConnection())
            {
                if (cnMySQL == null)
                {
                    return -1;
                }
                string spName = "removePlayerSTPD";

                using (MySqlCommand cmdMySQL = new MySqlCommand(spName, cnMySQL))
                {
                    cmdMySQL.CommandType = CommandType.StoredProcedure;
                    cmdMySQL.Parameters.AddWithValue("p_playerID", playerID);
                    try
                    {
                        cmdMySQL.ExecuteNonQuery();
                        _logger.LogInformation("Successfully removed player with ID: {playerID}", playerID);
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        _logger.LogError(ex, "MySQL Error in removePlayer for ID {playerID}. MySQL Error Code: {ErrorCode}", playerID, ex.Number);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General Error in removePlayer for ID {playerID}", playerID);
                        return -1;
                    }
                }
            }
        }
    }
}