using PersonViewer.Interfaces;
using PersonViewer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Data;
using System.Data.SqlClient;

namespace PersonViewer.Databases
{
    public class SqlServerDatabase : IDbCustomConnector
    {
        public IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            return providerFactory.CreateConnection();
        }

        public List<Person> ExecuteQuery(IDbConnection database)
        {
            try
            {
                List<Person> list = new List<Person>();
                using (SqlCommand command = (database.CreateCommand() as SqlCommand))
                {
                    database.Open();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Person";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(new Person { Id = int.Parse(reader[0].ToString()), Name = reader[1].ToString() });
                    }
                }
                return list;
            }
            catch (NullReferenceException nullException)
            {
                MessageBox.Show(nullException.Message);
            }
            return null;
        }

    }
}
