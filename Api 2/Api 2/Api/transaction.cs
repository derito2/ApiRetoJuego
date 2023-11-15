// Define el espacio de nombres Api, que agrupa clases relacionadas con la API.
namespace Api;

// La clase 'transaction' representa un transaction con propiedades específicas.
public class transaction
{
    public int transaction_id { get; set; }// Propiedad 'transaction_id': Almacena un identificador único para cada transaction.
    public string typeOftransaction { get; set; }// Propiedad 'typeOftransaction': Almacena los tipos de transaction.

    // Constructor por defecto de la clase 'transaction'.
    // Se utiliza para crear instancias de 'transaction' con propiedades inicializadas por defecto.
    public transaction()
    {
    }
}
