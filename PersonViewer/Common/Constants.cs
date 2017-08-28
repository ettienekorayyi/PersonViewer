using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;

namespace PersonViewer.Common
{
    public class Constants
    {
        public const string SqlServerClient = "System.Data.SqlClient";
        public const string SqlServerConnection = "SqlServer";

        public const string MySqlClient = "MySqlClient";
        public const string MySqlConnection = "MySql";
        
        public const string OleDbClient = "System.Data.OleDb";
        public const string OleDbConnection = "OleDb";
        
        public const string FilePath = @"C:\Users\Stephen\Documents\Visual Studio 2013\" +
                @"Projects\Self Projects\" + @"\PersonViewer\PersonViewer\OleDB\Persons.csv";

    }
}
