using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;

namespace Latian_LKS_Laundry
{
    public partial class LoginForm : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        SqlCommand command;
        SqlDataReader reader;
        public Point mouseMove;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            label5.Hide();
            this.CenterToScreen();
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            textBox1.Focus();
            textBox2.ForeColor = Color.Black;
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseMove = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(-mouseMove.X, -mouseMove.Y);
                Location = mousePose;
            }
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        bool validate()
        {
            if(textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("All Field Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(IsValidEmail(textBox1.Text) == false)
            {
                MessageBox.Show("Email Doesn't Valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                string pass = Encrypt.Pass(textBox2.Text);
                command = new SqlCommand("select * from employee where email_employee = '" + textBox1.Text + "' and password_employee = '" + pass + "'", connection);
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Model.id = Convert.ToInt32(reader["id_employee"]);
                    Model.Name = Convert.ToString(reader["name_employee"]);
                    this.Hide();
                    MainForm main = new MainForm();
                    main.Show();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Please Try Again, Your Data is not Valid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Close();
            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label5.Show();
            textBox2.UseSystemPasswordChar = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            label5.Hide();
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
