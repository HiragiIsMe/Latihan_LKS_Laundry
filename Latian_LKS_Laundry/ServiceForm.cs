using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Latian_LKS_Laundry
{
    public partial class ServiceForm : Form
    {
        public Point mouseMove;
        public ServiceForm()
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
            string query = "select category.id_category, unit.id_unit, id_service as 'Id Service', name_service as 'Service Name', name_category as 'Category', name_unit as 'Unit', price_unit_service as 'Price', estimation_duration_service as 'Estimation Duration' from service join category on (service.id_category = category.id_category) join unit on (service.id_unit = unit.id_unit)";
            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }

        void Category()
        {
            string query = "select * from category";
            comboBox1.DataSource = Command.getData(query);

            comboBox1.DisplayMember = "name_category";
            comboBox1.ValueMember = "id_category";
        }

        void Unit()
        {
            string query = "select * from unit";
            comboBox2.DataSource = Command.getData(query);

            comboBox2.DisplayMember = "name_unit";
            comboBox2.ValueMember = "id_unit";
        }
        void Clear()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            numericUpDown1.Text = "";
            numericUpDown2.Text = "";
        }
        void Showw()
        {
            panelText.Show();
            panelSav.Show();
            panelUp.Hide();
        }

        void btnShow()
        {
            SavIn.Show();
            SavUp.Show();
        }

        void Hidee()
        {
            panelText.Hide();
            panelSav.Hide();
            panelUp.Show();
        }
        private void ServiceForm_Load(object sender, EventArgs e)
        {
            label2.Text = "Hello, " + Model.Name;
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button6.BackColor = Color1;
            this.CenterToScreen();
            label2.Text = "Hello, " + Model.Name;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            DataGrid();
            Category();
            Unit();
            Hidee();
        }

        private void In_Click(object sender, EventArgs e)
        {
            Showw();
            SavUp.Hide();
            In.Hide();
            Clear();
            dataGridView1.Enabled = false;
        }
        bool validate()
        {
            if(textBox1.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || numericUpDown1.Value == 0 || numericUpDown2.Value == 0)
            {
                MessageBox.Show("All Data Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void SavIn_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                try
                {
                    string insert = "insert into service values ('" + comboBox1.SelectedValue + "', '" + comboBox2.SelectedValue + "', '" + textBox1.Text + "', '" + numericUpDown1.Value + "', '" + numericUpDown2.Value + "')";
                    Command.NonQuery(insert);

                    MessageBox.Show("Data Success Inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hidee();
                    btnShow();
                    In.Show();
                    DataGrid();
                    dataGridView1.Enabled=true;
                }
                catch(Exception ex)
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
            if (validate())
            {
                try
                {
                    string update = "update service set id_category = '" + comboBox1.SelectedValue + "', id_unit = '"+ comboBox2.SelectedValue +"', name_service = '"+ textBox1.Text +"', price_unit_service = '"+ numericUpDown1.Value +"', estimation_duration_service = '"+ numericUpDown2.Value +"' where id_service = '"+ id +"'";
                    Command.NonQuery(update);

                    MessageBox.Show("Data Success Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hidee();
                    btnShow();
                    In.Show();
                    DataGrid();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        void Search()
        {
            string query = "select category.id_category, unit.id_unit, id_service as 'Id Service', name_service as 'Service Name', name_category as 'Category', name_unit as 'Unit', price_unit_service as 'Price', estimation_duration_service as 'Estimation Duration' from service join category on (service.id_category = category.id_category) join unit on (service.id_unit = unit.id_unit) where name_service like '%" + search.Text + "%' or name_category like '%" + search.Text + "%' or name_unit like '%" + search.Text + "%' or price_unit_service like '%" + search.Text + "%'";
            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
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

        private void Can_Click(object sender, EventArgs e)
        {
            Hidee();
            btnShow();
            Clear();
            dataGridView1.Enabled = true;
            In.Show();
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
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox2.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                numericUpDown2.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[3].Value + "?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    string delete = "delete from service where id_service='" + id + "'";
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
