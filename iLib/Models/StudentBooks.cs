namespace iLib.Models
{
    public class BorrowedBook : Book
    {
        public DateOnly BookStartingDate { get; set; }
        public DateOnly BookDueDate { get; set; }
    }
}
