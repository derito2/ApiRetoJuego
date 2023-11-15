// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
using System;

// La clase 'user' representa un user con propiedades específicas.
public class user
{
    public int user_id { get; set; }// Propiedad 'user_id': Almacena un identificador único para cada user.
    public string name { get; set; }// Propiedad 'name': Almacena el nombre del user.
    public string last_name { get; set; }// Propiedad 'last_name': Almacena los apellidos del user.
    public DateTime birth_date { get; set; }// Propiedad 'birth_date': Almacena fecha de nacimiento del user.
    public string gamertag { get; set; }// Propiedad 'gamertag': Almacena el gamertag del user.
    public int coins { get; set; }// Propiedad 'coins': Almacena las monedas del user.
    public string gender { get; set; }// Propiedad 'gender': Almacena el genero del user.
    public string state { get; set; }// Propiedad 'state': Almacena el estado donde vive el user.
    public string email { get; set; }// Propiedad 'email': Almacena el email del user.
    public string rol { get; set; }// Propiedad 'rol': Almacena el rol del user.

    // Constructor por defecto de la clase 'user'.
    // Se utiliza para crear instancias de 'user' con propiedades inicializadas por defecto.
    public user()
    {
    }
}

