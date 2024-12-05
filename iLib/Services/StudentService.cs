using AspNetCoreGeneratedDocument;
using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace iLib.Services
{
    public class StudentService : UserService
    {
        public StudentService()
        {
            _dB = new DBStudentsTable();
        }

        public string AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student is Null");
            }

            NullableFieldsValidation(student.UserEmail, student.UserPhoneNumber);

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (!_dB.AddUser(connection, transaction, student))
                {
                    throw new Exception("There was an internal error, the user was not added");
                }

                if (!((DBStudentsTable)_dB).AddStudent(connection, transaction, student))
                {
                    throw new Exception("There was an internal error, the student was not added");
                }

                transaction.Commit();
                return "Student added successfully";
            }
            catch(SqlException ex) 
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
