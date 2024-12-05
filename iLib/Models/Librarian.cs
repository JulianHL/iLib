namespace iLib.Models
{
    public class Librarian : User
    {
        public Librarian()
        {
            UserRole = "Librarian";
        }
        public string LibrarianFirstName { get; set; }
        public string LibrarianLastName { get; set; }
    }
}
