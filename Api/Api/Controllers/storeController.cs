using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class storeController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public storeController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Store")]
        public ActionResult<List<store>> Get()
        {
            List<store> ListaTiendas = new List<store>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM store", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store s = new store
                            {
                                user_id = Convert.ToInt32(reader["user_id"]),
                                chest_id = Convert.ToInt32(reader["chest_id"]),
                                item_id = Convert.ToInt32(reader["item_id"]),
                                date = Convert.ToDateTime(reader["date"]),
                                // Asegúrate de mapear el resto de las propiedades de la tienda aquí
                            };
                            ListaTiendas.Add(s);
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
            return Ok(ListaTiendas);
        }
    }
}
