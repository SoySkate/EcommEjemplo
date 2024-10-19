using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //funciones que hara el repository
    //La interface se comunica directamente con el repository (bueno lo implementa)
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
