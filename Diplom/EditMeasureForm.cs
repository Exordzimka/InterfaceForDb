using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class EditMeasureForm : Form
    {
        public EditMeasureForm(Measure measure)
        {
            InitializeComponent();
            textBox1.Text = measure.Title;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("Название не может быть пустым!");
            else
            {
                using var db = new DiplomContext();
                db.Add(new Measure {Title = textBox1.Text});
                db.SaveChanges();
                Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}