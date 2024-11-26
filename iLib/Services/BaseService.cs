using Microsoft.Data.SqlClient;

namespace iLib.Services
{
    public class BaseService
    {

        protected SqlConnection? EstablishConnection()
        {
            try
            {
                string ConnectionString = "Data Source=MSI;Initial Catalog=iLib;Integrated Security=True;TrustServerCertificate=True";
                return new SqlConnection(ConnectionString);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
