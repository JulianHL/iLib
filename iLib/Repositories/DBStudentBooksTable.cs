
using iLib.Models;
using Microsoft.Data.SqlClient;
using System.Numerics;
using System.Windows.Input;

namespace iLib.Repositories
{
    public class DBStudentBooksTable : DBBooksTable
    {
        public List<StudentBook>? GetAllStudentBooksByStudentId(SqlConnection connection, int userId)
        {
            string storedProcedure = "[dbo].[GetStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }

            List<StudentBook> StudentBooks = new List<StudentBook>();
            while (reader.Read())
            {
                StudentBooks.Add(new StudentBook
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),
                    BookFormat = reader.GetString(3),

                });
            }
            return StudentBooks;

        }

        public List<StudentBook>? GetAllStudentBooks(SqlConnection connection)
        {
            List<StudentBook> borrowedBooks;
            string storedProcedure = "[dbo].[GetAllStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using SqlDataReader reader = command.ExecuteReader();
            borrowedBooks = new List<StudentBook>();
            while (reader.Read())
            {
                borrowedBooks.Add(new StudentBook
                {

                    BookStudent = new Student
                    {
                        StudentFirstName = reader.GetString(0),
                        StudentLastName = reader.GetString(1),
                    },
                    BookIsbn = reader.GetString(2),
                    BookTitle = reader.GetString(3),
                    BookAuthor = reader.GetString(4),
                    BookStartingDate = DateOnly.FromDateTime(reader.GetDateTime(5)),
                    BookDueDate = DateOnly.FromDateTime(reader.GetDateTime(6))
                });
            }
            return borrowedBooks;
        }

        public bool AddStudentBooks(SqlConnection connection, SqlTransaction transaction, int userId, string bookIsbn, DateOnly startingDate, DateOnly dueDate)
        {
            string storedProcedure = "[dbo].[AddStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);
            command.Parameters.AddWithValue("@Starting_Date", startingDate);
            command.Parameters.AddWithValue("@Due_Date", dueDate);

            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                return false;
            }

            return true;


        }

        public bool ConflictStudentBooks(SqlConnection connection, SqlTransaction transaction, int userId, string bookIsbn)
        {
            string storedProcedure = "[dbo].[ConflictStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return false;
            }

            return true;

        }

        public StudentBook? GetStudentBookByIsbn(SqlConnection connection, int userId, string bookIsbn)
        {

            string storedProcedure = "[dbo].[GetStudentBookByIsbn]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            StudentBook studentbook = new StudentBook
            {
                BookIsbn = reader.GetString(0),
                BookTitle = reader.GetString(1),
                BookAuthor = reader.GetString(2),
                BookQuantity = reader.GetInt32(3),
                BookPublisher = reader.GetString(4),
                BookGenre = reader.GetString(5),
                BookFaculty = reader.GetString(6),
                BookLanguage = reader.GetString(7),
                BookFormat = reader.GetString(8),
                BookDescription = reader.IsDBNull(9) ? null :  reader.GetString(9),
                BookEdition = reader.IsDBNull(10) ? 0 :  reader.GetInt32(10),
                BookPages = reader.IsDBNull(11) ? 0 : reader.GetInt32(11),
                BookPublicationDate = reader.IsDBNull(12) ? null : DateOnly.FromDateTime(reader.GetDateTime(12)),
                BookStartingDate = DateOnly.FromDateTime(reader.GetDateTime(13)),
                BookDueDate = DateOnly.FromDateTime(reader.GetDateTime(14))
            };
            return studentbook;
        }

        public List<StudentBook>? SearchStudentBooks(SqlConnection connection, string searchTerm)
        {
            string storedProcedure = "[dbo].[SearchBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SearchTerm", searchTerm);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }

            List<StudentBook> studentBooks = new List<StudentBook>();
            while (reader.Read())
            {
                studentBooks.Add(new StudentBook
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),
                    BookFormat = reader.GetString(3),
                });
            }

            return studentBooks;
        }

    }
}
