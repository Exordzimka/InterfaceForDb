using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class CreateWorkshopForm : Form
    {
        public CreateWorkshopForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text)) return;
            var workshop = new Workshop {Title = textBox1.Text};
            using var db = new DiplomContext();
            db.Workshops.Add(workshop);
            db.SaveChanges();
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}