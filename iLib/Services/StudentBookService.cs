using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class StudentBookService : BaseService
    {

        public List<StudentBook> GetAllStudentBooksByStudentId(int userId)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            DBStudentBooksTable dBStudentBooks = new DBStudentBooksTable();
            List<StudentBook>? studentBooks = dBStudentBooks.GetAllStudentBooksByStudentId(connection, userId);
            if (studentBooks == null)
            {
                throw new Exception("studentBooks is null");
            }

            
            return studentBooks;

        }

        public string AddStudentBooks(int userId, string bookIsbn)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                return "The connection was not established, connection is null";
            }

            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            DBStudentBooksTable dBStudentBooks = new DBStudentBooksTable();
            if (dBStudentBooks.ConflictStudentBooks(connection, transaction, userId, bookIsbn))
            {
                return "You borrowed this book already";
            }

            DBBooksTable dBBooks = new DBBooksTable();
            if (!dBBooks.ReduceBookQuantity(connection, transaction, bookIsbn))
            {
                return "There is no copies available";
            }

            DateOnly startingDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly dueDate = startingDate.AddMonths(1);
            if (!dBStudentBooks.AddStudentBooks(connection, transaction, userId, bookIsbn, startingDate, dueDate))
            {
                return "There was internal error, the book was not borrowed";
            }
            return "The borrowing process was successful";

        }

    }
}
