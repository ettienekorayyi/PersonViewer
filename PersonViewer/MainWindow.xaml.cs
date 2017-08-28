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

using System.Data;
using Microsoft.Win32;
using PersonViewer.FactoryPattern;
using System.Data.Common;
using PersonViewer.Common;

using PersonViewer.Databases;
using PersonViewer.Interfaces;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using PersonViewer.TypeTextFile;

namespace PersonViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lvUsers_Loaded(object sender, RoutedEventArgs e)
        {   
            lvUsers.ItemsSource = new Utility().UseDataSource(Constants.SqlServerClient,Constants.SqlServerConnection,new SqlServerDatabase());
        }


        private void rdBtnCsv_Click(object sender, RoutedEventArgs e)
        {
            //string filePath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + @"C:\Users\Stephen\Documents\Visual Studio 2013\Projects\Self Projects\PersonViewer\PersonViewer\bin\Debug\" + "\"; Extended Properties='text;HDR=Yes;FMT=Delimited(,)';";

            lvUsers.ItemsSource = new Utility().UseDataSource(Constants.OleDbClient,Constants.OleDbConnection,new CsvFileFormat());//new TypeTextFile.CsvFileFormat().GetTextFileFormatCsv();
        }

        private void rdBtnMySql_Click(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = new Utility().UseDataSource(Constants.MySqlClient,Constants.MySqlConnection,new MySqlDatabase());
        }
        
        private void rdBtnSqlServer_Click(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = new Utility().UseDataSource(Constants.SqlServerClient, Constants.SqlServerConnection, new SqlServerDatabase());
        }
    }
}
