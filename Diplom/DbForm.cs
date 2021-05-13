using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class DbForm : Form
    {
        public DbForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var createItemForm = new CreateItem();
            createItemForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var createPartitionForm = new CreatePartitionForm();
            createPartitionForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var createResourceForm = new CreateResourceForm();
            createResourceForm.ShowDialog();
        }
    }
}