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

namespace Latian_LKS_Laundry
{
    public partial class InputServiceTransactionForm : Form
    {

        SqlConnection connection = new SqlConnection(Utils.conn);
        SqlCommand cmd;
        SqlDataReader reader;
        public Point mouseMove;
        public InputServiceTransactionForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            label1.Text = dateTime.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
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

        void Discount()
        {
            
        }
        void Service()
        {
            string query = "select * from service";
            comboBox1.DataSource =  Command.getData(query);

            comboBox1.DisplayMember = "name_Service";
            comboBox1.ValueMember = "id_service";
        }
        void DataGrid()
        {
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Name = "Service Name";
            dataGridView1.Columns[2].Name = "Service Price";
            dataGridView1.Columns[3].Name = "Total Unit";
            dataGridView1.Columns[4].Name = "Estimation Duration Per Service";
            dataGridView1.Columns[5].Name = "Discount";
            dataGridView1.Columns[6].Name = "Sub Total";

            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        void Calculate()
        {
            int totalprice = 0;
            int totalest = 0;
            int discount = 0; 
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells[5].Value != "")
                {
                    discount += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                }
                else
                {
                    discount = 0;
                }
                totalprice += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
                totalest += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }

            labelDisc.Text = discount.ToString();
            labelPrice.Text = totalprice.ToString();
            labelTime.Text = totalest.ToString();

        }
        private void Add_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Field Must Be Filled", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("select * from service where id_service = '" + comboBox1.SelectedValue + "'", connection);
                    reader = cmd.ExecuteReader();
                    reader.Read();

                    int id = Convert.ToInt32(reader["id_service"]);
                    string name = Convert.ToString(reader["name_service"]);
                    int price = Convert.ToInt32(reader["price_unit_service"]);
                    int unit = Convert.ToInt32(numericUpDown1.Value);
                    string duration = Convert.ToString(reader["estimation_duration_service"]);
                    int subtotal = Convert.ToInt32(price * unit);
                    subtotal -= Convert.ToInt32(comboBox2.SelectedValue); 
                    connection.Close();
                    int discount = Convert.ToInt32(comboBox2.SelectedValue);
                    if(comboBox2.SelectedValue == null)
                    {
                        discount = 0;
                    }
                    numericUpDown1.Value = 0;
                    string[] add = { Convert.ToString(id), name, Convert.ToString(price), Convert.ToString(unit), duration, discount.ToString() ,Convert.ToString(subtotal) };
                    dataGridView1.Rows.Add(add);
                }catch (Exception ex)
                {
                    throw;
                }
            }
            Calculate();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
            }
        }

        private void Rem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                if(dataGridView1.CurrentRow.Selected != false)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                else
                {
                    MessageBox.Show("Please Select 1 Rows To Remove", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Calculate();
        }
        int id_customer;
        int id_header;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand("select top(1) * from customer where phone_number_customer like '%" + textBox2.Text + "%' order by id_customer desc", connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    id_customer = Convert.ToInt32(reader["id_customer"]);
                    textBox1.Text = Convert.ToString(reader["name_customer"]);
                    richTextBox1.Text = Convert.ToString(reader["address_customer"]);
                }
                connection.Close();
            }catch (Exception ex)
            {
                throw;
            }
        }
        void clear()
        {
            labelTime.Text = "0";
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            dataGridView1.Rows.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                if(id_customer != 0)
                {
                    try
                    {
                        DateTime timee = DateTime.Now.AddHours(Convert.ToInt32(labelTime.Text));
                        string insertHeader = "insert into headertransaction values ('" + Model.id + "', '" + id_customer + "', '" + DateTime.Now + "', '" + timee + "')";
                        Command.NonQuery(insertHeader);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    try
                    {
                        string idHeader = "select top(1) id_header_transaction from headertransaction order by id_header_transaction desc";
                        connection.Open();
                        cmd = new SqlCommand(idHeader, connection);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            id_header = Convert.ToInt32(reader["id_header_transaction"]);
                            connection.Close();
                            try
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                {
                                    DateTime detai = DateTime.Now.AddHours(Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value));
                                    string insertDetail = "insert into detailtransaction(id_service, id_header_transaction, price_detail_transaction, total_unit_detail_transaction) values ('" + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) + "', '" + id_header + "', '" + Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) + "', '" + Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value) + "')";
                                    Command.NonQuery(insertDetail);
                                }
                                MessageBox.Show("Transaction Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                printPreviewDialog1.Document = printDocument1;
                                printPreviewDialog1.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            clear();
                        }
                        else
                        {
                            connection.Close();
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Select A Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select Service", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox1.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            ServiceDiscount ds = new ServiceDiscount();
            ds.Show();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            string discount = "select * from servicediscount where id_service = " + comboBox1.SelectedValue + " and expired_discount >= CONVERT(DATE, GETDATE())";
            comboBox2.DataSource = Command.getData(discount);
            comboBox2.DisplayMember = "name_discount";
            comboBox2.ValueMember = "discount";
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

        private void button4_Enter(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button3.BackColor = Color1;
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#495664");
            button3.BackColor = Color1;
        }

        private void InputServiceTransactionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.W)
            {
                comboBox1.Focus();
            }

            if (e.Control && e.KeyCode == Keys.Q)
            {
                numericUpDown1.Focus();
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
                comboBox2.Focus();
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                Add.PerformClick();
            }

            if (e.Control && e.KeyCode == Keys.P)
            {
                textBox2.Focus();
            }

            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void InputServiceTransactionForm_Load_1(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            label2.Text = "Hello, " + Model.Name;
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button8.BackColor = Color1;
            this.CenterToScreen();
            Discount();
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            Service();
            DataGrid();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Testttt", new Font("Arial", 30, FontStyle.Regular), Brushes.Black, new Point(10,10));
        }
    }
}
