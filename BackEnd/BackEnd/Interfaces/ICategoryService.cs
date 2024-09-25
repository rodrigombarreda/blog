using BackEnd.Models;

namespace BackEnd.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(Category category);
        Task<Category> GetCategoryById(Guid id);
        Task<List<Category>> GetAllCategories();
    }
}
