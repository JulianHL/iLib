using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class UserService : BaseService
    {
        DBUserTable _dB;
        public UserService()
        {
            _dB = new DBUserTable();
        }
        public User validateUser(string username, string password)
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
            User? user = _dB.validateUser(connection, username, password);

            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }

            return user;
        }
    }
}
