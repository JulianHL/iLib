using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class StudentBookService : BaseService
    {

        public List<StudentBook> GetAllStudentBooksByStudentId(int user_Id)
        {
            using (SqlConnection? connection = EstablishConnection())
            {
                if (connection == null)
                {
                    throw new Exception("The connection was not established, connection is null");
                }

                connection.Open();
                DBStudentBooksTable dB = new DBStudentBooksTable();
                List<StudentBook>? studentBooks = dB.GetAllStudentBooksByStudentId(connection,user_Id);
                if (studentBooks == null)
                {
                    throw new Exception("The connection was not established, connection is null");
                }

                return studentBooks;
            }
        }

    }
}
