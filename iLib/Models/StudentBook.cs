namespace iLib.Models
{
    public class StudentBook : Book
    {
        public Student? BookStudent { get; set; }
        public DateOnly BookStartingDate { get; set; }
        public DateOnly BookDueDate { get; set; }
    }
}
