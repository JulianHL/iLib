
using iLib.Models;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace iLib.Repositories
{
    public class DBStudentBooksTable : DBBooksTable
    {
        public List<StudentBook>? GetAllStudentBooksByStudentId(SqlConnection connection, int userId)
        {
            List<StudentBook> StudentBooks;
            string storedProcedure = "[dbo].[GetStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);

            using SqlDataReader reader = command.ExecuteReader();
            StudentBooks = new List<StudentBook>();
            while (reader.Read())
            {
                StudentBooks.Add(new StudentBook
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),

                });
            }
            return StudentBooks;

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

            if (reader.HasRows)
            {
                return true;
            }
            return false;

        }
        
    }
}
