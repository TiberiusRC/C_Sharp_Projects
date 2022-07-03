using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginApp_WPF
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
               using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\TR_REPO\C#_Projects_GIT\C_Sharp_Projects\LoginApp_WPF\Database1.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM Login WHERE Username = '" +textUserName.Text.Trim() + "' AND Password = '" + textPassword.Text.Trim() + "'";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query,connection);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);
                    if(table.Rows.Count == 1)
                    {
                        Form1 dashboard = new Form1();
                        this.Hide();
                        dashboard.Show();
                    }
                }
            }
        }
        private bool IsValid()
        {
            if(textUserName.Text.TrimStart() == string.Empty | textUserName.Text.Length < 5)
            {
                MessageBox.Show("User name must be 5 or more letters long !");
                    return false;
            }
            if(textPassword.Text.TrimStart() == string.Empty | textPassword.Text.Length < 5)
            {
                MessageBox.Show("Password must be 5 or more letters long ! ");
                return false;               

            }
            return true;

        }
    }
}
