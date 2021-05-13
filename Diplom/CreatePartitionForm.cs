using System;
using System.Windows.Forms;
using static System.Char;

namespace Diplom
{
    public partial class CreatePartitionForm : Form
    {
        public CreatePartitionForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("Название не может быть пустым!");
            else
            {
                using var db = new DiplomContext();
                db.Add(new Partition {Title = textBox1.Text});
                db.SaveChanges();
                Dispose();
            }
        }
    }
}