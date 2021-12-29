using System;
using System.IO;
using System.Windows.Forms;
using UsedDiskSpace.Model;

namespace UsedDiskSpace
{
    public class Presenter
    {
        private readonly IMainForm view;
        private FolderItems folderItems;
        public Presenter(IMainForm mainForm)
        {
            view = mainForm;

            view.LoadData += new EventHandler(View_LoadData);
            view.FolderExpand += new EventHandler(View_FolderExpand);
            view.FolderSelect += new EventHandler(View_FolderSelect);
            view.ListViewColumnClick += new EventHandler(View_ListColumnClick);
        }

        private void View_ListColumnClick(object sender, EventArgs e)
        {
            ListViewSorter listViewSorter = view.ListViewSorter;
            if ((e as ColumnClickEventArgs).Column == listViewSorter.Column)
            {
                if (listViewSorter.Order == SortOrder.Ascending)
                {
                    listViewSorter.Order = SortOrder.Descending;
                }
                else
                {
                    listViewSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                listViewSorter.Column = (e as ColumnClickEventArgs).Column;
                listViewSorter.Order = SortOrder.Ascending;
            }
        }

        private void View_FolderSelect(object sender, EventArgs e)
        {
            view.FolderItems.Clear();
            TreeNode folderNode = view.ActiveNode;
            string path = folderNode.FullPath;
            view.SetHeader(path);
            if (Directory.Exists(path))
            {
                folderItems = new FolderItems(path);
                foreach (var item in folderItems.folders)
                {
                    int imageIndex = (int)ItemImageIndex.Folder;
                    ListViewItem folderItem = new ListViewItem(item.Name, imageIndex);
                    folderItem.SubItems.Add($"{item.Size} КБ");
                    view.FolderItems.Add(folderItem);
                }
                foreach (var item in folderItems.files)
                {
                    int imageIndex = (int)ItemImageIndex.File;
                    ListViewItem folderItem = new ListViewItem(item.Name, imageIndex);
                    folderItem.SubItems.Add($"{item.Size} КБ");
                    view.FolderItems.Add(folderItem);

                }
            }
        }
        private void View_FolderExpand(object sender, EventArgs e)
        {
            TreeNode expandedNode = view.ActiveNode;
            expandedNode.Nodes.Clear();
            string expandedFolderPath = expandedNode.FullPath;
            ListFolders(expandedNode, expandedFolderPath);
        }
        private void View_LoadData(object sender, EventArgs e)
        {
            Drives drives = Drives.Create();
            foreach (var drive in drives)
            {
                int imageIndex = (int)ItemImageIndex.Drive;
                TreeNode driveNode = new TreeNode(drive.Name, imageIndex, imageIndex);
                view.Nodes.Add(driveNode);
                ListFolders(driveNode, drive.FullPath);
            }
        }
        private void ListFolders(TreeNode parentNode, string folderPath)
        {
            Folders folders = Folders.Create(folderPath);
            foreach (var folder in folders)
            {
                int imageIndex = (int)ItemImageIndex.Folder;
                TreeNode folderNode = new TreeNode(folder.Name, imageIndex, imageIndex);
                parentNode.Nodes.Add(folderNode);
                ListSubFolders(folderNode, folder.FullPath);
            }
        }

        private void ListSubFolders(TreeNode parentNode, string folderPath)
        {
            Folders folders = Folders.Create(folderPath);
            foreach (var folder in folders)
            {
                int imageIndex = (int)ItemImageIndex.Folder;
                TreeNode folderNode = new TreeNode(folder.Name, imageIndex, imageIndex);
                parentNode.Nodes.Add(folderNode);
            }
        }
    }
}
