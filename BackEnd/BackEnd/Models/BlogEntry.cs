namespace BackEnd.Models
{
    public class BlogEntry
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; } 
    }
}
