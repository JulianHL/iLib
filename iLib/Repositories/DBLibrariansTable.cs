using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBLibrariansTable : DBUserTable
    {

        public bool AddLibrarian(SqlConnection connection, SqlTransaction transaction, Librarian librarian)
        {
            string storedProcedure = "[dbo].[AddLibrarian]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", librarian.UserId);
            command.Parameters.AddWithValue("@Librarian_FirstName", librarian.LibrarianFirstName);
            command.Parameters.AddWithValue("@Librarian_LastName", librarian.LibrarianLastName);

            int affectedRows = command.ExecuteNonQuery();
            return affectedRows != 0;
        }
    }
}
