// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api;

// La clase 'store' representa un store con propiedades específicas.
public class store
{
    public int user_id { get; set; }// Propiedad 'user_id': Almacena un identificador único para cada user.
    public int chest_id { get; set; }// Propiedad 'chest_id': Almacena un identificador único para cada chest.
    public int item_id { get; set; }// Propiedad 'item_id': Almacena un identificador único para cada item.
    public DateTime date { get; set; }// Propiedad 'date': Almacena la fecha de la compra.

    // Constructor por defecto de la clase 'store'.
    // Se utiliza para crear instancias de 'store' con propiedades inicializadas por defecto.
    public store()
    {
    }
}


