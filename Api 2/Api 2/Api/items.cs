// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api;

// La clase 'items' representa un items con propiedades específicas.
public class items
{
    public int item_id { get; set; } // Propiedad 'item_id': Almacena un identificador único para cada item.
    public string item_name { get; set; }  // Propiedad 'item_name': Almacena el nombre del item.

    // Constructor por defecto de la clase 'items'.
    // Se utiliza para crear instancias de 'items' con propiedades inicializadas por defecto.
    public items()
    {
    }
}
