using Microsoft.Data.Sqlite;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace XrmPro_MVC.Models
{

    public class StudentModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int id;

        [Required]
        [RegularExpression("[^'\"]+")]
        [MaxLength(20)]
        public string name;

        [Required]
        [RegularExpression(@"\w+@\w+.\w{0,3}")]
        [MaxLength(50)]
        public string email;

        [Required]
        [MaxLength(50)]
        [RegularExpression("[^'\"]+")]
        public string git;

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
                        $"VALUES ({id}, '{name}', '{email}', '{git}')";
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
                            this.id = reader.GetInt32(0);
                            this.name = reader.GetString(1);
                            this.email = reader.GetString(2);
                            this.git = reader.GetString(3);
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
                    $"name = '{name}', " +
                    $"email = '{email}', " +
                    $"git = '{git}' " +
                    $"WHERE id = '{id}'";
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

        public bool Delete(int id)
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
    }
}
