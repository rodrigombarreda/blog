using BackEnd.Models;

namespace BackEnd.Interfaces
{
    public interface IBlogEntryService
    {
        Task<List<BlogEntryDto>> GetAllEntries(int page, int pageSize, string filter);
        Task<BlogEntry> GetEntryById(Guid id);
        Task<BlogEntry> AddEntry(BlogEntry entry);
        Task<BlogEntry> UpdateEntry(Guid id, string title, string content, string category, string user);
        Task<bool> DeleteEntry(Guid id);
    }
}
