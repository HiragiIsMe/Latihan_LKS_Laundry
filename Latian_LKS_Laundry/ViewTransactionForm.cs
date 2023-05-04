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
    public partial class ViewTransactionForm : Form
    {
        public Point mouseMove;
        SqlConnection conn = new SqlConnection(Utils.conn);
        public ViewTransactionForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            label1.Text = dt.ToString();
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            ViewTransactionForm viewTransactionForm = new ViewTransactionForm();
            viewTransactionForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            InputPackageTransactionForm inputPackageTransactionForm = new InputPackageTransactionForm();
            inputPackageTransactionForm.Show();
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
            if (e.Button == MouseButtons.Left)
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
            string queryHead = "select headertransaction.id_header_transaction, customer.name_customer as 'Customer Name', employee.name_employee as 'Employe Name',headertransaction.transaction_date_time_header_transaction as 'Transaction Time', headertransaction.complete_estimation_date_time_header as 'Complete Estimation Time' from headertransaction join customer on (headertransaction.id_customer = customer.id_customer) join employee on (headertransaction.id_employee = employee.id_employee)";

            dataGridView1.DataSource = Command.getData(queryHead);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ViewTransactionForm_Load(object sender, EventArgs e)
        {
            label2.Text = "Hello, " + Model.Name;
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button10.BackColor = Color1;
            this.CenterToScreen();
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            DataGrid();

            /*var month = new List<Tuple<int, string>>()
            {
                Tuple.Create(1, "January"), 
                Tuple.Create(2, "February"),
                Tuple.Create(3, "March"),
                Tuple.Create(4, "April"),
                Tuple.Create(5, "May"),
                Tuple.Create(6, "June"),
                Tuple.Create(7, "July"),
                Tuple.Create(8, "August"),
                Tuple.Create(9, "September"),
                Tuple.Create(10, "October"),
                Tuple.Create(11, "November"),
                Tuple.Create(12, "December")
            };
            comboBox1.DataSource = month;
            comboBox1.DisplayMember = "Item2";
            comboBox1.ValueMember = "Item1";*/
        }
        void DataGrid2()
        {

            SqlCommand cmd = new SqlCommand("select * from detailtransaction where id_header_transaction='" + id + "'", conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                
                if (reader["id_service"] == DBNull.Value)
                {
                    conn.Close();
                    string query = "select detailtransaction.id_detail_transaction, package.name_package as 'Name', package.price_package as 'Package Price', detailtransaction.complete_datetime_detail_transaction as 'Complete Time' from detailtransaction join package on detailtransaction.id_package = package.id_package where detailtransaction.id_header_transaction='" + id + "'";

                    dataGridView2.DataSource = Command.getData(query);
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.Columns[0].Visible = false;
                }else if (reader["id_package"] == DBNull.Value)
                {
                    conn.Close();
                    string query = "select detailtransaction.id_detail_transaction, service.name_service as 'Name', service.price_unit_service as 'Service Price Per Unit', detailtransaction.complete_datetime_detail_transaction as 'Complete Time' from detailtransaction join service on detailtransaction.id_service = service.id_service where detailtransaction.id_header_transaction='" + id + "'";

                    dataGridView2.DataSource = Command.getData(query);
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.Columns[0].Visible = false;
                }
                conn.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        int id;
        int id_detail;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                DataGrid2();
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView2.CurrentRow.Selected = true;
                id_detail = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
            }
            
        }

        private void Comp_Click(object sender, EventArgs e)
        {
            if(dataGridView2.CurrentRow.Selected == true)
            {
                string update = "update detailtransaction set complete_datetime_detail_transaction = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id_detail_transaction = '" + id_detail + "'";
                try
                {
                    Command.NonQuery(update);
                    MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGrid2();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select One Row In Detail Transaction", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            string searching = "select headertransaction.id_header_transaction, customer.name_customer as 'Customer Name', employee.name_employee as 'Employe Name',headertransaction.transaction_date_time_header_transaction as 'Transaction Time', headertransaction.complete_estimation_date_time_header as 'Complete Estimation Time', headertransaction.price as 'Total Price', headertransaction.discount as 'Discount', headertransaction.total_price as 'Total Pay' from headertransaction join customer on (headertransaction.id_customer = customer.id_customer) join employee on (headertransaction.id_employee = employee.id_employee) where customer.name_customer like '%" + search.Text + "%'";
            dataGridView1.DataSource = Command.getData(searching);
        }

        private void search_Click(object sender, EventArgs e)
        {
            search.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            ServiceDiscount ds = new ServiceDiscount();
            ds.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
/*string query = "select headertransaction.id_header_transaction as 'ID', headertransaction.id_customer as 'Customer ID', customer.name_customer as 'Customer Name', employee.name_employee, headertransaction.transaction_date_time_header_transaction as 'Complete Estimation Time' from headertransaction join customer on (headertransaction.id_customer = customer.id_customer) join employee on (headertransaction.id_employee = employee.id_employee) where MONTH(transaction_date_time_header_transaction) = '" + comboBox1.SelectedValue + "'";

dataGridView1.DataSource = Command.getData(query);
dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;*/