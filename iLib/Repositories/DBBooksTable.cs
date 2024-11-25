using Microsoft.Data.SqlClient;

namespace iLib.Repositories
{
    public class DBBooksTable
    {



        public bool ReduceBookQuantity(SqlConnection connection, SqlTransaction transaction, string bookIsbn)
        {
            string storedProcedure = "[dbo].[ReduceBookQuantity]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);
            
            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                return false;
            }
            return true;
        }
    }
}
