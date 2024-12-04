using iLib.Repositories;
using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class BookService : BaseService
    {
        protected DBBooksTable _dB;

        public BookService()
        {
            _dB = new DBBooksTable();
        }

        public List<Book> GetBooksByFaculty(string bookfaculty)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<Book>? books = _dB.GetBooksByFaculty(connection, bookfaculty);
            if (books == null)
            {
                throw new Exception("books is null");
            }
            return books;
        }

        public Book GetBookByIsbn(string bookIsbn)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            Book? book = _dB.GetBookByIsbn(connection, bookIsbn);
            if (book == null)
            {
                throw new Exception("books is null");
            }
            return book;
        }

        public List<Book> GetAllBooks()
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<Book>? books = _dB.GetAllBooks(connection);
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
                if (!_dB.AddBook(connection, transaction, book))
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
                if (!_dB.UpdateBook(connection, transaction, book))
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





    }
}
