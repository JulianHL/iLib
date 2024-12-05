using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class UserService : BaseService
    {
        protected DBUsersTable _dB;
        public UserService()
        {
            _dB = new DBUsersTable();
        }
        public User ValidateUser(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password are required.");
            }

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            User? user = _dB.ValidateUser(connection, username, password);

            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }

            return user;
        }
        
        public int GetUserIdByUserName(string username)
        {

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            int userId = _dB.getUserIdByUserName(connection, username);

            if (userId == 0)
            {
                throw new Exception("User not found");
            }

            return userId;

        }

        public string GetUserRoleByUserName(string username)
        {

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            string? userRole = _dB.getUserRoleByUserName(connection, username);

            if (userRole == null)
            {
                throw new Exception("User not found");
            }

            return userRole;

        }
    }
}
