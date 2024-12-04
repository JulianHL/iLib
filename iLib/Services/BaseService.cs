using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class BaseService
    {

        protected SqlConnection? EstablishConnection()
        {
            try
            {
                string ConnectionString = "Data Source=SHIVANG;Initial Catalog=iLib;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                return new SqlConnection(ConnectionString);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
