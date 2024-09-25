using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ICategoryLogic
    {
        Task<Category> CreateCategory( string description);
        Task<Category> GetCategoryById(Guid id);
        Task<List<Category>> GetAllCategories();
    }
}
