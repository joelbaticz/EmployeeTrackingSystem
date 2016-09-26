using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ETS.data
{
    public sealed class ConnectionHelper
    {
        //fields
        private const string CONNECTION_STRING = "Data Source=.;Initial Catalog=ETSJoel;User ID=sa;Password=Petersham";

        //methods
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = CONNECTION_STRING;
            connection.Open();
            return connection;
        }
    }
}
