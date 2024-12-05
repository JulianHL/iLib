using iLib.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;

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


        public bool AddUser(SqlConnection connection, SqlTransaction transaction, User user)
        {
            string storedProcedure = "[dbo].[AddUser]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", user.UserId);
            command.Parameters.AddWithValue("@User_UserName", user.UserName);
            command.Parameters.AddWithValue("@User_Password", user.UserPassword);
            command.Parameters.AddWithValue("@User_UserPassword", user.UserPassword);
            command.Parameters.AddWithValue("@User_Role", user.UserRole);
            command.Parameters.AddWithValue("@User_Email", string.IsNullOrWhiteSpace(user.UserEmail) ? DBNull.Value : user.UserEmail);
            command.Parameters.AddWithValue("@User_PhoneNumber", user.UserPhoneNumber == 0 ?DBNull.Value : user.UserPhoneNumber);

            int affectedRows = command.ExecuteNonQuery();
            return affectedRows != 0;
        }
    }

    }
