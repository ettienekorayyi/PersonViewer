﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using PersonViewer.Common;
using PersonViewer.Interfaces;
using PersonViewer.Databases;
using PersonViewer.TypeTextFile;


namespace PersonViewer.FactoryPattern
{
    public class DbPickerFactory
    {
        public IDbCustomConnector CreateDbClasses(string dbClasses)
        {
            switch (dbClasses)
            {
                case Constants.SqlServerClient:
                    return new SqlServerDatabase();
                case Constants.MySqlClient:
                    return new MySqlDatabase();
                case Constants.OleDbClient:
                    return new CsvFileFormat();
            }
            return null;
        }
    }
}
