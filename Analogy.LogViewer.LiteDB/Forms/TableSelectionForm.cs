using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.LiteDB.Forms
{
    public partial class TableSelectionForm : Form
    {
        private List<string> Names { get; set; }
        private Action<string> Loader { get; set; }
        public TableSelectionForm()
        {
            InitializeComponent();
        }

        public TableSelectionForm(List<string> names, Action<string> loader) : this()
        {
            Names = names;
            Loader = loader;
        }

        private void TableSelectionForm_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.AddRange(Names.Select(n => new TreeNode(n)).ToArray());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                Loader?.Invoke(treeView1.SelectedNode.Text);
            }
        }
    }
}