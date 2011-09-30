using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Runtime.InteropServices;

namespace PythonDotNetTest
{
    public partial class TestForm : Form
    {
        // Drag & drop row reordering related variables.
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

#if false
        // Include SetParent Reference
        [DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndParent);

        // Include hWnd of a selected window references
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
#endif

        public TestForm()
        {
            InitializeComponent();
            InitDataGridView();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            //AttachToMayaNativeWindow();
        }

#if false
        /**
            Based on
            http://www.techartblog.com/tutorial/integrating-dotnet-and-maya2012-ui-pt1
            Not used in sample. 
         */
        private void AttachToMayaNativeWindow()
        {
            //Set the name and Class of the Maya blank window
            string lpszParentClass = "QWidget";
            string lpszParentWindow = "Python.NET Maya Host Window";

            IntPtr ParenthWnd = new IntPtr(0);

            /* Attach the .Net Form into the Maya blank window */
            ParenthWnd = FindWindow(lpszParentClass, lpszParentWindow);
            SetParent(this.Handle, ParenthWnd);
        }
#endif

        public void InitDataGridView()
        {
            this.dataGridView.Columns.Add("Name", "Name");
            this.dataGridView.Columns.Add("TransX", "TransX");
            this.dataGridView.Columns.Add("TransY", "TransY");
            this.dataGridView.Columns.Add("TransZ", "TransZ");
        }

        public void AddRow(string strName, float fTransX, float fTransY, float fTransZ)
        {
            string[] row = new string[] { strName, fTransX.ToString(), fTransY.ToString(), fTransZ.ToString() };
            dataGridView.Rows.Add(row);
        }

        private void openFileDialogBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = "";
            openFileDialog.ShowHelp = false;
            openFileDialog.AutoUpgradeEnabled = true;
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();
        }

        private void TestForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string filePath in filePaths)
                {
                    MessageBox.Show(filePath);
                }
            }
        }

        /**
             Drag & drop reordering or rows.
             http://windowsclient.net/blogs/faqs/archive/2006/07/10/how-do-i-perform-drag-and-drop-reorder-of-rows.aspx
        */

        private void dataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item
                    DragDropEffects dropEffect = dataGridView.DoDragDrop(
                    dataGridView.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dataGridView.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;
                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                e.Y - (dragSize.Height / 2)),
                dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView.PointToClient(new Point(e.X, e.Y));
            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
            dataGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                if (rowIndexOfItemUnderMouseToDrop < dataGridView.RowCount && rowIndexOfItemUnderMouseToDrop >= 0)
                {
                    DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                    dataGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    dataGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                }
            }
        }
    }
}
