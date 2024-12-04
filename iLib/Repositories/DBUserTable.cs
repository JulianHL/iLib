using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBUserTable
    {
        public User? validateUser(SqlConnection connection, string username, string password)
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
    }
}
