using System;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace UsedDiskSpace
{
    public interface IMainForm
    {
        TreeNode ActiveNode { get; }
        TreeNodeCollection Nodes { get; }
        ListViewSorter ListViewSorter { get; }
        ListViewItemCollection FolderItems { get; }
        void SetHeader(string header);
        event EventHandler LoadData;
        event EventHandler FolderSelect;
        event EventHandler FolderExpand;
        event EventHandler ListViewColumnClick;
    }
    public partial class MainForm : Form, IMainForm
    {
        private ListViewSorter listViewSorter;
        private TreeNode workingNode;
        public TreeNode ActiveNode
        {
            get { return workingNode; }
        }
        public TreeNodeCollection Nodes
        {
            get { return FolderTree.Nodes; }
        }
        public ListViewItemCollection FolderItems
        {
            get { return FileList.Items; }
        }
        public ListViewSorter ListViewSorter
        {
            get { return listViewSorter; }
        }

        public event EventHandler LoadData;
        public event EventHandler FolderSelect;
        public event EventHandler FolderExpand;
        public event EventHandler ListViewColumnClick;

        public MainForm()
        {
            InitializeComponent();
            listViewSorter = new ListViewSorter();
            FileList.ListViewItemSorter = listViewSorter;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData(this, EventArgs.Empty);
        }
        private void FolderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingNode = e.Node;
            FolderSelect(this, EventArgs.Empty);
        }

        private void FolderTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            workingNode = e.Node;
            FolderExpand(sender, e);
        }

        public void SetHeader(string header)
        {
            this.Text = header;
            PathLabel.Text = header;
        }

        private void FileList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 1)
                ListViewColumnClick(sender, e);
            FileList.Sort();
        }
    }
    public enum ItemImageIndex
    {
        Drive,
        Folder,
        File
    }

}

