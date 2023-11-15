namespace Api;

// La clase 'options_items' representa las options_items con propiedades específicas.
public class options_items
{
    public int chest_id { get; set; }// Propiedad 'chest_id': Almacena un identificador único para cada chest.
    public int item_id { get; set; }// Propiedad 'item_id': Almacena un identificador único para cada item.

    // Constructor por defecto de la clase 'options_items'.
    // Se utiliza para crear instancias de 'options_items' con propiedades inicializadas por defecto.
    public options_items()
    {
    }
}