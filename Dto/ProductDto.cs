using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? QuantityStock { get; set; }
        public string Image { get; set; }
        //dice chat que es mejor poner el dto aqui que no el propio model
        public int CategoryId { get; set; }
    }
}
