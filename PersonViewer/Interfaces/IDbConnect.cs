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
    public interface IDbConnect
    {
        IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString);
    }
}
