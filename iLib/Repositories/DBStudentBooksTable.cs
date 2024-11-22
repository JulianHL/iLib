
using iLib.Models;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace iLib.Repositories
{
    public class DBStudentBooksTable
    {
        public List<StudentBook>? GetAllStudentBooksByStudentId(SqlConnection connection, int user_Id)
        {
            List<StudentBook> StudentBooks;
            string storedProcedure = "[dbo].[GetStudentBooks]";
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", user_Id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    StudentBooks = new List<StudentBook>();
                    while (reader.Read())
                    {
                        StudentBooks.Add(new StudentBook
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
                            BookPublicationDate = reader.IsDBNull(11) ? null : DateOnly.FromDateTime(reader.GetDateTime(11)),
                            BookStartingDate = DateOnly.FromDateTime(reader.GetDateTime(12)),
                            BookDueDate = DateOnly.FromDateTime(reader.GetDateTime(13))

                        });
                    }
                }

            }
            return StudentBooks;

        }

        public bool AddStudentBooks(SqlConnection connection,int user_Id, StudentBook studentBook)
        {
            bool isStudentBookAdded = false;
            string storedProcedure = "";
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", user_Id);
                command.Parameters.AddWithValue("@Book_Isbn", studentBook.BookIsbn);
                command.Parameters.AddWithValue("@Starting_Date",studentBook.BookStartingDate);
                command.Parameters.AddWithValue("@Due_Date", studentBook.BookDueDate);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    isStudentBookAdded = true;
                }

            }
            return isStudentBookAdded;
        }
    }
}
