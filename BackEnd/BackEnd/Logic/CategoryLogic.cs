using BackEnd.Interfaces;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Logic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryService _categoryService;

        public CategoryLogic(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Category> CreateCategory( string description)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Description = description
            };
            return await _categoryService.AddCategory(category);
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _categoryService.GetCategoryById(id);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryService.GetAllCategories();
        }
    }
}
