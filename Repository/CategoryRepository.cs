using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Repository
{
    //funciones que se usaran para acciones CRUD + interface(funciones declaradas)
    //el repository se intercomunica directamente con el context
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int catId)
        {
            return _context.Categories.Any(c=>c.Id== catId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c=>c.Id).ToList();
        }

        public Category GetCategory(int catId)
        {
            return _context.Categories.Where(c => c.Id == catId).FirstOrDefault();
        }

        public Category GetCategory(string name)
        {
            return _context.Categories.Where(c => c.Name == name).FirstOrDefault();
        }

        public Category GetCategoryByAProduct(int productId)
        {
            var product =  _context.Products.Where(p=>p.Id==productId).FirstOrDefault();
            return product.Category;

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
