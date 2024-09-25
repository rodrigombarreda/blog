using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogAppDbContext _context;

        public CategoryService(BlogAppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
