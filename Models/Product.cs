using System.ComponentModel;

namespace EcommerceEjemploApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int? QuantityStock { get; set; }
        public string Image {  get; set; }
        public Category Category { get; set; }

    }
}
