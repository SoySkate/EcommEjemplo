namespace EcommerceEjemploApi.Dto
{

    //El dto se usa para poder simplificar la comunicacion, proteger integridad, mejorar la performance, 
    //y facil serializar

    //hay etiquetas de validacion predeterminadas, y tambien se pueden generar etiquetas personales (nose como)
    //mirar "google"
    //ETIQUETAS EJEMPLO: [requirede] / [ EmailAddress]...
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateTime { get; set; }

        //foreign keys:
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
