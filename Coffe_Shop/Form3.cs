using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Coffe_Shop
{
    public partial class Form3 : Form
    {
        private double result=0;
        public Form3()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            add_product_to_pannel();
            textBox1.Text = "Next Customer";
        }

        private SqlConnection sqlConnection = null;
        private string connectionString = @"Data Source=DESKTOP-339MSAS\SQLEXPRESS;Initial Catalog=CoffeeShop;Integrated Security=True";


        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
    
        private void add_product_to_pannel()
        {
            SqlDataReader sqlDataReader = null;

            foreach (TabPage tp in tabControl1.TabPages)
            {
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;

                SqlCommand sqlCommand = new SqlCommand("SELECT Description,Price FROM [Table] WHERE Catagory='" + tp.Text + "'", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
               
                while (sqlDataReader.Read())
                {
                    Button b = new Button();
                    b.Size = new Size(110, 110);
                    b.BackColor = Color.Lime;
                    b.Text = sqlDataReader["Description"].ToString()+$" ${Convert.ToDouble(sqlDataReader["Price"].ToString()):f2}";
                    b.Tag = sqlDataReader["Price"].ToString();
                    b.Click += new EventHandler(b_Click);
                    flp.Controls.Add(b);
                }
                tp.Controls.Add(flp);
                sqlDataReader.Close();

                flp.AutoScroll = true;
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            listBox1.Items.Add(b.Text + "\n");
            result += Convert.ToDouble(b.Tag);
            
            textBox1.Text = "";
            textBox1.Text = b.Text;

            textBox2.Text = $"$ {result:f2}";
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object selectedProduct = listBox1.SelectedItem;
            if(listBox1.SelectedIndex>=0)
            {
                int pos = listBox1.SelectedItem.ToString().LastIndexOf("$");
                string str = listBox1.SelectedItem.ToString().Substring(pos + 1);
                result -= Convert.ToDouble(str);
                textBox2.Text = "";
                textBox2.Text = $"$ {result:f2}";
                listBox1.Items.Remove(selectedProduct);
            }
            else
            {
                MessageBox.Show("Select product to delete");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(result,listBox1);
            form4.Show();
            
            textBox1.Text = "Next Customer";
            result = 0;
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Next Customer";
            textBox2.Text = "";
            result = 0;
            listBox1.Items.Clear();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
