using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Char;

namespace Diplom
{
    public partial class EditItem : Form
    {
        private Item _item;

        public EditItem(Item item)
        {
            _item = item;
            InitializeComponent();
            textBox1.Text = _item.Title;
            using (var db = new DiplomContext())
            {
                listBox2.Items.AddRange(db.ItemItems.Where(itemItem => itemItem.ParentItemId == _item.Id).ToArray());
            }

            using (var db = new DiplomContext())
            {
                listBox4.Items.AddRange(db.ItemResources.Where(resource => resource.ItemId == item.Id).ToArray());
            }

            using (var db = new DiplomContext())
            {
                var usedItemIds = listBox2.Items.Cast<ItemItem>().Select(itemItem => itemItem.ChildItemId);
                foreach (var unusedItem in db.Items)
                {
                    if (!usedItemIds.Contains(unusedItem.Id))
                        listBox1.Items.Add(unusedItem);
                }
            }

            using (var db = new DiplomContext())
            {
                var usedResourceIds = listBox4.Items.Cast<ItemResource>().Select(resource => resource.Id);
                foreach (var unusedResource in db.Resources)
                {
                    if (!usedResourceIds.Contains(unusedResource.Id))
                        listBox3.Items.Add(unusedResource);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            var item = (Item) listBox1.SelectedItem;
            var count = EnterCount(item.Title);
            if (count <= 0) return;
            var itemResource = new ItemItem {Count = count, ChildItemId = item.Id};
            listBox2.Items.Add(itemResource);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndices.Count == 0) return;
            using var db = new DiplomContext();
            var itemId = ((ItemItem) listBox2.Items[listBox2.SelectedIndex]).ChildItemId;
            listBox1.Items.Add(db.Items.FirstOrDefault(x => x.Id == itemId));
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndices.Count == 0) return;
            var resource = (Resource) listBox3.SelectedItem;
            var count = EnterCount(resource.Title);
            if (count <= 0) return;
            var itemResource = new ItemResource {Count = count, Resource = resource, ResourceId = resource.Id};
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
            textBox.Location = new Point(form.Width / 2 - textBox.Width / 2, form.Height / 2 - textBox.Height * 2);
            acceptButton.Location = new Point(textBox.Left - textBox.Width / 4, textBox.Height + textBox.Top + 10);
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
                var count = string.IsNullOrWhiteSpace(textBox.Text) ? 0 : int.Parse(textBox.Text);
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
            else if (listBox4.Items.Count == 0 && listBox2.Items.Count <= 1)
                MessageBox.Show("Новое изделие не может быть состоять из одного другого");
            else if (listBox2.Items.Count == 0 && listBox4.Items.Count == 0)
                MessageBox.Show("Изделие должно из чего нибудь состоять");
            else
            {
                _item.Title = textBox1.Text;
                using var db = new DiplomContext();
                using var db1 = new DiplomContext();
                using var db2 = new DiplomContext();
                db.Update(_item);
                db.SaveChanges();
                var itemsForDelete = db1.ItemItems.Where(x => x.ParentItemId == _item.Id);
                db1.RemoveRange(itemsForDelete);
                db1.SaveChanges();
                foreach (ItemItem itemItem in listBox2.Items)
                {
                    db.Add(itemItem);
                    db.SaveChanges();
                }

                var resourcesForDelete = db2.ItemResources.Where(x => x.ItemId == _item.Id);
                db2.RemoveRange(resourcesForDelete);
                db2.SaveChanges();
                foreach (ItemResource itemResource in listBox4.Items)
                {
                    itemResource.Resource = null;
                    itemResource.ItemId = _item.Id;
                    db.Add(itemResource);
                    db.SaveChanges();
                }

                Dispose();
            }
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            var createResourceForm = new CreateResourceForm();
            createResourceForm.ShowDialog();
            listBox3.Items.Clear();
            using var db = new DiplomContext();
            var itemResources = listBox4.Items.Cast<ItemResource>().ToList();
            var resources =
                db.Resources.Where(re => !itemResources.Select(itemResource => itemResource.Id).Contains(re.Id));
            listBox3.Items.AddRange(resources.ToArray());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}