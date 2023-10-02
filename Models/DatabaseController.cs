using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGPokellection.Models
{
    class DatabaseController
    {
        public string Server { get; set; } = "2.57.91.5";
        public string DatabaseName { get; set; } = "u826675553_Pokellection";
        public string UserName { get; set; } = "u826675553_Josh";
        public string Password { get; set; } = "MyLeftShoe22!";
        private MySqlConnection Connection { get; set; }




        private static DatabaseController _instance = null;
        public static DatabaseController Instance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseController();
            }
            return _instance;
        }

        public DatabaseController()
        {
            CreateConnection();
        }




        public void CreateConnection()
        {
            string connstring = string.Format("Server={0};User ID={1};Password={2};Database={3}", Server, UserName, Password, DatabaseName);
            Connection = new MySqlConnection(connstring);
        }




        public async Task<string> GetTcgApiData()
        {
            string result = "";

            using var command = new MySqlCommand("SELECT * FROM users");
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result = reader.GetString(0);
            }

            return result;
        }
    }
}
