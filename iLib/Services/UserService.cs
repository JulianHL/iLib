using iLib.Models;
using iLib.Repositories;
using Microsoft.Data.SqlClient;
using System.Numerics;
using System.Text.RegularExpressions;

namespace iLib.Services
{
    public class UserService : BaseService
    {
        protected DBUserTable _dB;
        public UserService()
        {
            _dB = new DBUserTable();
        }
        public User validateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password are required.");
            }

            using SqlConnection? connection = EstablishConnection();
            if (connection == null)
            {
                throw new Exception("The connection was not established, connection is null");
            }
            connection.Open();
            User? user = _dB.validateUser(connection, username, password);

            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }

            return user;
        }

        public bool EmailFormatValidation(string email)
        {
            string emailFormat = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailFormat);
        }

        public bool PhonNumberAreaCodeValidation(int areaCode)
        {
            Dictionary<int, bool> codigosValidos = new Dictionary<int, bool>
            {
                { 204, true }, { 226, true }, { 236, true }, { 249, true }, { 289, true },
                { 306, true }, { 343, true }, { 365, true }, { 387, true }, { 403, true },
                { 416, true }, { 418, true }, { 431, true }, { 437, true }, { 438, true },
                { 450, true }, { 506, true }, { 514, true }, { 519, true }, { 548, true },
                { 579, true }, { 581, true }, { 587, true }, { 604, true }, { 613, true },
                { 639, true }, { 647, true }, { 672, true }, { 705, true }, { 709, true },
                { 742, true }, { 778, true }, { 782, true }, { 807, true }, { 819, true },
                { 825, true }, { 867, true }, { 873, true }, { 902, true }, { 905, true }
            };

            return codigosValidos.ContainsKey(areaCode);
        }

        public bool PhonNumberValidation(long phoneNumber)
        {
            if (phoneNumber>9059999999 || phoneNumber<2040000000)
            {
                return false;
            }

            int areaCode = (int)(phoneNumber / 10000000);
            if (!PhonNumberAreaCodeValidation(areaCode))
            {
                return false;
            }

            return true;
        }

        public void NullableFieldsValidation(string? email, long phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(email) && !EmailFormatValidation(email))
            {
                throw new InvalidOperationException("The email is invalid");
            }

            if ((phoneNumber != 0 && !PhonNumberValidation(phoneNumber)))
            {
                throw new InvalidOperationException("The phone number is invalid");
            }
        }
    }
}
