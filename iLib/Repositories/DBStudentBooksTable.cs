
using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBStudentBooksTable
    {
        public List<StudentBooks>? GetAllStudentBooksByStudentId(SqlConnection connection)
        {
            List<StudentBooks> StudentBooks;
            string storedProcedure = "[dbo].[GetStudentBooks]";
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    StudentBooks = new List<StudentBooks>();
                    while (reader.Read())
                    {
                        StudentBooks.Add(new StudentBooks
                        {
                            BookTitle = reader.GetString(0),
                            BookAuthor = reader.GetString(1),
                            BookQuantity = reader.GetInt32(2),
                            BookPublisher = reader.GetString(3),
                            BookGenre = reader.GetString(4),
                            BookLanguage = reader.GetString(5),
                            BookFormat = reader.GetString(6),
                            BookDescription = reader.IsDBNull(7)?null : reader.GetString(7),
                            BookEdition = reader.IsDBNull(8)?null : reader.GetString(8),
                            BookPages = reader.IsDBNull(9)?null : reader.GetInt32(9),
                            BookPublicationDate = reader.IsDBNull(10)?null : DateOnly.FromDateTime(reader.GetDateTime(10)),
                            BookStartingDate = DateOnly.FromDateTime(reader.GetDateTime(11)),
                            BookDueDate = DateOnly.FromDateTime(reader.GetDateTime(12))

                        });
                    }
                }

            }
            return StudentBooks;

        }
    }
}
