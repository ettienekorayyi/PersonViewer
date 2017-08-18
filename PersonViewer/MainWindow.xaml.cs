using System;
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
            RegistryKey _regSql = Registry.LocalMachine.OpenSubKey(Common.Constants.SqlServerRegistry, false);
            //RegistryKey _regCsv = Registry.LocalMachine.OpenSubKey(Common.Constants.CsvRegistry, false);
            ConnectionStringSettings connectionStringSettings = null;
            DbProviderFactory providerFactory = null;
            
            if (_regSql == null)
            {
                connectionStringSettings = ConfigurationManager.ConnectionStrings[Constants.SqlServer];
                providerFactory = new DbPickerFactory().DbmsSelector(connectionStringSettings);
            }
            else if (Constants.FilePath != null)
            {
                
                connectionStringSettings = ConfigurationManager.ConnectionStrings[Constants.CsvClient];
                providerFactory = new DbPickerFactory().DbmsSelector(connectionStringSettings);
            }

            //this.Get();
            this.ViewData(connectionStringSettings,providerFactory);
            //
            
        }

        public void Get()
        {
            string fileName = @"C:\Users\Stephen\Documents\Visual Studio 2013\Projects\Self Projects\" +
                @"\PersonViewer\PersonViewer\OleDB\Persons.csv";
            
            FileInfo file = new FileInfo(fileName);
            //string connectionString =
            //    new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " +
            //    System.IO.Path.GetDirectoryName(fileName) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"").ToString();
            
            //working
            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                file.DirectoryName + "\"; Extended Properties='text;HDR=Yes;FMT=Delimited(,)';"))
            
            using (OleDbCommand cmd = new OleDbCommand(string.Format
                                  ("SELECT * FROM [{0}]", file.Name), con))
            {
                con.Open();
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
        private void ViewData(ConnectionStringSettings connectionStringSettings, DbProviderFactory providerFactory)
        {
            try
            {
                using (IDbConnection database = providerFactory.CreateConnection())
                using (IDbCommand command = database.CreateCommand())
                {
                    database.ConnectionString = connectionStringSettings.ToString();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM dbo.Person";

                    database.Open();
                    
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
