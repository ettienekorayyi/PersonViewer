using PersonViewer.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Windows;

using PersonViewer.FactoryPattern;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using PersonViewer.Databases;
using PersonViewer.Interfaces;

namespace PersonViewer.Common
{
    public class Utility
    {
        public DbPickerFactory pickerFactory { get; private set; }
        public IDbConnection connection { get; private set; }

        public List<Person> UseDataSource(string dbType,string connectionString, IDbCustomConnector client)
        {
            pickerFactory = new DbPickerFactory();
            
            connection = pickerFactory.CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            return client.ExecuteQuery(connection);
        }

    }
}
