using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string username;
        public MainWindow(string username)
        {
            this.username = username;
            InitializeComponent();
        }
        String dbConnection = @"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            WpfApp1.final_projectDataSet final_projectDataSet = ((WpfApp1.final_projectDataSet)(this.FindResource("final_projectDataSet")));
            // Load data into the table documentation. You can modify this code as needed.
            WpfApp1.final_projectDataSetTableAdapters.documentationTableAdapter final_projectDataSetdocumentationTableAdapter = new WpfApp1.final_projectDataSetTableAdapters.documentationTableAdapter();
            final_projectDataSetdocumentationTableAdapter.Fill(final_projectDataSet.documentation);
            System.Windows.Data.CollectionViewSource documentationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("documentationViewSource")));
            documentationViewSource.View.MoveCurrentToFirst();
        }

        private void DetailsButton(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ProductName = dataRowView[1].ToString();
                String ProductDescription = dataRowView[2].ToString();
                MessageBox.Show(ProductName + "\r\n" + ProductDescription);
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BookmarkButton(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String sqlid = dataRowView[0].ToString();
                SqlConnection sqlCon = new SqlConnection(dbConnection);
                sqlCon.Open();
                String query = "insert into sqlbookmarks (sqlid, username) values ('" + sqlid + "', '" + username + "')";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
                MessageBox.Show("Successfully bookmarked");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Bookmarks_Click(object sender, RoutedEventArgs e)
        {
            bookmarks bookmarks = new bookmarks (username);
            bookmarks.Show();
            this.Close();
        }

        private void Snippets_Click(object sender, RoutedEventArgs e)
        {
            Snippets snippets = new Snippets(username);
            snippets.Show();
            this.Close();
        }
    }
}
