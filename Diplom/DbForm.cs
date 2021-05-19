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
        
        private void button3_Click(object sender, EventArgs e)
        {
            var createResourceForm = new CreateResourceForm1();
            createResourceForm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var createMeasureForm = new CreateMeasureForm();
            createMeasureForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteForm("item");
            deleteForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteForm("resource");
            deleteForm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteForm("measure");
            deleteForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var editChoseForm = new EditChoseForm("item");
            editChoseForm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var editChoseForm = new EditChoseForm("resource");
            editChoseForm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var editChoseForm = new EditChoseForm("measure");
            editChoseForm.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var createWorkShopForm = new CreateWorkshopForm();
            createWorkShopForm.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var editChoseForm = new EditChoseForm("workshop");
            editChoseForm.ShowDialog();   
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteForm("workshop");
            deleteForm.ShowDialog();
        }
    }
}