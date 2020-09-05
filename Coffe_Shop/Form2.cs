using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Coffe_Shop
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            selection_type();
        }

        private SqlConnection sqlConnection = null;
        private string connectionString = @"Data Source=DESKTOP-339MSAS\SQLEXPRESS;Initial Catalog=CoffeeShop;Integrated Security=True";

        private void Form2_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Table]", sqlConnection);

            // TODO: данная строка кода позволяет загрузить данные в таблицу "coffeeShopDataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.coffeeShopDataSet.Table);

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void update_data()
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [Table]", connectionString);

            sqlDataAdapter.Fill(dataSet, "Table");
            dataGridView1.DataSource = dataSet.Tables["Table"].DefaultView;
        }

        private void uPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update_data();
        }

        private void selection_type()
        {
            comboBox1.Items.AddRange(new string[] { "Hot Drinks", "Cold Drinks", "Other" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            update_data();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Table] WHERE Catagory='" + comboBox1.Text + "'", sqlConnection);
                int check=sqlCommand.ExecuteNonQuery();
                if(check!=0)
                {
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter ds = new SqlDataAdapter(sqlCommand);
                    ds.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("ERROR");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text)&&!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [Table] WHERE [ProductId]=@ProductId", sqlConnection);
                sqlCommand.Parameters.AddWithValue("ProductId", textBox1.Text);
                int check = sqlCommand.ExecuteNonQuery();
                if(check!=0)
                {
                    MessageBox.Show("Deleted");
                }
                else
                {
                    MessageBox.Show("ERROR");
                }
            }
            update_data();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value!=null)   
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            }
        }
    }
}
