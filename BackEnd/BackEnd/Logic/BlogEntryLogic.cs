using BackEnd.Controllers;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Logic
{
    public class BlogEntryLogic : IBlogEntryLogic
    {

        

        private readonly IBlogEntryService _blogEntryService;
        private readonly ICategoryService _categoryService;

        public BlogEntryLogic(IBlogEntryService blogEntryService, ICategoryService categoryService)
        {
            _blogEntryService = blogEntryService;
            _categoryService = categoryService;

        }

        public async Task<List<BlogEntryDto>> GetAllEntries(int page, int pageSize, string filter)
        {
            return await _blogEntryService.GetAllEntries(page, pageSize, filter);
        }


        public async Task<BlogEntry> GetEntryById(Guid id)
        {
            return await _blogEntryService.GetEntryById(id);
        }

        public async Task<BlogEntry> CreateEntry(string title, string content, string categoryId,string userId)
        {
            if (!Guid.TryParse(categoryId, out Guid categoryGuid))
            {
                throw new ArgumentException("El ID de la categoría no es válido.");
            }

            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                throw new ArgumentException("El ID del autor no es válido.");
            }

            var category = await _categoryService.GetCategoryById(categoryGuid);
            if (category == null)
            {
                throw new ArgumentException("La categoría no existe.");
            }



            var entry = new BlogEntry
            {
                Id = Guid.NewGuid(),  
                Title = title,
                Content = content,
                CategoryId = category.Id,
                PublicationDate = DateTime.UtcNow,  
                AuthorId = userGuid
            };

            return await _blogEntryService.AddEntry(entry);
        }

        public async Task<BlogEntry> UpdateEntry(Guid id, string title, string content, string categoryId, string userId)
        {
            return await _blogEntryService.UpdateEntry(id,title,content,categoryId,userId);
        }

        public async Task<bool> DeleteEntry(Guid id)
        {
            return await _blogEntryService.DeleteEntry(id);
        }
    }
}
