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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for bookmarks.xaml
    /// </summary>
    public partial class bookmarks : Window
    {
        public string username;
        public bookmarks(string username)
        {
            this.username = username;
            InitializeComponent();
        }
        String dbConnection = @"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

                SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True");

                using (sqlCon)
                {
                    sqlCon.Open();
                    String query = "select bookmarkid, sqlid, username from sqlbookmarks where username ='" + username + "'";
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlCon))
                    {
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        sqlbookmarksDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
                sqlCon.Close();
            }

        private string GetDescription(string sqlid)
        {

            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True");

            using (sqlCon)
            {
                sqlCon.Open();
                String query = "select text from documentation where sqlid ='" + sqlid+ "'";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                String text = (String) cmd.ExecuteScalar();
                sqlCon.Close();

                return text.ToString();
            }
        }
        private string GetTitle(string sqlid)
        {

            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True");

            using (sqlCon)
            {
                sqlCon.Open();
                String query = "select title from documentation where sqlid ='" + sqlid + "'";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                String text = (String)cmd.ExecuteScalar();
                sqlCon.Close();

                return text.ToString();
            }
        }

        private void OpenButton(object sender, RoutedEventArgs e)
        {
                try
                {
                    DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                    String ProductName = dataRowView[1].ToString();
                String title = GetTitle(ProductName);
                    String description = GetDescription(ProductName);
                    MessageBox.Show(title + "\r\n" + description);
                    //This is the code which will show the button click row data. Thank you.
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }

        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String bookmarkid = dataRowView[0].ToString();
                SqlConnection sqlCon = new SqlConnection(dbConnection);
                sqlCon.Open();
                String query = "DELETE FROM sqlbookmarks  where bookmarkid =" + bookmarkid;
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            bookmarks bookmarks = new bookmarks(username);
            bookmarks.Show();
            this.Close();
        }

        private void ReturnButton(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(username);
            mainWindow.Show();
            this.Close();
        }
    }
}