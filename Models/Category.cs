namespace EcommerceEjemploApi.Models
{
    //Clase creada que se usara para crear objetos de esta clase
    //Se le añade si es necesario segun las relaciones las foreign key
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
