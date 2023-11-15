using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class purseController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public purseController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Purse")]
        public ActionResult<List<purse>> Get()
        {
            List<purse> ListaBilleteras = new List<purse>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM purse", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            purse p = new purse
                            {
                                purse_id = Convert.ToInt32(reader["purse_id"]),
                                user_id = Convert.ToInt32(reader["user_id"]),
                                match_id = reader.IsDBNull(reader.GetOrdinal("match_id")) ? (int?)null : Convert.ToInt32(reader["match_id"]),
                                coins = Convert.ToInt32(reader["coins"]),
                                transaction_id = Convert.ToInt32(reader["transaction_id"]),
                                cost = Convert.ToDecimal(reader["cost"]),
                                // Asegúrate de mapear el resto de las propiedades de la billetera aquí
                            };
                            ListaBilleteras.Add(p);
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
            return Ok(ListaBilleteras);
        }
    }
}
