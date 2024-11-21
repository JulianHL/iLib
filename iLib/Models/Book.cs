namespace iLib.Models
{
    public class Book
    {
        public String BookIsbn { get; set; }
        public String BookTitle { get; set; }
        public String BookAuthor {  get; set; }
        public int BookQuantity { get; set; }
        public String BookPublisher { get; set; }
        public String BookGenre { get; set; }
        public String BookLanguage { get; set; }
        public String BookFormat { get; set; }
        public String? BookDescription { get; set; }
        public String? BookEdition { get; set; }
        public int? BookPages { get; set; }
        public DateOnly? BookPublicationDate { get; set; }

    }
}
