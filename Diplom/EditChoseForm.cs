using System;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    public partial class EditChoseForm : Form
    {
        private string _type;
        public EditChoseForm(string type)
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            ToEdit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            ToEdit();
        }

        private void ToEdit()
        {
            switch (_type)
            {
                case "item":
                    var editItemForm = new EditItem((Item)listBox1.SelectedItem);
                    editItemForm.ShowDialog();
                    break;
                case "resource":
                    var editResourceForm = new EditResourceForm((Resource)listBox1.SelectedItem);
                    editResourceForm.ShowDialog();
                    break;
                case "measure":
                    var editMeasureForm = new EditMeasureForm((Measure)listBox1.SelectedItem);
                    editMeasureForm.ShowDialog();
                    break;
            }
        }
    }
}