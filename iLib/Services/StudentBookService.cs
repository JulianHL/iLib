using iLib.Models;
using iLib.Repositories;
using iLib.Exceptions;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class StudentBookService : BookService
    {
        public StudentBookService()
        {
            _dB = new DBStudentBooksTable();
        }

        public List<StudentBook> GetAllStudentBooks()
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<StudentBook>? studentBooks = ((DBStudentBooksTable)_dB).GetAllStudentBooks(connection);
            if (studentBooks == null)
            {
                throw new Exception("borrowed books list is null");
            }

            return studentBooks;
        }

        public List<StudentBook> GetAllStudentBooksByStudentId(int userId)
        {
            if (userId == 0)
            {
                throw new Exception("Invalid user id");
            }

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<StudentBook>? studentBooks = ((DBStudentBooksTable)_dB).GetAllStudentBooksByStudentId(connection, userId);
            if (studentBooks == null)
            {
                throw new Exception("studentBooks is null");
            }


            return studentBooks;

        }

        public string AddStudentBooks(int userId, string bookIsbn)
        {
            if (userId == 0)
            {
                throw new Exception("Invalid user id");
            }

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (((DBStudentBooksTable)_dB).ConflictStudentBooks(connection, transaction, userId, bookIsbn))
                {
                    throw new ConflictException("The book is already borrowed");
                }


                if (!_dB.ReduceBookQuantity(connection, transaction, bookIsbn))
                {
                    throw new ConflictException("There are no copies available");
                }

                DateOnly startingDate = DateOnly.FromDateTime(DateTime.Now);
                DateOnly dueDate = startingDate.AddMonths(1);
                if (!((DBStudentBooksTable)_dB).AddStudentBooks(connection, transaction, userId, bookIsbn, startingDate, dueDate))
                {
                    throw new Exception("The book was not borrowed");
                }
                transaction.Commit();
                return "The book was successfully borrowed";

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public StudentBook GetStudentBookByIsbn(int userId, string bookIsbn)
        {
            if (userId == 0)
            {
                throw new Exception("Invalid user id");
            }

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            StudentBook? studentBook = ((DBStudentBooksTable)_dB).GetStudentBookByIsbn(connection, userId, bookIsbn);
            if (studentBook == null)
            {
                throw new Exception("books is null");
            }
            return studentBook;
        }


    }
}
