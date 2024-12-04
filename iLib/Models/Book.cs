namespace iLib.Models
{
    public class Book
    {
        public string BookIsbn { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor {  get; set; }
        public int BookQuantity { get; set; }
        public string BookPublisher { get; set; }
        public string BookGenre { get; set; }
        public string BookFaculty { get; set; }
        public string BookLanguage { get; set; }
        public string BookFormat { get; set; }
        public string? BookDescription { get; set; }
        public int? BookEdition { get; set; }
        public int? BookPages { get; set; }
        public DateOnly? BookPublicationDate { get; set; }

    }
}
