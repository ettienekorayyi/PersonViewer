using PersonViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonViewer.Databases
{
    public class SqlServerDatabase : IDbConnect
    {
        public IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            return providerFactory.CreateConnection();
        }
    }
}
