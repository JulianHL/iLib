using iLib.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace iLib.Repositories
{
    public class DBLibrarianBooksTable : DBBooksTable
    {
        public List<Book>? GetAllBooks(SqlConnection connection)
        {
            List<Book> librarianBooks;
            string storedProcedure = "[dbo].[GetAllBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using SqlDataReader reader = command.ExecuteReader();
            librarianBooks = new List<Book>();
            while (reader.Read())
            {
                librarianBooks.Add(new Book
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),
                    BookQuantity = reader.GetInt32(3),
                    BookPublisher = reader.GetString(4),
                    BookGenre = reader.GetString(5),
                    BookLanguage = reader.GetString(6),
                    BookFormat = reader.GetString(7),
                    BookDescription = reader.IsDBNull(8) ? null : reader.GetString(8),
                    BookEdition = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    BookPages = reader.IsDBNull(10) ? null : reader.GetInt32(10),
                    BookPublicationDate = reader.IsDBNull(11) ? null : DateOnly.FromDateTime(reader.GetDateTime(11))
                });
            }
            return librarianBooks;
        }

        public bool AddBook(SqlConnection connection, SqlTransaction transaction, Book book)
        {
            string storedProcedure = "[dbo].[AddBook]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Book_Isbn", book.BookIsbn);
            command.Parameters.AddWithValue("@Book_Title", book.BookTitle);
            command.Parameters.AddWithValue("@Book_Author", book.BookAuthor);
            command.Parameters.AddWithValue("@Book_Quantity", book.BookQuantity);
            command.Parameters.AddWithValue("@Book_Publisher", book.BookPublisher);
            command.Parameters.AddWithValue("@Book_Genre", book.BookGenre);
            command.Parameters.AddWithValue("@Book_Language", book.BookLanguage);
            command.Parameters.AddWithValue("@Book_Format", book.BookFormat);
            command.Parameters.AddWithValue("@Book_Description", (object?)book.BookDescription ?? DBNull.Value);
            command.Parameters.AddWithValue("@Book_Edition", (object?)book.BookEdition ?? DBNull.Value);
            command.Parameters.AddWithValue("@Book_Pages", (object?)book.BookPages ?? DBNull.Value);
            command.Parameters.AddWithValue("@Book_PublicationDate", (object?)book.BookPublicationDate ?? DBNull.Value);

            int affectedRows = command.ExecuteNonQuery();
            return affectedRows != 0;
        }

        public bool UpdateBook(SqlConnection connection, SqlTransaction transaction, Book book)
        {
            string storedProcedure = "[dbo].[UpdateBook]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Book_Isbn", book.BookIsbn);
            command.Parameters.AddWithValue("@Book_Title", book.BookTitle);
            command.Parameters.AddWithValue("@Book_Author", book.BookAuthor);
            command.Parameters.AddWithValue("@Book_Quantity", book.BookQuantity);
            command.Parameters.AddWithValue("@Book_Publisher", book.BookPublisher);
            command.Parameters.AddWithValue("@Book_Genre", book.BookGenre);
            command.Parameters.AddWithValue("@Book_Language", book.BookLanguage);
            command.Parameters.AddWithValue("@Book_Format", book.BookFormat);
            command.Parameters.AddWithValue("@Book_Description", (object?)book.BookDescription ?? DBNull.Value);
            command.Parameters.AddWithValue("@Book_Edition", (object?)book.BookEdition ?? DBNull.Value);
            command.Parameters.AddWithValue("@Book_Pages", (object?)book.BookPages ?? DBNull.Value);
            command.Parameters.AddWithValue("@Book_PublicationDate", (object?)book.BookPublicationDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@Last_Modified_Date", DateOnly.FromDateTime(DateTime.Now));

            int affectedRows = command.ExecuteNonQuery();
            return affectedRows > 0;
        }

        public List<StudentBook>? GetBorrowedBooks(SqlConnection connection)
        {
            List<StudentBook> borrowedBooks;
            string storedProcedure = "[dbo].[GetBorrowedBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using SqlDataReader reader = command.ExecuteReader();
            borrowedBooks = new List<StudentBook>();
            while (reader.Read())
            {
                borrowedBooks.Add(new StudentBook
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),
                    StudentFirstName = reader.GetString(3),
                    StudentLastName = reader.GetString(4),
                    BookStartingDate = DateOnly.FromDateTime(reader.GetDateTime(5)),
                    BookDueDate = DateOnly.FromDateTime(reader.GetDateTime(6))
                });
            }
            return borrowedBooks;
        }
    }
}

