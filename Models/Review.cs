namespace EcommerceEjemploApi.Models
{
    //Clase creada que se usara para crear objetos de esta clase
    //Se le añade si es necesario segun las relaciones las foreign key
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateTime { get; set; }

        // Claves foráneas
        public int ProductId { get; set; } //foreign key
        public Product Product { get; set; }
        public int UserId {  get; set; }//foreign key
        public User User { get; set; }
    }
}
