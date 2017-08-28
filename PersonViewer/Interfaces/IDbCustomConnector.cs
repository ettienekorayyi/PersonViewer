using PersonViewer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonViewer.Interfaces
{
    public interface IDbCustomConnector
    {
        IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString);

        List<Person> ExecuteQuery(IDbConnection database);
    }
}
