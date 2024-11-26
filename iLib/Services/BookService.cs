using iLib.Repositories;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace iLib.Services
{
    public class BookService : BaseService
    {
        protected DBBooksTable _dB;

        public BookService()
        {
            _dB = new DBBooksTable();
        }
    }
}
