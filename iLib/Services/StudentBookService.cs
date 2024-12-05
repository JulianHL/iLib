using iLib.Models;
using iLib.Repositories;
using Microsoft.AspNetCore.Mvc;
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
                    throw new Exception("You borrowed this book already");
                }


                if (!_dB.ReduceBookQuantity(connection, transaction, bookIsbn))
                {
                    throw new Exception("There are no copies available");
                }

                DateOnly startingDate = DateOnly.FromDateTime(DateTime.Now);
                DateOnly dueDate = startingDate.AddMonths(1);
                if (!((DBStudentBooksTable)_dB).AddStudentBooks(connection, transaction, userId, bookIsbn, startingDate, dueDate))
                {
                    return "There was internal error, the book was not borrowed";
                }
                transaction.Commit();
                return "The borrowing process was successful";

            }catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public StudentBook GetStudentBookByIsbn(int userId, string bookIsbn)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            StudentBook? studentBook = ((DBStudentBooksTable)_dB).GetStudentBookByIsbn(connection,userId, bookIsbn);
            if (studentBook == null)
            {
                throw new Exception("books is null");
            }
            return studentBook;
        }

        public List<StudentBook>? SearchStudentBooks(string searchTerm)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();;
            DBStudentBooksTable dbStudentBooksTable = (DBStudentBooksTable)_dB;
            List<StudentBook>? studentBooks = dbStudentBooksTable.SearchStudentBooks(connection, searchTerm);
            if (studentBooks == null)
            {
                throw new Exception("No student books found for the given search term");
            }

            return studentBooks;
        }

    }
}
