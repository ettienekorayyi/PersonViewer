using PersonViewer.Common;
using PersonViewer.Interfaces;
using PersonViewer.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Configuration;
using System.Data.Common;

namespace PersonViewer.TypeTextFile
{
    public class CsvFileFormat : IDbCustomConnector
    {

        public List<Person> ExecuteQuery(IDbConnection database)
        {
            return this.ExecuteQuery();
        }

        public List<Person> ExecuteQuery()
        {
            FileInfo file = new FileInfo(Constants.FilePath);

            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                file.DirectoryName + "\"; Extended Properties='text;HDR=Yes;FMT=Delimited(,)';"))
            using (OleDbCommand cmd = new OleDbCommand(string.Format
                                  ("SELECT * FROM [{0}]", file.Name), connection))
            {
                List<Person> list = new List<Person>();
                connection.Open();
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                        list.Add(new Person
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString()
                        });

                }
                return list;
            }
        }

        public IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            return providerFactory.CreateConnection(); ;
        }
    }
}
