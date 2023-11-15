// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api;

// La clase 'purse' representa un purse con propiedades específicas.
public class purse
{
    public int purse_id { get; set; }// Propiedad 'purse_id': Almacena un identificador único para cada purse.
    public int user_id { get; set; }// Propiedad 'user_id': Almacena un identificador único para cada user.
    public int? match_id { get; set; } // Nullable to handle DEFAULT NULL
    public int coins { get; set; }// Propiedad 'coins': Almacena las monedas.
    public int transaction_id { get; set; }// Propiedad 'transaction_id': Almacena un identificador único para cada transaction.
    public decimal cost { get; set; }// Propiedad 'cost': Almacena el costo.

    // Constructor por defecto de la clase 'purse'.
    // Se utiliza para crear instancias de 'purse' con propiedades inicializadas por defecto.
    public purse()
    {
    }
}
