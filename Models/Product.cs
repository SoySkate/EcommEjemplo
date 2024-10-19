using System.ComponentModel;

namespace EcommerceEjemploApi.Models
{
    //Clase creada que se usara para crear objetos de esta clase
    //Se le añade si es necesario segun las relaciones las foreign key
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int? QuantityStock { get; set; }
        public string Image {  get; set; }

        //Foreign Key:
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        // Relación de uno a muchos: un producto puede tener varias reseñas (reviews)
        public ICollection<Review> Reviews { get; set; }

    }
}
