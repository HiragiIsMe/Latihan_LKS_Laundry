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
using System.Text.RegularExpressions;
namespace Latian_LKS_Laundry
{
    public partial class CustomerForm : Form
    {
        SqlConnection conn = new SqlConnection(Utils.conn);
        public Point mouseMove;
        public CustomerForm()
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseMove = new Point(-e.X, -e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseMove.X, mouseMove.Y);
                Location = mousePose;
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

        void DataGrid()
        {
            string query = "select id_customer, name_customer as 'Customer Name', phone_number_customer as 'Phone Number', address_customer as 'Adrress' from customer";
            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].Visible = false;
        }

        void Search()
        {
            string query = "select id_customer, name_customer as 'Customer Name', phone_number_customer as 'Phone Number', address_customer as 'Adrress' from customer where name_customer like '%" + search.Text + "%' or address_customer like '%" + search.Text + "%' or phone_number_customer like '%" + search.Text + "%'";
            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].Visible = false;
        }
        void Showw()
        {
            panelText.Show();
            panelSav.Show();
            panelUp.Hide();
        }

        void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
        }
        void btnShow()
        {
            SavIn.Show();
            SavUp.Show();
            In.Show();
        }

        void Hidee()
        {
            panelText.Hide();
            panelSav.Hide();
            panelUp.Show();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button11.BackColor = Color1;
            this.CenterToScreen();
            label2.Text = "Hello, " + Model.Name;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            Hidee();
            DataGrid();
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

        private void search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void In_Click(object sender, EventArgs e)
        {
            Showw();
            SavUp.Hide();
            In.Hide();
            dataGridView1.Enabled = false;
            Clear();
        }
        bool Validate()
        {
            if (textBox2.Text[0] != '+')
            {
                MessageBox.Show("Invalid Number Phone");
                return false;
            }

            SqlCommand cmd = new SqlCommand("select * from customer where phone_number_customer = '"+ textBox2.Text +"'", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                conn.Close();
                MessageBox.Show("Phone Number Is Already Use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(textBox1.Text == "" || textBox2.Text == "" || richTextBox1.Text == "")
            {
                conn.Close();
                MessageBox.Show("All Data Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            conn.Close();
            return true;
        }
        
        private void SavIn_Click(object sender, EventArgs e)
        {
            if(Validate())
            {
                try{
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into customer values (@name, '" + textBox2.Text + "', '" + richTextBox1.Text + "')", conn);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Data Success Inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hidee();
                    btnShow();
                    DataGrid();
                    Clear();
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
            if (id > 0)
            {
                Showw();
                SavIn.Hide();
                In.Hide();
            }
            else
            {
                MessageBox.Show("Please Select One Row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavUp_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                try
                {
                    string update = "update customer set name_customer = '" + textBox1.Text + "', phone_number_customer = '" + textBox2.Text + "', address_customer = '" + richTextBox1.Text + "' where id_customer = '"+ id +"'";
                    Command.NonQuery(update);

                    MessageBox.Show("Data Success Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hidee();
                    btnShow();
                    DataGrid();
                    Clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Can_Click(object sender, EventArgs e)
        {
            Hidee();
            btnShow();
            dataGridView1.Enabled = true;
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

                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[1].Value + "?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    string delete = "delete from customer where id_customer='" + id + "'";
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
                MessageBox.Show("Error Phone Number");
            }
        }

        private void button5_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button5.BackColor = Color1;
        }

        private void button5_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
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
    }
}
