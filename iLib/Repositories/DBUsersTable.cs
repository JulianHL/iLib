using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBUsersTable
    {
        public User? ValidateUser(SqlConnection connection, string username, string password)
        {
            string storedProcedure = "[dbo].[ValidateUser]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_UserName", username);
            command.Parameters.AddWithValue("@User_Password", password);
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new User()
            {
                UserId= reader.GetInt32(0),
                UserName = reader.GetString(1),
                UserRole = reader.GetString(2),
            };
        }

        public int getUserIdByUserName(SqlConnection connection, string username)
        {
            string storedProcedure = "[dbo].[GetUserIdByUserName]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_UserName", username);
            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return 0;
            }

            return reader.GetInt32(0);
        }

        public string? getUserRoleByUserName(SqlConnection connection, string username)
        {
            string storedProcedure = "[dbo].[GetUserRoleByUserName]";
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
