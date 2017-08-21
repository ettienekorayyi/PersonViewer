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
//using System.Data.OleDb;

//using System.IO;
using PersonViewer.Databases;
using PersonViewer.Interfaces;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace PersonViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// reference: 
    /// https://www.codeproject.com/Articles/55890/Don-t-hard-code-your-DataProviders
    /// https://msdn.microsoft.com/en-us/library/ms971499.aspx
    /// https://blogs.msmvps.com/deborahk/dal-using-a-data-provider-factory/
    /// Xampp/MySql:
    /// https://praveenpuglia.com/make-wamp-work-on-windows-10-technical-preview/
    /// https://stackoverflow.com/questions/13607334/when-storing-a-mysql-connection-string-in-app-config-what-value-should-the-prov
    /// https://www.codeproject.com/Tips/423233/How-to-Connect-to-MySQL-Using-Csharp
    /// https://www.digitalocean.com/community/tutorials/how-to-create-a-new-user-and-grant-permissions-in-mysql
    /// https://stackoverflow.com/questions/39442151/authentication-to-host-localhost-for-user-root-using-method-mysql-native-pa
    /// http://www.complete-concrete-concise.com/web-tools/adding-a-new-user-to-a-mysql-database-in-xampp
    /// </summary>
    public partial class MainWindow : Window
    {
        public DbPickerFactory pickerFactory { get; set; }
        public IDbConnection connection { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lvUsers_Loaded(object sender, RoutedEventArgs e)
        {
            pickerFactory = new DbPickerFactory();
            
            if (rdBtnSqlServer.IsChecked == true)
                this.QuerySqlServer();
        }


        private void rdBtnCsv_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            lvUsers.ItemsSource = new TypeTextFile.CsvFileFormat().GetTextFileFormatCsv();
            view.Refresh();
        }

        private void rdBtnMySql_Click(object sender, RoutedEventArgs e)
        {
            pickerFactory = new DbPickerFactory();

            if (rdBtnMySql.IsChecked == true)
            {
                connection = pickerFactory.CreateDbClasses(Constants.MySqlClient).ConnectToDatabase(
                    ConfigurationManager.ConnectionStrings[Constants.MySql]);
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[Constants.MySql].ConnectionString;
                lvUsers.ItemsSource = new Utility().ExecuteQuery(connection);
            }
        }

        private void rdBtnSqlServer_Click(object sender, RoutedEventArgs e)
        {
            if (rdBtnSqlServer.IsChecked == true)
                this.QuerySqlServer();
        }

        private void QuerySqlServer()
        {
            connection = pickerFactory.CreateDbClasses(Constants.SqlServerClient).ConnectToDatabase(
                    ConfigurationManager.ConnectionStrings[Constants.SqlServer]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[Constants.SqlServer].ConnectionString;
            lvUsers.ItemsSource = new Utility().ExecuteQuery(connection);
        }
    }
}
