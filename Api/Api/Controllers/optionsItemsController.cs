using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class optionsItemsController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public optionsItemsController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("OptionsItems/{chestId}")]
        public ActionResult<List<optionsItems>> Get(int chestId)
        {
            List<optionsItems> ListaOptionsItems = new List<optionsItems>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT op.chest_id, it.item_name FROM options_items op JOIN items it on it.item_id = op.item_id WHERE op.chest_id = @chestId", _connection))
                {
                    cmd.Parameters.AddWithValue("@chestId", chestId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            optionsItems oi = new optionsItems
                            {
                                chest_id = Convert.ToInt32(reader["chest_id"]),
                                item_name = reader["item_name"].ToString()
                            };
                            ListaOptionsItems.Add(oi);
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
            return Ok(ListaOptionsItems);
        }
    }
}
