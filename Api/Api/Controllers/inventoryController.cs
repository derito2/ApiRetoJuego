using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class inventoryController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public inventoryController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Inventory")]
        public ActionResult<List<inventory>> Get()
        {
            List<inventory> ListaPartidos = new List<inventory>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM `inventory`", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inventory inventor = new inventory
                            {
                                user_id = Convert.ToInt32(reader["user_id"]),
                                item_id = Convert.ToInt32(reader["item_id"])
                            };
                            ListaPartidos.Add(inventor);
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

