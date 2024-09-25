using BackEnd.Controllers;
using BackEnd.Models;

public interface IBlogEntryLogic
{
    Task<List<BlogEntryDto>> GetAllEntries(int page, int pageSize, string filter);
    Task<BlogEntry> GetEntryById(Guid id);
    Task<BlogEntry> CreateEntry(string title, string content, string category, string user);
    Task<BlogEntry> UpdateEntry(Guid id, string title, string content, string category, string user);
    Task<bool> DeleteEntry(Guid id);
}