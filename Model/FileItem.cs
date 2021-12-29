using System.IO;

namespace UsedDiskSpace.Model
{
    public class FileItem:FileSystemItem
    {
        public string Name
        {
            get { return Path.GetFileName(FullPath); }
        }
        public long Size { get; }
        public FileItem(string path) : base(path)
        {
            FileInfo fileInfo = new FileInfo(path);
            Size = fileInfo.Length / 1024;
        }
    }
}
