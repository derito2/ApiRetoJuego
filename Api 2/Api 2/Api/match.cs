// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
using System;

// La clase 'match' representa un itematchms con propiedades específicas.
public class match
{
    public int match_id { get; set; }// Propiedad 'match_id': Almacena un identificador único para cada match.
    public int user_id { get; set; }// Propiedad 'user_id': Almacena un identificador único para cada user.
    public int score { get; set; }// Propiedad 'score': Almacena el puntaje.

    // Constructor por defecto de la clase 'match'.
    // Se utiliza para crear instancias de 'match' con propiedades inicializadas por defecto.
    public match()
    {
    }
}
