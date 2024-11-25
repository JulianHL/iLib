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

        public List<Book> GetBooksByFaculty(string faculty)
        {
            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            List<Book> books = _dB.GetBooksByFaculty(connection, faculty);
            if (books == null)
            {
                throw new Exception("books is null");
            }
            return books;
        }
    }
}
