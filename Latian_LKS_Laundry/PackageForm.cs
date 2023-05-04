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
    public partial class PackageForm : Form
    {
        public Point mouseMove;
        public PackageForm()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
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
            string query = "select id_package, name_package as 'Package Name', description_package as 'Description', price_package as 'Price', duration_package as 'Estimation Duration' from package";

            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
        }
        void Showw()
        {
            panelPack.Show();
            panelSav.Show();
            panelUp.Hide();
        }

        void Hidee()
        {
            panelPack.Hide();
            panelSav.Hide();
            panelUp.Show();
        }

        void detHidee()
        {
            panelDeta.Hide();
            delDet.Hide();
        }

        void detShoww()
        {
            panelDeta.Show();
            delDet.Show();
        }
        void btnShow()
        {
            SavIn.Show();
            SavUp.Show();
        }

        void Clear()
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
            numericUpDown1.Text = "";
            numericUpDown2.Text = ""; 
        }
        private void PackageForm_Load(object sender, EventArgs e)
        {
            label2.Text = "Hello, " + Model.Name;
            Color Color1 = System.Drawing.ColorTranslator.FromHtml("#333C4A");
            button7.BackColor = Color1;
            this.CenterToScreen();
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            loadSer();
            DataGrid();
            Hidee();
            detHidee();
        }
        int id;
        void DataGrid2()
        {
            string query = "select id_detail_package, name_Service as 'Service Name', price_unit_service as 'Price', estimation_duration_service as 'Estimation Duration', total_unit_service_detail_package as 'Total Unit' from detailpackage join service on detailpackage.id_service = service.id_service where id_package='"+ id +"'";
            dataGridView2.DataSource = Command.getData(query);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.Columns[0].Visible = false;
        }
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
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                numericUpDown2.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);

                DataGrid2();
                detShoww();
            }
        }
        int idDetail;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {

            }
            else
            {
                dataGridView2.CurrentRow.Selected = true;
                idDetail = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void In_Click(object sender, EventArgs e)
        {
            In.Hide();
            detHidee();
            Showw();
            SavUp.Hide();
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            dataGridView2.DataSource = "";
            Clear();
        }
        bool Validate()
        {
            if(textBox1.Text == "" || richTextBox1.Text == "" || numericUpDown1.Value == 0 || numericUpDown2.Value == 0)
            {
                MessageBox.Show("All Data Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;   
            }
            return true;
        }
        private void SavIn_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                try
                {
                    string insert = "insert into package values ('"+ textBox1.Text +"', '"+ numericUpDown1.Value +"', '"+ richTextBox1.Text +"', '"+ numericUpDown2.Value +"')";
                    Command.NonQuery(insert);

                    MessageBox.Show("Data Success Inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hidee();
                    dataGridView1.Enabled = true;
                    dataGridView2.Enabled = true;
                    DataGrid();
                    In.Show();
                    btnShow();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Up_Click(object sender, EventArgs e)
        {
            if(id > 0)
            {
                In.Hide();
                detHidee();
                Showw();
                SavIn.Hide();
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                dataGridView2.DataSource = "";
            }
            else
            {
                MessageBox.Show("Please Select One Row In Detail Package!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavUp_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                try
                {
                    string update = "update package set name_package='"+ textBox1.Text +"', description_package='"+ richTextBox1.Text +"', price_package='"+ numericUpDown1.Value +"', duration_package='"+ numericUpDown2.Value +"' where id_package='"+ id +"'";
                    Command.NonQuery(update);

                    MessageBox.Show("Data Success Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnShow();
                    DataGrid();
                    dataGridView1.Enabled = true;
                    dataGridView2.Enabled = true;
                    Hidee();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView1.SelectedRows[0].Cells[1].Value + "?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    string delete = "delete from package where id_package='" + id + "'";
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

        private void delDet_Click(object sender, EventArgs e)
        {
            if (idDetail > 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Delete " + dataGridView2.SelectedRows[0].Cells[1].Value + "?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    string delete = "delete from detailpackage where id_detail_package='" + idDetail + "'";
                    try
                    {
                        Command.NonQuery(delete);
                        MessageBox.Show("Data Success Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGrid2();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select One Row In Detail Package!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void loadSer()
        {
            string query = "select * from service";
            comboBox1.DataSource = Command.getData(query);

            comboBox1.DisplayMember = "name_service";
            comboBox1.ValueMember = "id_service";
        }
        private void add_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "" || numericUpDown3.Value == 0)
            {
                MessageBox.Show("All Data Must Be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string insertDet = "insert into detailpackage values ('" + comboBox1.SelectedValue + "', '" + id + "', '" + numericUpDown3.Value + "')";
                    Command.NonQuery(insertDet);

                    MessageBox.Show("Data Success Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGrid2();
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
            Clear();
            In.Show();
            dataGridView1.Enabled = true;
            dataGridView2.Enabled = true;
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
        void Search()
        {
            string query = "select id_package, name_package as 'Package Name', description_package as 'Description', price_package as 'Price', duration_package as 'Estimation Duration' from package where name_package like '%"+ search.Text +"%'";

            dataGridView1.DataSource = Command.getData(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].Visible = false;
        }
        private void search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ServiceDiscount ds = new ServiceDiscount();
            ds.Show();
        }

        private void PackageForm_KeyDown(object sender, KeyEventArgs e)
        {
            button1.BackColor = Color.White;
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
