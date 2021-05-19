using System;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    public partial class StoreForm : Form
    {
        private Resource _resource;
        public StoreForm()
        {
            InitializeComponent();
            using var db = new DiplomContext();
            listBox1.Items.AddRange(db.Resources.ToArray());
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndices.Count==0) return;
            _resource = (Resource)listBox1.SelectedItem;
            var ss = _resource.CountOnStore.ToString();
            textBox1.Text = _resource.CountOnStore.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar;
            if (!char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var count = int.Parse(textBox1.Text);
            count++;
            textBox1.Text = count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var count = int.Parse(textBox1.Text);
            count--;
            textBox1.Text = count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndices.Count==0) return;
            using var db = new DiplomContext();
            _resource.CountOnStore = int.Parse(textBox1.Text);
            db.Update(_resource);
            db.SaveChanges();
        }
    }
}