﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PersonViewer.Model;

using System.Configuration;
using System.Data;
using Microsoft.Win32;
using PersonViewer.FactoryPattern;
using System.Data.Common;
using PersonViewer.Common;
using System.Data.OleDb;

using System.IO;
using PersonViewer.Databases;
using PersonViewer.Interfaces;

namespace PersonViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// reference: 
    /// https://www.codeproject.com/Articles/55890/Don-t-hard-code-your-DataProviders
    /// https://msdn.microsoft.com/en-us/library/ms971499.aspx
    /// https://blogs.msmvps.com/deborahk/dal-using-a-data-provider-factory/
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lvUsers_Loaded(object sender, RoutedEventArgs e)
        {
            DbPickerFactory pickerFactory = new DbPickerFactory();
            IDbConnection connection = null;

            // To check the csv, just make the if condition == to null
            if (Registry.LocalMachine.OpenSubKey(Constants.SqlServerRegistry, false) != null)
            {
                connection = pickerFactory.CreateDbClasses(Constants.SqlServerClient).ConnectToDatabase(
                    ConfigurationManager.ConnectionStrings[Constants.SqlServer]);
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[Constants.SqlServer].ConnectionString;
                this.ViewData(connection);
            }
            else
            {
                this.GetTextFileFormatCsv();
            }

            
            
        }

        public void GetTextFileFormatCsv()
        {   
            FileInfo file = new FileInfo(Constants.FilePath);
            
            //working
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                file.DirectoryName + "\"; Extended Properties='text;HDR=Yes;FMT=Delimited(,)';"))
            using (OleDbCommand cmd = new OleDbCommand(string.Format
                                  ("SELECT * FROM [{0}]", file.Name), connection))
            {
                connection.Open();
                using(OleDbDataReader reader = cmd.ExecuteReader())
                {
                    List<Person> list = new List<Person>();
                    while(reader.Read())
                        list.Add(new Person
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString()
                        });
                    lvUsers.ItemsSource = list;
                }
            }
        }
        public void ViewData(IDbConnection database)
        {
            try
            {
                using (IDbCommand command = database.CreateCommand())
                {
                    database.Open();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM dbo.Person";

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Person> list = new List<Person>();

                        while (reader.Read())
                            list.Add(new Person { Id = int.Parse(reader[0].ToString()), Name = reader[1].ToString() });
                        lvUsers.ItemsSource = list;
                    }
                }
            }
            catch (NullReferenceException nullException)
            {
                MessageBox.Show(nullException.Message);
            }
        }

    }
}
