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
    /// Interaction logic for Snippets.xaml
    /// </summary>
    public partial class Snippets : Window
    {
        public string username;
        public Snippets(string username)
        {
            this.username = username;
            InitializeComponent();
            SyncDataGrid();
        }
        String dbConnection = @"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True";

        private void SyncDataGrid()
        {

            SqlConnection sqlCon = new SqlConnection(dbConnection);

            using (sqlCon)
            {
                sqlCon.Open();
                String query = "select * from snippets where username ='" +username+"'";
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlCon))
                {
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    snippetsDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            sqlCon.Close();
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

        private void OpenButton(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String snippetid = dataRowView[0].ToString();
                String title = dataRowView[1].ToString();
                String description = dataRowView[2].ToString();
                EditSnippet edit= new EditSnippet(username, snippetid, title, description);
                edit.Show();
                this.Close();
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
                String snippetid = dataRowView[0].ToString();
                SqlConnection sqlCon = new SqlConnection(dbConnection);
                sqlCon.Open();
                String query = "DELETE FROM snippets where snippetid ='" + snippetid+"'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Snippets snippets = new Snippets(username);
            snippets.Show();
            this.Close();
        }


        private void CreateSnippetButton(object sender, RoutedEventArgs e)
        {
            CreateSnippet cp = new CreateSnippet(username);
            cp.Show();
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
