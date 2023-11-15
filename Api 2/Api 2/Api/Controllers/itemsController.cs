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
    public class itemsController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public itemsController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Items")]
        public ActionResult<List<items>> Get()
        {
            List<items> ListaItems = new List<items>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM items", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items item = new items
                            {
                                item_id = Convert.ToInt32(reader["item_id"]),
                                item_name = reader["item_name"].ToString(),
                                // Asegúrate de mapear aquí el resto de las propiedades de la entidad items
                            };
                            ListaItems.Add(item);
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
            return Ok(ListaItems);
        }
    }
}
