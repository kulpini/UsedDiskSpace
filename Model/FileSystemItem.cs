namespace UsedDiskSpace.Model
{
    public abstract class FileSystemItem
    {
        public string FullPath { get; set; }
        private string Name { get; set; }
        public FileSystemItem(string path)
        {
            FullPath = path;
        }
    }
}
