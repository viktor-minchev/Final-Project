using System;
using System.Collections.Generic;
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
    /// Interaction logic for CreateSnippet.xaml
    /// </summary>
    public partial class CreateSnippet : Window
    {
        public string username;
        public CreateSnippet(string username)
        {
            this.username = username;
            InitializeComponent();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True");

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Open();

                //Build our actual query 

                string query = "INSERT INTO snippets(username,title,text,snippetid)values ('" + username + "','" + this.txtTitle.Text + "','" + this.txtText.Text + "','" + this.txtSnippetID.Text+ "') ";

                //Establish a sql command

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully created");

                Snippets sp = new Snippets(username);
                sp.Show();
                this.Close();

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }
        }
    }
}
