﻿using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace DataLibrary
{
    public class DataAccess
    {
        #region FIELDS / PROPERTIES
        private readonly string server = "2.57.91.5";
        private readonly string database_name = "u826675553_Pokellection";
        private readonly string user_name = "u826675553_Josh";
        private readonly string password = "MyLeftShoe22!";
        private readonly string MySqlConnectionString;
        #endregion




        #region CONSTRUCTORS
        public DataAccess()
        {
            MySqlConnectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database_name, user_name, password);
        }
        #endregion




        #region METHODS
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
        #endregion
    }
}
