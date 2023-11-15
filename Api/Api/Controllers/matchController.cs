using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class matchController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public matchController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Match")]
        public ActionResult<List<match>> Get()
        {
            List<match> ListaPartidos = new List<match>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM `match`", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            match match1 = new match
                            {
                                match_id = Convert.ToInt32(reader["match_id"]),
                                user_id = Convert.ToInt32(reader["user_id"]),
                                score = Convert.ToInt32(reader["score"]),
                                // Asegúrate de mapear aquí el resto de las propiedades de la entidad match
                            };
                            ListaPartidos.Add(match1);
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
            return Ok(ListaPartidos);
        }
    }
}