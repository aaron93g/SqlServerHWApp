using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDataAccess
    {

        public List<T> LoadData<T,U>(string sqlStatement, U parameter, string connectionString)
        {

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, connection).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameter, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameter);
            }
        }

    }
}
