// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api;

// La clase 'inventory' representa un inventario con propiedades específicas.
public class inventory
{
    public int user_id { get; set; } // Propiedad 'user_id': Almacena un identificador único para cada usuario.
    public int item_id { get; set; } // Propiedad 'item_id': Almacena un identificador único para cada item.

    // Constructor por defecto de la clase 'inventory'.
    // Se utiliza para crear instancias de 'inventory' con propiedades inicializadas por defecto.
    public inventory()
    {
    }
}

