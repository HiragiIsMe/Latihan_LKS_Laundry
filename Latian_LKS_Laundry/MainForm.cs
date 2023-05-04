
namespace Latian_LKS_Laundry
{
    public partial class MainForm : Form
    {
        public Point mouseLocation;
        public MainForm()
        {
            InitializeComponent();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            label2.Text = "Hello, " + Model.Name;
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
            DateTime date = DateTime.Now;
            label1.Text = date.ToString();
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

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Logout?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                this.Hide();
                LoginForm login = new LoginForm();
                login.Show();
            }
        }


        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                button5.BackColor = Color.White;
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