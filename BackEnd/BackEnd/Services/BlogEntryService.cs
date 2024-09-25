using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class BlogEntryService : IBlogEntryService
    {
        private readonly BlogAppDbContext _context;

        public BlogEntryService(BlogAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogEntryDto>> GetAllEntries(int page, int pageSize, string filter)
        {
            var query = from entry in _context.BlogsEntrys
                        join category in _context.Categories on entry.CategoryId equals category.Id
                        join user in _context.Users on entry.AuthorId equals user.Id
                        select new
                        {
                            id = entry.Id,
                            Content = entry.Content,
                            Title = entry.Title,
                            CategoryName = category.Description,
                            UserName = user.FirstName + " " + user.LastName,
                            PublicationDate = entry.PublicationDate,
                        };

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(e => e.Title.Contains(filter) ||
                                         e.CategoryName.Contains(filter) ||
                                         e.UserName.Contains(filter));
            }

            var entries = await query
                .OrderBy(e => e.PublicationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entries.Select(e => new BlogEntryDto
            {
                id = e.id,
                Content = e.Content,
                Title = e.Title,
                CategoryName = e.CategoryName,
                UserName = e.UserName,
                PublicationDate = e.PublicationDate
            }).ToList();
        }


        public async Task<BlogEntry> GetEntryById(Guid id)
        {
            return await _context.BlogsEntrys.Include(b => b.Id).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogEntry> AddEntry(BlogEntry entry)
        {
            try
            {
                _context.BlogsEntrys.Add(entry);
                await _context.SaveChangesAsync();
                return entry;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la entrada: {ex.Message}");
                throw;
            }
        }


        public async Task<BlogEntry> UpdateEntry(Guid id, string title, string content, string category, string user)
        {
            var entry = await _context.BlogsEntrys.FindAsync(id);

            if (entry == null)
            {
                throw new KeyNotFoundException("Entrada no encontrada");
            }

            if (!Guid.TryParse(category, out Guid categoryId))
            {
                throw new FormatException("El formato de category no es válido");
            }

            if (!Guid.TryParse(user, out Guid userId))
            {
                throw new FormatException("El formato de user no es válido");
            }

            entry.Title = title;
            entry.Content = content;
            entry.CategoryId = categoryId; 
            entry.AuthorId = userId;       

            _context.BlogsEntrys.Update(entry);
            await _context.SaveChangesAsync();

            return entry;
        }



        public async Task<bool> DeleteEntry(Guid id)
        {
            var entry = await _context.BlogsEntrys.FindAsync(id);
            if (entry == null)
            {
                return false;
            }

            _context.BlogsEntrys.Remove(entry);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
