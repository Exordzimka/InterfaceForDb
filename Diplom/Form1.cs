using System;
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
            if(listBox1.SelectedItem!=null)
                AddElementsInTree(listBox1.SelectedIndex);
        }

        private void AddElementsInTree(int index)
        {
            using var db = new DiplomContext();
            using var db1 = new DiplomContext();
            using var db2 = new DiplomContext();
            using var db3 = new DiplomContext();
            treeView1.Nodes.Clear();
            var item = (Item) listBox1.Items[index];
            var itemPartitions = db.ItemPartitions.Where(partition => partition.ItemId == item.Id);
            foreach (var itemPartition in itemPartitions)
            {
                itemPartition.Partition = db1.Partitions.FirstOrDefault(partition1 => partition1.Id == itemPartition.PartitionId);
                treeView1.Nodes.Add(new TreeNode(itemPartition.Partition.Title));

                var itemResources =
                    db2.ItemResources.Where(itemResource => itemResource.ItemId == item.Id);
                foreach (var itemResource in itemResources)
                {
                    var resources = db3.Resources.Where(resource => resource.PartitionId == itemPartition.PartitionId);
                    foreach (var resource in resources)
                    {
                        if(itemResource.ResourceId == resource.Id)
                            treeView1.Nodes[^1].Nodes.Add($"{resource.Title} {itemResource.Count} шт.");
                    }
                }
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
            if(listBox1.SelectedIndices.Count==0) return;
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
    }
}