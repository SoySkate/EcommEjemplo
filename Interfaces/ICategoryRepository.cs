using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int catId);   
        Category GetCategory(string name);  
        Category GetCategoryByAProduct(int productId);
        bool CategoryExists(int catId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
