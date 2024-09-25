namespace BackEnd.Models
{
    public class BlogEntryDto
    {
        public Guid id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
