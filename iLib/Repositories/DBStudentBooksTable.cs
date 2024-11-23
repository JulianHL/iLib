﻿
using iLib.Models;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace iLib.Repositories
{
    public class DBStudentBooksTable : DBBooksTable
    {
        public List<StudentBook>? GetAllStudentBooksByStudentId(SqlConnection connection, int userId)
        {
            List<StudentBook> StudentBooks;
            string storedProcedure = "[dbo].[GetStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);

            using SqlDataReader reader = command.ExecuteReader();
            StudentBooks = new List<StudentBook>();
            while (reader.Read())
            {
                StudentBooks.Add(new StudentBook
                {
                    BookIsbn = reader.GetString(0),
                    BookTitle = reader.GetString(1),
                    BookAuthor = reader.GetString(2),
                    BookQuantity = reader.GetInt32(3),
                    BookPublisher = reader.GetString(4),
                    BookGenre = reader.GetString(5),
                    BookLanguage = reader.GetString(6),
                    BookFormat = reader.GetString(7),
                    BookDescription = reader.IsDBNull(8) ? null : reader.GetString(8),
                    BookEdition = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                    BookPages = reader.IsDBNull(10) ? null : reader.GetInt32(10),
                    BookPublicationDate = reader.IsDBNull(11) ? null : DateOnly.FromDateTime(reader.GetDateTime(11)),
                    BookStartingDate = DateOnly.FromDateTime(reader.GetDateTime(12)),
                    BookDueDate = DateOnly.FromDateTime(reader.GetDateTime(13))

                });
            }
            return StudentBooks;

        }

        public bool AddStudentBooks(SqlConnection connection, SqlTransaction transaction, int userId, string bookIsbn, DateOnly startingDate, DateOnly dueDate)
        {
            string storedProcedure = "[dbo].[AddStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);
            command.Parameters.AddWithValue("@Starting_Date", startingDate);
            command.Parameters.AddWithValue("@Due_Date", dueDate);

            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                return false;
            }
            return true;


        }

        public bool ConflictStudentBooks(SqlConnection connection, SqlTransaction transaction, int userId, string bookIsbn)
        {
            string storedProcedure = "[dbo].[ConflictStudentBooks]";

            using SqlCommand command = new SqlCommand(storedProcedure, connection, transaction);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_Id", userId);
            command.Parameters.AddWithValue("@Book_Isbn", bookIsbn);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            return false;

        }
        
    }
}
