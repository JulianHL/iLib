using Microsoft.Data.SqlClient;
using iLib.Repositories;
namespace iLib.Services
{
    public class StudentService : UserService
    {
        public StudentService() { 
            _dB = new DBStudentsTable();
        }
        public string GetStudentFacultyByUserName(string username)
        {

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            string? userFaculty = ((DBStudentsTable)_dB).GetStudentFacultyByUserName(connection, username);

            if (userFaculty == null)
            {
                throw new Exception("User not found");
            }

            return userFaculty;
        }
    }
}
