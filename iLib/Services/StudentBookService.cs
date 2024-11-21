using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class StudentBookService : BaseService
    {

        public List<StudentBook> GetAllStudentBooksByStudentId()
        {
            using (SqlConnection? connection = EstablishConnection())
            {
                if (connection == null)
                {
                    throw new Exception("The connection was not established, connection is null");
                }

                DBStudentBooksTable dB = new DBStudentBooksTable();
                List<StudentBook> studentBooks = new List<StudentBook>();
                if (studentBooks == null)
                {
                    throw new Exception("The connection was not established, connection is null");
                }

                return studentBooks;
            }
        }

    }
}
