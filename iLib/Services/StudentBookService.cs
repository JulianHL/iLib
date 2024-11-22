using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class StudentBookService : BaseService
    {

        public List<StudentBook> GetAllStudentBooksByStudentId(int userId)
        {
            using (SqlConnection? connection = EstablishConnection())
            {
                if (connection == null)
                {
                    throw new Exception("The connection was not established, connection is null");
                }

                connection.Open();
                DBStudentBooksTable dB = new DBStudentBooksTable();
                List<StudentBook>? studentBooks = dB.GetAllStudentBooksByStudentId(connection,userId);
                if (studentBooks == null)
                {
                    throw new Exception("Internal Server Error");
                }

                return studentBooks;
            }
        }

        public bool AddStudentBooks(int userId, string bookIsbn)
        {
            using (SqlConnection? connection = EstablishConnection())
            {
                if (connection == null)
                {
                    throw new Exception("The connection was not established, connection is null");
                }

                connection.Open();
                DBStudentBooksTable db = new DBStudentBooksTable();
                if(db.ConflictStudentBooks(connection, bookIsbn))
                {
                    throw new Exception("You borrowed this book already");
                }

                DateOnly startingDate = DateOnly.FromDateTime(DateTime.Now);
                DateOnly dueDate = startingDate.AddMonths(1);
                if (!db.AddStudentBooks(connection, userId, bookIsbn, startingDate, dueDate))
                {
                    return false;
                }

                return true;
            }
        }

    }
}
