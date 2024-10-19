using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Dto
{

    //El dto se usa para poder simplificar la comunicacion, proteger integridad, mejorar la performance, 
    //y facil serializar

    //hay etiquetas de validacion predeterminadas, y tambien se pueden generar etiquetas personales (nose como)
    //mirar "google"
    //ETIQUETAS EJEMPLO: [requirede] / [EmailAddress]...
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
