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
        // Connection String
        public const string SqlServer = "SqlServer";
        public const string MySql = "MySql";
        
        // Text File: CSV
        public const string FilePath = @"C:\Users\Stephen\Documents\Visual Studio 2013\Projects\Self Projects\" +
                @"\PersonViewer\PersonViewer\OleDB\Persons.csv";

        // Data Provider
        public const string SqlServerClient = "System.Data.SqlClient";
        public const string MySqlClient = "MySqlClient";

        // Registry path               
        public const string SqlServerRegistry = @"SOFTWARE\Microsoft\Microsoft SQL Server\120\Tools\ClientSetup";
        
    }
}
