using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class LibrarianService : UserService
    {

        public string AddLibrarian(Librarian librarian)
        {

            if (librarian == null)
            {
                throw new ArgumentNullException("student is Null");
            }

            NullableFieldsValidation(librarian.UserEmail, librarian.UserPhoneNumber);

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }

            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                if (!_dB.AddUser(connection, transaction, librarian))
                {
                    throw new Exception("There was an internal error, the user was not added");
                }

                if (!((DBLibrariansTable)_dB).AddLibrarian(connection, transaction, librarian))
                {
                    throw new Exception("There was an internal error, the librarian was not added");
                }

                transaction.Commit();
                return "Student added successfully";
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
