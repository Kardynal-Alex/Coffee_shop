using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Coffe_Shop
{
    public partial class Form4 : Form
    {
        private double result=0f;
        private double enter_sum = 0f;

        private ListBox listBox;
        public Form4(double res,ListBox listBoxCopy)
        {
            InitializeComponent();

            textBox1.Text = res.ToString();
            
            result = res;
            listBox = listBoxCopy;
            
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                enter_sum = Convert.ToDouble(textBox2.Text);
                if (result <= enter_sum)
                {
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = printDocument1;
                    printDocument1.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
                    DialogResult resultat = printDialog.ShowDialog();
                    if(resultat==DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                    System.Threading.Thread.Sleep(10);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ERROR");
                    textBox2.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Format Valid");
                textBox2.Text = "";
            }
            
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)//drawing check
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Times New Roman",12);

            float fontHeight = font.GetHeight();

            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphics.DrawString("Welcome to coffee shop\n", new Font("Times New Roman", 18), new SolidBrush(Color.Black), startX, startY);

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                graphics.DrawString(listBox.Items[i].ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
                offset += (int)fontHeight + 5;
            }
            
            offset += 20;
            graphics.DrawString("Total to Pay".PadRight(30) + string.Format("{0:f2} $",result), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += 20;
            graphics.DrawString("Enter sum".PadRight(30) + string.Format("{0:f2} $", enter_sum),font,new SolidBrush(Color.Black), startX, startY + offset);
            offset += 20;
            graphics.DrawString("Your rest".PadRight(30) + string.Format("{0:f2} $", (enter_sum-result)), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += 40;
            var now = DateTime.Now;
            graphics.DrawString("Date of purchase : "+ now.ToString("dd-MM-yyyy"), font, new SolidBrush(Color.Black), startX, startY + offset);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            listBox.Items.Clear();
        }
    }
}
