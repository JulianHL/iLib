using iLib.Models;
using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBStudentsTable : DBUserTable
    {

        public bool AddStudent(SqlConnection connection, SqlTransaction transaction, Student student)
        {
            string storedProcedure = "[dbo].[AddStudent]";
            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", student.UserId);
            command.Parameters.AddWithValue("@Student_FirstName", student.StudentFirstName);
            command.Parameters.AddWithValue("@Student_LastName", student.StudentLastName);
            command.Parameters.AddWithValue("@Student_Faculty", student.StudentFaculty);

            int affectedRows = command.ExecuteNonQuery();
            return affectedRows != 0;
        }
    }
}
