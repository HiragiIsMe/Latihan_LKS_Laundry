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
    public partial class PackageDiscount : Form
    {
        public PackageDiscount()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            label1.Text = date.ToString();
        }

        private void Serv_Click(object sender, EventArgs e)
        {
            this.Close();
            ServiceDiscount serviceDiscount = new ServiceDiscount();
            serviceDiscount.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ServiceDiscount ds = new ServiceDiscount();
            ds.Show();
        }
        void DataGrid()
        {
            string query = "select packagediscount.id_discount, package.id_package, packagediscount.name_discount as 'Name', package.name_package as 'Service', packagediscount.discount as 'Discount', packagediscount.expired_discount as 'Expired Date' from packagediscount join package on packagediscount.id_package = package.id_package";

            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        void Package()
        {
            string query = "select * from package";

            comboBox1.DataSource = Command.getData(query);
            comboBox1.DisplayMember = "name_package";
            comboBox1.ValueMember = "id_package";
        }
        private void PackageDiscount_Load(object sender, EventArgs e)
        {
            Color Color2 = System.Drawing.ColorTranslator.FromHtml("#495664");
            Pack.BackColor = Color2;
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button3.BackColor = Color1;
            this.CenterToScreen();
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            label2.Text = "Hello, " + Model.Name;
            DataGrid();
            Package();
            Hidee();
        }
        void Hidee()
        {
            panelSav.Hide();
            panelForm.Hide();
            panelUp.Show();
            buttonIn.Show();
        }
        void Showw()
        {
            panelSav.Show();
            panelForm.Show();
            panelUp.Hide();
            buttonIn.Hide();
        }
        void Clear()
        {
            textBox1.Text = "";
            numericUpDown1.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = "";
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Clear();
            Showw();
            SavUp.Hide();
            panelUp.Hide();
            buttonIn.Hide();
        }

        private void Up_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                Showw();
                panelUp.Hide();
                buttonIn.Hide();
                SavIn.Hide();
            }
            else
            {
                MessageBox.Show("Please Select One Row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[2].Value + "?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        string delete = "delete from packagediscount where id_discount=" + id + "";
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

        private void SavUp_Click(object sender, EventArgs e)
        {
            if (ValidateIn())
            {
                try
                {
                    string update = "update packagediscount set id_package=" + comboBox1.SelectedValue + ", name_discount='" + textBox1.Text + "', discount=" + numericUpDown1.Value + ", expired_discount='" + dateTimePicker1.Value.Date + "' where id_discount=" + id + "";
                    Command.NonQuery(update);
                    MessageBox.Show("Data Success Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SavIn.Show();
                    Hidee();
                    DataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        bool ValidateIn()
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || numericUpDown1.Value == 0 || dateTimePicker1.Value == null)
            {
                MessageBox.Show("All Field Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void SavIn_Click(object sender, EventArgs e)
        {
            if (ValidateIn())
            {
                try
                {
                    string insert = "insert into packagediscount values (" + comboBox1.SelectedValue + ",'" + textBox1.Text + "', " + numericUpDown1.Value + ", '" + dateTimePicker1.Value.Date + "')";
                    Command.NonQuery(insert);
                    MessageBox.Show("Data Success Inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SavUp.Show();
                    Hidee();
                    DataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Can_Click(object sender, EventArgs e)
        {
            Hidee();
            SavIn.Show();
            SavUp.Show();
            Clear();
        }
        int id;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                numericUpDown1.Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value);

                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value;

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
