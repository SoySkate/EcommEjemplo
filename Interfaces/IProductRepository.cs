using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //funciones que hara el repository
    //La interface se comunica directamente con el repository (bueno lo implementa)
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int productId);
        Product GetProduct(string name);
        Product GetProduct(decimal price);
        bool ProductExists(int productId);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
