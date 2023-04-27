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
    /// Interaction logic for EditSnippet.xaml
    /// </summary>
    public partial class EditSnippet : Window
    {
        public string username;
        public string snippetid;
        public EditSnippet(string username, string snippetid, string title, string text)
        {
            InitializeComponent();
    
            txtText.Text = text;
            txtTitle.Text = title;

            this.username = username;
            this.snippetid = snippetid;
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC01\LOCALHOST; Initial Catalog=final_project; Integrated Security=True");

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Open();

                //Build our actual query 

                string query = "UPDATE snippets SET title = '" + txtTitle.Text + "', text='" + txtText.Text + "' WHERE snippetid = '" + snippetid + "'";
                //Establish a sql command

                SqlCommand cmd = new SqlCommand(query, sqlCon);

                cmd.ExecuteNonQuery();
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
