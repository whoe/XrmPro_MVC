using Microsoft.Data.Sqlite;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace XrmPro_MVC.Models
{
    public class StudentModel
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }


        [Required]
        [StringLength(20, ErrorMessage = "Max Length 20")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "only letters and digits")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max Length 50")]
        [RegularExpression(@"\w+@\w+\.\w{2,3}", ErrorMessage = "example@xrmpro.ru")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Max Length 50")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "only letters and digits")]
        [Required]
        public string Git { get; set; }

        private static readonly string filenameDatabase = "students.db";
        private static readonly string connectionString = $"Data Source={filenameDatabase};";
        
        public StudentModel()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = @"CREATE TABLE IF NOT EXISTS Students (
                                                id INTEGER NOT NULL PRIMARY KEY,
                                                name NVARCHAR(20),
                                                email NVARCHAR(50),
                                                git NVARCHAR(50));";
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        
        public bool Create()
        {
            bool result;
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    string sql = $"INSERT INTO Students (id, name, email, git) " +
                        $"VALUES ({Id}, '{Name}', '{Email}', '{Git}')";
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        connection.Open();
                        result = command.ExecuteNonQuery() == 0 ? false : true;
                        connection.Close();
                    }
                }
            }
            catch (SqliteException e)
            {
                if (e.SqliteErrorCode == 19)
                    result = false;
                else
                    throw;
            }

            return result;
        }
        
        public StudentModel Read(int id)
        {
            var hasStudent = true;
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = $"SELECT * FROM Students WHERE id = {id}";
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            this.Id = reader.GetInt32(0);
                            this.Name = reader.GetString(1);
                            this.Email = reader.GetString(2);
                            this.Git = reader.GetString(3);
                        }
                        else
                            hasStudent = false;
                    }
                    connection.Close();
                }
            }
            return hasStudent ? this : null;
        }

        public bool Update()
        {
            bool result;
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = "UPDATE Students SET " +
                    $"name = '{Name}', " +
                    $"email = '{Email}', " +
                    $"git = '{Git}' " +
                    $"WHERE id = '{Id}'";
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    connection.Open();
                    result = command.ExecuteNonQuery() == 0 ? false : true;
                    connection.Close();
                }
            }
            return result;
        }

        public static bool Delete(int id)
        {
            bool result;
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = $"DELETE FROM Students WHERE id = {id}";
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    connection.Open();
                    result = command.ExecuteNonQuery() == 0 ? false : true;
                    connection.Close();
                }
            }

            return result;
        }

        public static IEnumerable<StudentModel> ReadAll()
        {
            var result = new List<StudentModel>();
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = $"SELECT * FROM Students";
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new StudentModel();
                            student.Id = reader.GetInt32(0);
                            student.Name = reader.GetString(1);
                            student.Email = reader.GetString(2);
                            student.Git = reader.GetString(3);
                            result.Add(student);
                        }
                    }
                    connection.Close();
                }
            }

            return result;
        }
    }
}
