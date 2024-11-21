namespace iLib.Models
{
    public class StudentBook : Book
    {
        public DateOnly BookStartingDate { get; set; }
        public DateOnly BookDueDate { get; set; }
    }
}
