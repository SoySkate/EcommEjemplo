using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;

        }
        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public Product GetProduct(string name)
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public Product GetProduct(decimal price)
        {
            return _context.Products.Where(p => p.Price == price).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p=>p.Id).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p=>p.Id == productId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
