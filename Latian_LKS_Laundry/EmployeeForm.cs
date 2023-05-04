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
using System.Text.RegularExpressions;

namespace Latian_LKS_Laundry
{
    public partial class EmployeeForm : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        SqlCommand cmd;
        SqlDataReader reader;
        public Point mouseMove;
        public EmployeeForm()
        {
            InitializeComponent();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            EmployeeForm employeeForm = new EmployeeForm();
            employeeForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            ServiceForm serviceForm = new ServiceForm();
            serviceForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            PackageForm packageForm = new PackageForm();
            packageForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            InputServiceTransactionForm inputServiceTransactionForm = new InputServiceTransactionForm();
            inputServiceTransactionForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            InputPackageTransactionForm inputPackageTransactionForm = new InputPackageTransactionForm();
            inputPackageTransactionForm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            ViewTransactionForm viewTransactionForm = new ViewTransactionForm();
            viewTransactionForm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerForm customerForm = new CustomerForm();
            customerForm.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            label1.Text = dt.ToString();
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseMove = new Point(-e.X, -e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mouseMove.Offset(mousePose.X, mousePose.Y);
                Location = mousePose;
            }
        }
        void DataGrid()
        {
            string query = "select employee.id_job, id_employee as 'Id Employee', name_employee as 'Name', email_employee as 'Email', phone_number_employee as 'Phone Number', addres_employee as 'Addres', date_of_birth_employee as 'Date Of Birth', salary_employee as 'Salary', name_job as 'Job Title' from employee join job on(employee.id_job = job.id_job)";
            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void Search()
        {
            string query = "select id_employee as 'Id Employee', name_employee as 'Name', email_employee as 'Email', phone_number_employee as 'Phone Number', addres_employee as 'Addres', date_of_birth_employee as 'Date Of Birth', salary_employee as 'Salary', name_job as 'Job Title' from employee join job on(employee.id_job = job.id_job) where name_employee like '%" + search.Text + "%' or email_employee like '%" + search.Text + "%' or phone_number_employee like '%" + search.Text + "%'";
            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            comboBox1.Text = "";
            numericUpDown1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        void Hidee()
        {
            panelText.Hide();
            panelSav.Hide();
        }

        void btnShow()
        {
            panelUp.Show();
            SavIn.Show();
            SavUp.Show();
            In.Show();
        }

        void Showw()
        {
            panelText.Show();
            panelSav.Show();
        }

        void Job()
        {
            string query = "select * from job";
            comboBox1.DataSource = Command.getData(query);

            comboBox1.DisplayMember = "name_job";
            comboBox1.ValueMember = "id_job";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button5.BackColor = Color1;
            this.CenterToScreen();
            label2.Text = "Hello, " + Model.Name;
            timer1.Start();
            Hidee();
            textBox4.ForeColor = Color.Black;
            textBox4.UseSystemPasswordChar = true;
            textBox5.ForeColor = Color.Black;
            textBox5.UseSystemPasswordChar = true;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            DataGrid();
            Job();
        }

        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[7].Value);

                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Logout?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Hide();
                LoginForm login = new LoginForm();
                login.Show();
            }
        }
        private void search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void search_Click(object sender, EventArgs e)
        {
            search.Text = "";
        }

        private void search_Leave(object sender, EventArgs e)
        {
            search.Text = "Search..";
            DataGrid();
        }

        private void In_Click(object sender, EventArgs e)
        {
            Showw();
            panelUp.Hide();
            In.Hide();
            SavUp.Hide();
            Clear();
            dataGridView1.Enabled = false;
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
        bool validateIn()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox3.Text == "" || textBox5.Text == "" || richTextBox1.Text == "" || comboBox1.Text == "" || numericUpDown1.Value == 0 || dateTimePicker1.Value == null)
            {
                MessageBox.Show("All Data Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Confirm Password Doesn't same with password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBox4.TextLength <= 8 || textBox5.TextLength <= 8)
            {
                MessageBox.Show("Password Must Be At least 8 character");
                return false;
            }

            var isNumeric = new Regex(@"[0-9]+");
            if (!isNumeric.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Password Must Be At least 1 Numeric");
                return false;
            }

            var isSymbol = new Regex(@"[`~!@#$%^&*()=[{\]}|_+<>,./?;:-]+");
            if (!isSymbol.IsMatch(textBox4.Text))
            {
                MessageBox.Show("Password Must Be At least 1 Symbol");
                return false;
            }

            if(IsValidEmail(textBox2.Text) != true)
            {
                MessageBox.Show("Invalid Email");
                return false;
            }

            if(textBox3.Text[0] != '+')
            {
                MessageBox.Show("Invalid Number Phone");
                return false;
            }

            cmd = new SqlCommand("select * from employee where phone_number_employee='" + textBox4.Text + "'", connection);
            connection.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                MessageBox.Show("Phone Number has Been Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            connection.Close();

            cmd = new SqlCommand("select * from employee where email_employee='" + textBox2.Text + "'", connection);
            connection.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                MessageBox.Show("Email has Been Used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            connection.Close();
            return true;
        }
        private void SavIn_Click(object sender, EventArgs e)
        {
            if (validateIn())
            {
                string pass = Encrypt.Pass(textBox5.Text);
                string insert = "insert into employee values ('" + comboBox1.SelectedValue + "','" + pass + "', '"+ textBox1.Text +"', '" + textBox2.Text + "', '" + richTextBox1.Text + "', '" + textBox3.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + numericUpDown1.Value + "')";
                try
                {
                    Command.NonQuery(insert);

                    MessageBox.Show("Data Success Inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    Hidee();
                    btnShow();
                    DataGrid();
                    dataGridView1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void Up_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                Showw();
                panelUp.Hide();
                In.Hide();
                SavIn.Hide();
            }
            else
            {
                MessageBox.Show("Please Select One Row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool ValidateUp()
        {
            if(textBox4.Text != "")
            {
                if (textBox3.TextLength <= 8 || textBox5.TextLength <= 8)
                {
                    MessageBox.Show("Password Must Be At least 8 character");
                    return false;
                }

                var isNumeric = new Regex(@"[0-9]+");
                if (!isNumeric.IsMatch(textBox3.Text))
                {
                    MessageBox.Show("Password Must Be At least 1 Numeric");
                    return false;
                }

                var isSymbol = new Regex(@"[`~!@#$%^&*()=[{\]}|_+<>,./?;:-]+");
                if (!isSymbol.IsMatch(textBox3.Text))
                {
                    MessageBox.Show("Password Must Be At least 1 Symbol");
                    return false;
                }
            }

            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || richTextBox1.Text == "" || comboBox1.Text == "" || numericUpDown1.Value == 0 || dateTimePicker1.Value == null)
            {
                MessageBox.Show("All Data Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (IsValidEmail(textBox2.Text) != true)
            {
                MessageBox.Show("Invalid Email");
                return false;
            }

            return true;
        }
        private void SavUp_Click(object sender, EventArgs e)
        {
            if (ValidateUp())
            {
                if (textBox4.Text == "")
                {
                    string update = "update employee set id_job='" + comboBox1.SelectedValue + "', name_employee='" + textBox1.Text + "', email_employee='" + textBox2.Text + "', addres_employee='" + richTextBox1.Text + "', phone_number_employee='" + textBox4.Text + "', date_of_birth_employee='" + dateTimePicker1.Value + "', salary_employee='" + numericUpDown1.Value + "' where id_employee='" + id + "'";
                    try
                    {
                        Command.NonQuery(update);
                        MessageBox.Show("Data Success Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        Hidee();
                        btnShow();
                        DataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    if(textBox4.Text != textBox5.Text)
                    {
                        MessageBox.Show("Confirm Password Doesn't same with password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string pass = Encrypt.Pass(textBox5.Text);
                        string update = "update employee set id_job='" + comboBox1.SelectedValue + "', password_employee='"+ pass +"' ,name_employee='" + textBox1.Text + "', email_employee='" + textBox2.Text + "', addres_employee='" + richTextBox1.Text + "', phone_number_employee='" + textBox4.Text + "', date_of_birth_employee='" + dateTimePicker1.Value + "', salary_employee='" + numericUpDown1.Value + "' where id_employee='" + id + "'";
                        try
                        {
                            Command.NonQuery(update);
                            MessageBox.Show("Data Success Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            Hidee();
                            btnShow();
                            DataGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }
        private void Can_Click(object sender, EventArgs e)
        {
            Hidee();
            panelUp.Show();
            In.Show();
            SavUp.Show();
            SavIn.Show();
            Clear();
            dataGridView1.Enabled = true;
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete "+ dataGridView1.SelectedRows[0].Cells[2].Value +"?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result == DialogResult.OK)
                {
                    string delete = "delete from employee where id_employee='" + id + "'";
                    try
                    {
                        Command.NonQuery(delete);
                        MessageBox.Show("Data Success Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select One Row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ServiceDiscount ds = new ServiceDiscount();
            ds.Show();
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button5.BackColor = Color1;
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button5.BackColor = Color1;
        }

        private void button6_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button6.BackColor = Color1;
        }

        private void button6_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button6.BackColor = Color1;
        }

        private void button7_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button7.BackColor = Color1;
        }

        private void button7_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button7.BackColor = Color1;
        }

        private void button8_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button8.BackColor = Color1;
        }

        private void button8_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button8.BackColor = Color1;
        }

        private void button9_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button9.BackColor = Color1;
        }

        private void button9_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button9.BackColor = Color1;
        }

        private void button10_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button10.BackColor = Color1;
        }

        private void button10_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button10.BackColor = Color1;
        }

        private void button11_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button11.BackColor = Color1;
        }

        private void button11_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button11.BackColor = Color1;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button3.BackColor = Color1;
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button3.BackColor = Color1;
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
                MessageBox.Show("Error Phone Number");
            }
        }
    }

}
