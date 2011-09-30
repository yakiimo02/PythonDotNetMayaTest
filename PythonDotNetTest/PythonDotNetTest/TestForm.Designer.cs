namespace PythonDotNetTest
{
    partial class TestForm
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
            this.makeSphereBtn = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.updateGeomInfoBtn = new System.Windows.Forms.Button();
            this.geomInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.makeGeometryGroupBox = new System.Windows.Forms.GroupBox();
            this.makeCubeBtn = new System.Windows.Forms.Button();
            this.openFileDialogBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.geomInfoGroupBox.SuspendLayout();
            this.makeGeometryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // makeSphereBtn
            // 
            this.makeSphereBtn.Location = new System.Drawing.Point(29, 32);
            this.makeSphereBtn.Name = "makeSphereBtn";
            this.makeSphereBtn.Size = new System.Drawing.Size(117, 39);
            this.makeSphereBtn.TabIndex = 0;
            this.makeSphereBtn.Text = "Make Sphere";
            this.makeSphereBtn.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(6, 72);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(475, 400);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDown);
            this.dataGridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseMove);
            this.dataGridView.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragOver);
            this.dataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            // 
            // updateGeomInfoBtn
            // 
            this.updateGeomInfoBtn.Location = new System.Drawing.Point(27, 18);
            this.updateGeomInfoBtn.Name = "updateGeomInfoBtn";
            this.updateGeomInfoBtn.Size = new System.Drawing.Size(201, 38);
            this.updateGeomInfoBtn.TabIndex = 2;
            this.updateGeomInfoBtn.Text = "Update Geometry Trans Info";
            this.updateGeomInfoBtn.UseVisualStyleBackColor = true;
            // 
            // geomInfoGroupBox
            // 
            this.geomInfoGroupBox.Controls.Add(this.label1);
            this.geomInfoGroupBox.Controls.Add(this.updateGeomInfoBtn);
            this.geomInfoGroupBox.Controls.Add(this.dataGridView);
            this.geomInfoGroupBox.Location = new System.Drawing.Point(13, 113);
            this.geomInfoGroupBox.Name = "geomInfoGroupBox";
            this.geomInfoGroupBox.Size = new System.Drawing.Size(487, 478);
            this.geomInfoGroupBox.TabIndex = 3;
            this.geomInfoGroupBox.TabStop = false;
            this.geomInfoGroupBox.Text = "Geometry Info";
            // 
            // makeGeometryGroupBox
            // 
            this.makeGeometryGroupBox.Controls.Add(this.makeCubeBtn);
            this.makeGeometryGroupBox.Controls.Add(this.makeSphereBtn);
            this.makeGeometryGroupBox.Location = new System.Drawing.Point(12, 7);
            this.makeGeometryGroupBox.Name = "makeGeometryGroupBox";
            this.makeGeometryGroupBox.Size = new System.Drawing.Size(299, 100);
            this.makeGeometryGroupBox.TabIndex = 4;
            this.makeGeometryGroupBox.TabStop = false;
            this.makeGeometryGroupBox.Text = "Make Geometry";
            // 
            // makeCubeBtn
            // 
            this.makeCubeBtn.Location = new System.Drawing.Point(152, 32);
            this.makeCubeBtn.Name = "makeCubeBtn";
            this.makeCubeBtn.Size = new System.Drawing.Size(119, 39);
            this.makeCubeBtn.TabIndex = 0;
            this.makeCubeBtn.Text = "Make Cube";
            this.makeCubeBtn.UseVisualStyleBackColor = true;
            // 
            // openFileDialogBtn
            // 
            this.openFileDialogBtn.Location = new System.Drawing.Point(349, 40);
            this.openFileDialogBtn.Name = "openFileDialogBtn";
            this.openFileDialogBtn.Size = new System.Drawing.Size(107, 36);
            this.openFileDialogBtn.TabIndex = 5;
            this.openFileDialogBtn.Text = "Open File Dialog";
            this.openFileDialogBtn.UseVisualStyleBackColor = true;
            this.openFileDialogBtn.Click += new System.EventHandler(this.openFileDialogBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Right-Click to Drag and Drop Reorder Rows";
            // 
            // TestForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 603);
            this.Controls.Add(this.openFileDialogBtn);
            this.Controls.Add(this.makeGeometryGroupBox);
            this.Controls.Add(this.geomInfoGroupBox);
            this.DoubleBuffered = true;
            this.Name = "TestForm";
            this.Text = "Python.NET Test Dialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TestForm_DragDrop);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.geomInfoGroupBox.ResumeLayout(false);
            this.geomInfoGroupBox.PerformLayout();
            this.makeGeometryGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button makeSphereBtn;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox geomInfoGroupBox;
        private System.Windows.Forms.GroupBox makeGeometryGroupBox;
        public System.Windows.Forms.Button makeCubeBtn;
        public System.Windows.Forms.Button updateGeomInfoBtn;
        private System.Windows.Forms.Button openFileDialogBtn;
        private System.Windows.Forms.Label label1;

    }
}