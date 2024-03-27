namespace Analogy.LogViewer.LiteDB.Forms
{
    partial class TableSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeView1 = new System.Windows.Forms.TreeView();
            btnLoad = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            treeView1.Location = new System.Drawing.Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new System.Drawing.Size(321, 450);
            treeView1.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Location = new System.Drawing.Point(337, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new System.Drawing.Size(181, 52);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // TableSelectionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(btnLoad);
            Controls.Add(treeView1);
            Name = "TableSelectionForm";
            Text = "TableSelectionForm";
            Load += TableSelectionForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnLoad;
    }
}