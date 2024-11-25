using Microsoft.Data.SqlClient;
using iLib.Models;
using System.Transactions;

namespace iLib.Repositories
{
    public class DBBooksTable
    {

        public List<Book> GetBooksByFaculty(SqlConnection connection, string faculty)
        {
            List<Book> books;
            string storedProcedure = "[dbo].[GetBooksByFaculty]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Faculty_Name", faculty);
            using SqlDataReader reader = command.ExecuteReader();
            books = new List<Book>();
            while (reader.Read())
            {
                books.Add(new Book
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),
                    BookQuantity = reader.GetInt32(3)
                });
            }

            return books;
        }

        public bool ReduceBookQuantity(SqlConnection connection, SqlTransaction transaction, string bookIsbn)
        {
            string storedProcedure = "[dbo].[ReduceBookQuantity]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);
            
            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                return false;
            }
            return true;
        }
    }
}
