using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Coffe_Shop
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            selection_type();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

        }

        private SqlConnection sqlConnection = null;
        private string connectionString = @"Data Source=DESKTOP-339MSAS\SQLEXPRESS;Initial Catalog=CoffeeShop;Integrated Security=True";

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
               comboBox1.Text!="" && maskedTextBox1.Text!="")
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO[Table] (ProductId,Description,Price,Catagory) VALUES (@ProductId,@Description,@Price,@Catagory)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("ProductId", textBox2.Text);
                sqlCommand.Parameters.AddWithValue("Description", textBox1.Text);
                sqlCommand.Parameters.AddWithValue("Price", maskedTextBox1.Text);
                sqlCommand.Parameters.AddWithValue("Catagory", comboBox1.Text);

                int check = sqlCommand.ExecuteNonQuery();
                if(check!=0)
                {
                    MessageBox.Show("ADDED");
                }
                else
                {
                    MessageBox.Show("ERROR(Presumably all fields were not filled)");
                }
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void selection_type()
        {
            comboBox1.Items.AddRange(new string[] { "Hot Drinks", "Cold Drinks", "Other" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";textBox1.Text = "";
            maskedTextBox1.Text = ""; comboBox1.Text = "";
        }
    }
}
