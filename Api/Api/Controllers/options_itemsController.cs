using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class options_itemsController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public options_itemsController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Options_items")]
        public ActionResult<List<options_items>> Get()
        {
            List<options_items> ListaOpcionesItems = new List<options_items>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM options_items", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            options_items op = new options_items
                            {
                                chest_id = Convert.ToInt32(reader["chest_id"]),
                                item_id = Convert.ToInt32(reader["item_id"]),
                                // Asegúrate de mapear aquí el resto de las propiedades de options_items
                            };
                            ListaOpcionesItems.Add(op);
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
            return Ok(ListaOpcionesItems);
        }
    }
}