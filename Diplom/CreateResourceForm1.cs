using System;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    public partial class CreateResourceForm1 : Form
    {
        public CreateResourceForm1()
        {
            InitializeComponent();
            using var db = new DiplomContext();
            comboBox1.Items.AddRange(db.Measures.ToArray());
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            using var db1 = new DiplomContext();
            comboBox2.Items.AddRange(db1.Workshops.ToArray());
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("Название не может быть пустым!");
            else
            {
                using var db = new DiplomContext();
                var count = string.IsNullOrWhiteSpace(textBox2.Text) ? 0 : int.Parse(textBox2.Text);
                db.Add(new Resource {Title = textBox1.Text, MeasureId = ((Measure) comboBox1.SelectedItem).Id, CountOnStore = count});
                db.SaveChanges();
                Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar;
            if (!char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
    }
}