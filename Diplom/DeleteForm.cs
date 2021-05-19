using System;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    public partial class DeleteForm : Form
    {
        private string _type;
        public DeleteForm(string type)
        {
            InitializeComponent();
            _type = type;
            using var db = new DiplomContext();
            switch (_type)
            {
                case "item":
                    listBox1.Items.AddRange(db.Items.ToArray());
                    break;
                case "resource":
                    listBox1.Items.AddRange(db.Resources.ToArray());
                    break;
                case "measure":
                    listBox1.Items.AddRange(db.Measures.ToArray());
                    break;
                case "workshop":
                    listBox1.Items.AddRange(db.Workshops.ToArray());
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            using var db = new DiplomContext();
            switch (_type)
            {
                case "item":
                    db.Items.Remove((Item) listBox1.SelectedItem);
                    db.SaveChanges();
                    break;
                case "resource":
                    db.Resources.Remove((Resource) listBox1.SelectedItem);
                    db.SaveChanges();
                    break;
                case "measure":
                    db.Measures.Remove((Measure) listBox1.SelectedItem);
                    db.SaveChanges();
                    break;
                case "workshop":
                    db.Workshops.Remove((Workshop) listBox1.SelectedItem);
                    db.SaveChanges();
                    break;
            }
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}