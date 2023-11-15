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
    public class transactionController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public transactionController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("Transaction")]
        public ActionResult<List<transaction>> Get()
        {
            List<transaction> ListaTransacciones = new List<transaction>();
            try
            {
                _connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM `transaction`", _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transaction trn = new transaction
                            {
                                transaction_id = Convert.ToInt32(reader["transaction_id"]),
                                typeOftransaction = reader["typeOftransaction"].ToString(),
                                // Asegúrate de mapear el resto de las propiedades de la transacción aquí
                            };
                            ListaTransacciones.Add(trn);
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
            return Ok(ListaTransacciones);
        }
    }
}
