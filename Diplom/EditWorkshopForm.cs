using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class EditWorkshopForm : Form
    {
        private Workshop _workshop;
        public EditWorkshopForm(Workshop workshop)
        {
            InitializeComponent();
            _workshop = workshop;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text)) return;
            _workshop.Title = textBox1.Text;
            using var db = new DiplomContext();
            db.Update(_workshop);
            db.SaveChanges();
            Dispose();
        }
    }
}