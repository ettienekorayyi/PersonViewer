using PersonViewer.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Windows;

namespace PersonViewer.Common
{
    public class Utility
    {
        public List<Person> ExecuteQuery(IDbConnection database)
        {
            try
            {
                List<Person> list = new List<Person>();
                using (IDbCommand command = database.CreateCommand())
                {
                    database.Open();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Person";

                    using (IDataReader reader = command.ExecuteReader())
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
