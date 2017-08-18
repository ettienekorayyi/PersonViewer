using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using PersonViewer.Common;


namespace PersonViewer.FactoryPattern
{
    public class DbPickerFactory
    {
        public DbProviderFactory DbmsSelector(ConnectionStringSettings identifier)
        { 
            switch(identifier.ProviderName)
            {
                case Constants.SqlClient :
                    return DbProviderFactories.GetFactory(identifier.ProviderName);
                case Constants.MySql :
                    return DbProviderFactories.GetFactory(identifier.ProviderName);
                case Constants.OleDb:
                    return DbProviderFactories.GetFactory(identifier.ProviderName);
            }
            return null;
        }
    }
}
