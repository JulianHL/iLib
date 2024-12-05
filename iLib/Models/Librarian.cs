namespace iLib.Models
{
    public class Librarian : User
    {
        public Student()
        {
            UserRole = "Librarian";
        }
        public string LibrarianFirstName { get; set; }
        public string LibrarianLastName { get; set; }
    }
}
