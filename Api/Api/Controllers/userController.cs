using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class userController : ControllerBase
    {
        private readonly MySqlConnection _connection;

        public userController(MySqlConnection connection)
        {
            _connection = connection;
        }
        [HttpGet("User")]
        public ActionResult<List<user>> Get([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("El correo electrónico es necesario para la consulta.");
            }

            List<user> ListaUsuarios = new List<user>();
            try
            {
                _connection.Open();

                string queryString = "SELECT * FROM user WHERE email = @Email";

                using (MySqlCommand cmd = new MySqlCommand(queryString, _connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user usr1 = new user
                            {
                                user_id = Convert.ToInt32(reader["user_id"]),
                                name = reader["name"].ToString(),
                                last_name = reader["last_name"].ToString(),
                                birth_date = Convert.ToDateTime(reader["birth_date"]),
                                gamertag = reader["gamertag"].ToString(),
                                password = reader["password"].ToString(),
                                coins = Convert.ToInt32(reader["coins"]),
                                gender = reader["gender"].ToString(),
                                state = reader["state"].ToString(),
                                email = reader["email"].ToString(),
                                rol = reader["rol"].ToString()
                            };
                            ListaUsuarios.Add(usr1);
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
            return Ok(ListaUsuarios);
        }


        [HttpPost("RegisterUser")]
        public ActionResult<user> Post([FromBody] user newUser)
        {
            try
            {
                int age = DateTime.Today.Year - newUser.birth_date.Year;
                if (newUser.birth_date.Date > DateTime.Today.AddYears(-age)) age--;

                // Verificar si el usuario es menor de 18 años
                if (age < 18)
                {
                    return BadRequest("Debes tener al menos 18 años para registrarte.");
                }
                _connection.Open();

                using (MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE email = @Email", _connection))
                {
                    checkCmd.Parameters.AddWithValue("@Email", newUser.email);
                    long count = (long)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        return BadRequest("El correo electrónico ya está en uso.");
                    }
                }

                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO user (name, last_name, email, gamertag, password, birth_date,  gender, state, coins, rol) VALUES (@Name, @Last_name, @Email,  @Gamertag, @Password, @BirthDate,@Gender, @State, @Coins, @Rol)", _connection))
                {
                    cmd.Parameters.AddWithValue("@Name", newUser.name);
                    cmd.Parameters.AddWithValue("@Last_name", newUser.last_name);
                    cmd.Parameters.AddWithValue("@Email", newUser.email);
                    cmd.Parameters.AddWithValue("@Gamertag", newUser.gamertag);
                    cmd.Parameters.AddWithValue("@Password", newUser.password);
                    cmd.Parameters.AddWithValue("@BirthDate", newUser.birth_date);
                    cmd.Parameters.AddWithValue("@Gender", newUser.gender);
                    cmd.Parameters.AddWithValue("@State", newUser.state);
                    cmd.Parameters.AddWithValue("@Coins", newUser.coins);
                    cmd.Parameters.AddWithValue("@Rol", newUser.rol);


                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        long newId = cmd.LastInsertedId;
                        newUser.user_id = (int)newId;
                        return CreatedAtAction(nameof(Get), new { id = newUser.user_id }, newUser);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                return Conflict("Existe un conflicto con los datos proporcionados: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
        [HttpPost("Login")]
        public ActionResult<LoginResponseDto> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                _connection.Open();

                using (var cmd = new MySqlCommand("SELECT * FROM user WHERE email = @Email LIMIT 1", _connection))
                {
                    cmd.Parameters.AddWithValue("@Email", loginDto.Email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asumiendo que la columna de contraseña se llama 'password' en tu base de datos
                            var storedPassword = reader["password"].ToString();

                            // Aquí simplemente estamos comparando las contraseñas en texto plano
                            // En una aplicación real, deberías comparar un hash de la contraseña proporcionada con el hash almacenado
                            if (storedPassword == loginDto.Password)
                            {
                                var userRol = reader["rol"].ToString();
                                var loginResponse = new LoginResponseDto
                                {
                                    Email = loginDto.Email, // Aquí guardamos el correo electrónico
                                    Message = "Inicio de sesión exitoso.",
                                    Rol = userRol

                                };
                                return Ok(loginResponse);
                            }
                            else
                            {
                                return Unauthorized("Contraseña incorrecta.");
                            }
                        }
                        else
                        {
                            return NotFound("Usuario no encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        [HttpGet("GetUserCoins")]
        public async Task<ActionResult<int>> GetUserCoins([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("El correo electrónico es necesario para la consulta.");
            }

            try
            {
                _connection.Open();
                using (var cmd = new MySqlCommand("SELECT coins FROM user WHERE email = @Email LIMIT 1", _connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = await cmd.ExecuteScalarAsync();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return NotFound("Usuario no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        [HttpGet("UserCountsByState")]
        public async Task<ActionResult<IEnumerable<UserCountByStateDto>>> GetUserCountsByState()
        {
            var userCounts = new List<UserCountByStateDto>();
            try
            {
                _connection.Open();
                var query = "SELECT state, COUNT(user_id) AS userCount FROM user GROUP BY state ORDER BY userCount DESC";
                using (var cmd = new MySqlCommand(query, _connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var userCountByState = new UserCountByStateDto
                            {
                                State = reader["state"].ToString(),
                                UserCount = Convert.ToInt32(reader["userCount"])
                            };
                            userCounts.Add(userCountByState);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return Ok(userCounts);
        }

        [HttpGet("UserAges")]
        public async Task<ActionResult<IEnumerable<UserAgeDto>>> GetUserAges()
        {
            var userAges = new List<UserAgeDto>();
            try
            {
                _connection.Open();
                var query = @"
         SELECT TIMESTAMPDIFF(YEAR, birth_date, CURDATE()) AS Age, COUNT(*) AS Count
         FROM user
         GROUP BY Age
         ORDER BY Count DESC;";
                using (var command = new MySqlCommand(query, _connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            userAges.Add(new UserAgeDto
                            {
                                Age = reader.GetInt32("Age"),
                                UserCount = reader.GetInt32("Count")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return Ok(userAges);
        }

        [HttpGet("UserGenders")]
        public async Task<ActionResult<IEnumerable<UserGenderDto>>> GetUserGenders()
        {
            var userGenders = new List<UserGenderDto>();
            try
            {
                _connection.Open();
                var query = "select count(*) as user_count, gender from user group by gender;";
                using (var command = new MySqlCommand(query, _connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            userGenders.Add(new UserGenderDto
                            {
                                Gender = reader["gender"].ToString(),
                                UserCount = reader.GetInt32("user_count")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return Ok(userGenders);
        }

        [HttpGet("CoinsByState")]
        public async Task<ActionResult<IEnumerable<CoinsStateDto>>> GetCoinsByState()
        {
            var coinsByState = new List<CoinsStateDto>();
            try
            {
                _connection.Open();
                var query = "SELECT state, SUM(coins) AS total_coins FROM user GROUP BY state ORDER BY total_coins DESC;";
                using (var command = new MySqlCommand(query, _connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            coinsByState.Add(new CoinsStateDto
                            {
                                State = reader["state"].ToString(),
                                TotalCoins = reader.GetInt32("total_coins")
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return Ok(coinsByState);

        }
        [HttpPut("UpdateUserCoins")]
        public ActionResult UpdateUserCoins([FromQuery] string email, [FromQuery] int newCoins)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("El correo electrónico es necesario para la actualización.");
            }

            try
            {
                _connection.Open();

                using (var cmd = new MySqlCommand("UPDATE user SET coins = @NewCoins WHERE email = @Email", _connection))
                {
                    cmd.Parameters.AddWithValue("@NewCoins", newCoins);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("La cantidad de monedas ha sido actualizada con éxito.");
                    }
                    else
                    {
                        return NotFound("Usuario no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }





        public class UserLoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponseDto
        {
            public string Email { get; set; }
            public string Message { get; set; }
            public string Rol { get; set; }
        }

        public class UserCountByStateDto
        {
            public string State { get; set; }
            public int UserCount { get; set; }
        }

        public class UserAgeDto
        {
            public int Age { get; set; }
            public int UserCount { get; set; }
        }

        public class UserGenderDto
        {
            public string Gender { get; set; }
            public int UserCount { get; set; }
        }

        public class CoinsStateDto
        {
            public string State { get; set; }
            public int TotalCoins { get; set; }
        }


    }
}