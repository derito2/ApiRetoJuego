using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
/*
 Conexion de MySQL a la tabla de chest mediante un api, para despues
realizar un servicio GET con Rest Framework, donde con el query dado
nos muestra los datos que le pedimos.
 
 
 */
namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class LeaderboardController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public LeaderboardController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Leaderboard")]
        public ActionResult<List<LeaderboardViewModel>> GetLeaderboard()
        {
            List<LeaderboardViewModel> leaderboardData = new List<LeaderboardViewModel>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT user_id, name, total_score FROM leaderboard", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LeaderboardViewModel item = new LeaderboardViewModel
                            {
                                UserId = Convert.ToInt32(reader["user_id"]),
                                Name = reader["name"].ToString(),
                                TotalScore = Convert.ToInt32(reader["total_score"])
                            };
                            leaderboardData.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return Ok(leaderboardData);
        }
    }
}
