using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class DataAccess
    {
        public string Server { get; set; } = "**********";
        public string DatabaseName { get; set; } = "****************";
        public string UserName { get; set; } = "****************";
        public string Password { get; set; } = "***************";
        public string MySqlConnectionString { get; set; }

        public DataAccess()
        {
            MySqlConnectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", Server, DatabaseName, UserName, Password);
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(MySqlConnectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);

                return rows.ToList();
            }
        }

        public async Task<string> LoadData_Simple(string sql)
        {
            using (IDbConnection connection = new MySqlConnection(MySqlConnectionString))
            {
                var row = await connection.QueryAsync<string>(sql);
                return row.First();
            }
        }

        public void SaveData<T>(string sql, T parameters)
        {
            using (IDbConnection connection = new MySqlConnection(MySqlConnectionString))
            {
                connection.Execute(sql, parameters);
            }
        }
    }
}
