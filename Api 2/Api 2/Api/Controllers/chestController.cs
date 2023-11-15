using System;
using System.Collections.Generic;
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
    public class chestController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public chestController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Chest")]
        public ActionResult<List<chest>> Get()
        {
            List<chest> ListaCofres = new List<chest>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM chest", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chest c = new chest
                            {
                                chest_id = Convert.ToInt32(reader["chest_id"]),
                                chest_name = reader["chest_name"].ToString(),
                                price = Convert.ToInt32(reader["price"]),
                                // Asegúrate de mapear aquí el resto de las propiedades de la entidad chest
                            };
                            ListaCofres.Add(c);
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
            return Ok(ListaCofres);
        }
    }
}
