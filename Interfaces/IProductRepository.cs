using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
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
