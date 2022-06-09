using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel
{
    public partial class LoginForma : Form
    {
        DBconnect connect = new DBconnect();
        public LoginForma()
        {
            InitializeComponent();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Orange;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LoginForm_Load(Object sender, EventArgs e)
        { }

        private void Button_login_Click(object sender, EventArgs e)
        {
            if (TextBox_username.Text.Trim().Equals("") || TextBox_password.Text == "")
            {
                MessageBox.Show("Unesite vaše korisničko ime i šifru", "Nedostaju podaci", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                string selectquery = "SELECT * FROM `users` WHERE `username` = @usn AND `password` = @pass;";
                MySqlCommand command = new MySqlCommand(selectquery, connect.GetConnection());
                command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = TextBox_username.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = TextBox_password.Text;
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Ovo korisničko ime i šifra ne postoje", "Pogrešni podaci", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
