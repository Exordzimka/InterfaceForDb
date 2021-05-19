using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using var db = new DiplomContext();
            var items = db.Items.ToList();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(items.ToArray());
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                AddElementsInTree((Item) listBox1.SelectedItem);
        }

        private void AddElementsInTree(Item item)
        {
            using var db = new DiplomContext();
            using var db1 = new DiplomContext();
            using var db2 = new DiplomContext();
            using var db3 = new DiplomContext();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(item.ToString());
            AddItem(item, treeView1.Nodes[^1]);
        }

        private void AddItem(Item item, TreeNode node)
        {
            using var db = new DiplomContext();
            using var db1 = new DiplomContext();
            var childrenItems = db.ItemItems.Where(itemItem => itemItem.ParentItemId == item.Id);
            var itemResources = db1.ItemResources.Where(resource => resource.ItemId == item.Id);
            foreach (var itemResource in itemResources)
                node.Nodes.Add(itemResource.ToString());
            foreach (var childrenItem in childrenItems)
            {
                using var dbConnection = new DiplomContext();
                node.Nodes.Add(childrenItem.ToString());
                var childItem = dbConnection.Items.FirstOrDefault(item1 => item1.Id == childrenItem.ChildItemId);
                AddItem(childItem, node.LastNode);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var createItemForm = new CreateItem();
            createItemForm.ShowDialog();
            using var db = new DiplomContext();
            var items = db.Items.ToList();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(items.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            using var db = new DiplomContext();
            db.Items.Remove((Item) listBox1.SelectedItem);
            db.SaveChanges();
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            treeView1.Nodes.Clear();
        }

        private void базаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbForm = new DbForm();
            dbForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count == 0) return;
            var item = (Item) listBox1.SelectedItem;
            var itemItems = new List<ItemItem>();
            var resourceItems = new List<ItemResource>();
            var resourceIdCountDictionary = new Dictionary<long, int>();
            using (var db = new DiplomContext())
            {
                itemItems.AddRange(db.ItemItems.Where(x => x.ParentItemId == item.Id));
            }
            foreach (var itemItem in itemItems)
            {
                using var db = new DiplomContext();
                foreach(var itemResource in db.ItemResources.Where(x => x.ItemId == itemItem.Id))
                {
                    if (resourceIdCountDictionary.ContainsKey(itemResource.Id))
                        resourceIdCountDictionary[itemResource.Id] += itemResource.Count;
                    else
                    {
                        resourceIdCountDictionary.Add(itemResource.Id, itemResource.Count);
                        resourceItems.Add(itemResource);
                    }
                }
            }

            using (var db = new DiplomContext())
            {
                foreach(var itemResource in db.ItemResources.Where(x => x.ItemId == item.Id))
                {
                    if (resourceIdCountDictionary.ContainsKey(itemResource.Id))
                        resourceIdCountDictionary[itemResource.Id] += itemResource.Count;
                    else
                    {
                        resourceIdCountDictionary.Add(itemResource.Id, itemResource.Count);
                        resourceItems.Add(itemResource);
                    }
                }
            }

            var resources = new List<Resource>();
            foreach (var itemResource in resourceItems)
            {
                using var db = new DiplomContext();
                resources.Add(db.Resources.FirstOrDefault(x => x.Id == itemResource.ResourceId));
                if (resources.Last().CountOnStore < resourceIdCountDictionary[resources.Last().Id])
                {
                    MessageBox.Show($"Не хватает {resources.Last().Title}");
                    return;
                }

                resources.Last().CountOnStore -= resourceIdCountDictionary[resources.Last().Id];
            }

            using var db1 = new DiplomContext();
            db1.UpdateRange(resources);
            db1.SaveChanges();
            MessageBox.Show("Изделие заказано");
        }

        private void складToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var storeForm = new StoreForm();
            storeForm.ShowDialog();
        }
    }
}