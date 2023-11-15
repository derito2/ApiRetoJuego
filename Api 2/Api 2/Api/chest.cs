// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api;

// La clase 'chest' representa un cofre con propiedades específicas.
public class chest
{
    public int chest_id { get; set; }     // Propiedad 'chest_id': Almacena un identificador único para cada cofre.
    public string chest_name { get; set; }     // Propiedad 'chest_name': Almacena el nombre del cofre.
    public int price { get; set; }     // Propiedad 'price': Almacena el precio del cofre.

    // Constructor por defecto de la clase 'chest'.
    // Se utiliza para crear instancias de 'chest' con propiedades inicializadas por defecto.
    public chest()
    {
    }
}