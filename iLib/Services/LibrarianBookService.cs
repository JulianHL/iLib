using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class LibrarianBookService : BookService
    {
        public LibrarianBookService()
        {
            _dB = new DBLibrarianBooksTable();
        }

        public List<Book> GetAllBooks()
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<Book>? books = ((DBLibrarianBooksTable)_dB).GetAllBooks(connection);
            if (books == null)
            {
                throw new Exception("books is null");
            }

            return books;
        }

        public string AddBook(Book book)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                return "The connection was not established, connection is null";
            }

            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (!((DBLibrarianBooksTable)_dB).AddBook(connection, transaction, book))
                {
                    throw new Exception("There was an internal error, the book was not added");
                }

                transaction.Commit();
                return "The book was successfully added";
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public string UpdateBook(Book book)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                return "The connection was not established, connection is null";
            }

            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (!((DBLibrarianBooksTable)_dB).UpdateBook(connection, transaction, book))
                {
                    return "There was an internal error, the book was not updated";
                }

                transaction.Commit();
                return "The book was successfully updated";
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<StudentBook> GetBorrowedBooks()
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<StudentBook>? borrowedBooks = ((DBLibrarianBooksTable)_dB).GetBorrowedBooks(connection);
            if (borrowedBooks == null)
            {
                throw new Exception("borrowed books list is null");
            }

            return borrowedBooks;
        }
    }
}
