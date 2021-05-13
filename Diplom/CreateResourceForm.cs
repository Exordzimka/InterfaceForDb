using System;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    public partial class CreateResourceForm : Form
    {
        public CreateResourceForm()
        {
            InitializeComponent();
            using var db = new DiplomContext();
            comboBox1.Items.AddRange(db.Partitions.ToArray());
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                MessageBox.Show("Название не может быть пустым!");
            else
            {
                using var db = new DiplomContext();
                db.Add(new Resource {Title = textBox1.Text, PartitionId = ((Partition) comboBox1.SelectedItem).Id});
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