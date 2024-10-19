using EcommerceEjemploApi.Enums;

namespace EcommerceEjemploApi.Dto
{

    //El dto se usa para poder simplificar la comunicacion, proteger integridad, mejorar la performance, 
    //y facil serializar

    //hay etiquetas de validacion predeterminadas, y tambien se pueden generar etiquetas personales (nose como)
    //mirar "google"
    //ETIQUETAS EJEMPLO: [requirede] / [ EmailAddress]...
    public class OrderDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int UserId { get; set; }
    }
}
