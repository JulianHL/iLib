namespace iLib.Models
{
    public class Librarian : User
    {
        public int Librarian_Id { get; set; }
        public int User_Id { get; set; }
        public string Librarian_FirstName { get; set; }
        public string Librarian_LastName { get; set; }
        public DateTime Hire_Date { get; set; }
        public string? Department { get; set; }
    }
}
