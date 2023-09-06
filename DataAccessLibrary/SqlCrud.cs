using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private string connection;
        SqlDataAccess db = new SqlDataAccess();

        public SqlCrud(string connection)
        {
            this.connection = connection;
        }




    }
}
