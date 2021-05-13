using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Char;

namespace Diplom
{
    public partial class CreateItem : Form
    {
        public CreateItem()
        {
            InitializeComponent();
            using var db = new DiplomContext();
            listBox1.Items.AddRange(db.Partitions.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            AddFromListToListPartition(listBox1, listBox2);
            var partition = (Partition) listBox2.Items[^1];
            using var db = new DiplomContext();
            var resources = db.Resources.Where(resource => resource.PartitionId == partition.Id);
            listBox3.Items.AddRange(resources.ToArray());
        }

        private void AddFromListToListPartition(ListBox from, ListBox to)
        {
            foreach (int selectedIndex in from.SelectedIndices)
            {
                var partition = (Partition) from.Items[selectedIndex];
                to.Items.Add(partition);
                from.Items.RemoveAt(selectedIndex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var deletedPartitions = (from int selectedIndex in listBox2.SelectedIndices
                select (Partition) listBox2.Items[selectedIndex]).ToList();
            AddFromListToListPartition(listBox2, listBox1);
            foreach (Partition partition in deletedPartitions)
            {
                using var db = new DiplomContext();
                var resources = db.Resources.Where(resource => resource.PartitionId == partition.Id);
                foreach (var resource in resources)
                {
                    for (int i = 0; i < listBox3.Items.Count; i++)
                    {
                        if (((Resource) listBox3.Items[i]).Id == resource.Id)
                            listBox3.Items.RemoveAt(i);
                    }

                    for (int i = 0; i < listBox4.Items.Count; i++)
                    {
                        if (((ItemResource) listBox4.Items[i]).ResourceId == resource.Id)
                            listBox4.Items.RemoveAt(i);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndices.Count == 0) return;
            var resource = (Resource) listBox3.SelectedItem;
            var count = EnterCount(resource.Title);
            if (count == 0) return;
            var itemResource = new ItemResource {Count = count, Resource = resource, ResourceId = resource.Id};
            using var db = new DiplomContext();
            listBox4.Items.Add(itemResource);
            listBox3.Items.RemoveAt(listBox3.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var resource = ((ItemResource) listBox4.Items[listBox4.SelectedIndex]).Resource;
            listBox3.Items.Add(resource);
            listBox4.Items.RemoveAt(listBox4.SelectedIndex);
        }

        private int EnterCount(string title)
        {
            var form = new Form();
            var acceptButton = new Button();
            var cancelButton = new Button();
            var textBox = new TextBox();
            acceptButton.Text = "OK";
            cancelButton.Text = "Отмена";
            textBox.Location = new Point(form.Width/2-textBox.Width/2, form.Height/2-textBox.Height*2);
            acceptButton.Location = new Point(textBox.Left - textBox.Width/4, textBox.Height + textBox.Top + 10);
            acceptButton.Height = 30;
            cancelButton.Location =
                new Point(acceptButton.Left + acceptButton.Width + 5, textBox.Height + textBox.Top + 10);
            cancelButton.Height = 30;
            acceptButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
            form.Text = title;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.AcceptButton = acceptButton;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.CancelButton = cancelButton;
            textBox.KeyPress += KeyPress;
            form.Controls.Add(textBox);
            form.Controls.Add(acceptButton);
            form.Controls.Add(cancelButton);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                var count = string.IsNullOrWhiteSpace(textBox.Text)?0:int.Parse(textBox.Text);
                form.Dispose();
                return count;
            }
            form.Dispose();
            return 0;
        }

        private new void KeyPress(object o, KeyPressEventArgs e)
        {
            var number = e.KeyChar;
            if (!IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Не заполнено название новой позиции");
            else if (listBox2.Items.Count == 0)
                MessageBox.Show("Не заполнены используемые разделы");
            else if (listBox4.Items.Count == 0)
                MessageBox.Show("Не заполнены используемые части");
            else
            {
                var item = new Item {Title = textBox1.Text};
                using var db = new DiplomContext();
                db.Add(item);
                db.SaveChanges();
                foreach (Partition partition in listBox2.Items)
                {
                    var itemPartition = new ItemPartition
                        {ItemId = item.Id, PartitionId = partition.Id};
                    db.Add(itemPartition);
                    db.SaveChanges();
                }

                foreach (ItemResource itemResource in listBox4.Items)
                {
                    itemResource.Resource = null;
                    itemResource.ItemId = item.Id;
                    db.Add(itemResource);
                    db.SaveChanges();
                }
                Dispose();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var createResourceForm = new CreatePartitionForm();
            createResourceForm.ShowDialog();
            listBox1.Items.Clear();
            using var db = new DiplomContext();
            var userPartitions = listBox2.Items.Cast<Partition>().ToList();
            var partitions =
                db.Partitions.Where(partition => !userPartitions.Select(userPartition => userPartition.Id).Contains(partition.Id));
            listBox1.Items.AddRange(partitions.ToArray());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var createResourceForm = new CreateResourceForm();
            createResourceForm.ShowDialog();
            listBox3.Items.Clear();
            using var db = new DiplomContext();
            var itemResources = listBox4.Items.Cast<ItemResource>().ToList();
            var partitions = listBox2.Items.Cast<Partition>().ToList();
            var resources =
                db.Resources.Where(re => !itemResources.Select(itemResource => itemResource.Id).Contains(re.Id) && partitions.Select(partition => partition.Id).Contains(re.PartitionId));
            listBox3.Items.AddRange(resources.ToArray());
        }
    }
}