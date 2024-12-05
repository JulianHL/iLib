using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBStudentsTable : DBUsersTable
    {
        public string? GetStudentFacultyByUserName(SqlConnection connection, string username)
        {
            string storedProcedure = "[dbo].[GetUserFacultyByUserName]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_UserName", username);
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return reader.GetString(0);
        }
    }
}
