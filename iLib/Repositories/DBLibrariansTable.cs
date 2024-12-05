using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBLibrariansTable : DBUserTable
    {

        public bool AddLibrarian(SqlConnection connection, SqlTransaction transaction, Librarian librarian)
        {
            string storedProcedure = "[dbo].[AddStudent]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_UserId", librarian.UserId);
            command.Parameters.AddWithValue("@Student_FirstName", librarian.LibrarianFirstName);
            command.Parameters.AddWithValue("@Student_LastName", librarian.LibrarianLastName);

            int affectedRows = command.ExecuteNonQuery();
            return affectedRows != 0;
        }
    }
}
