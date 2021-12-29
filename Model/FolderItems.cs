namespace UsedDiskSpace.Model
{
    public class FolderItems
    {
        public Folders folders;
        public Files files;
        public FolderItems(string path)
        {
            folders = Folders.Create(path);
            files = Files.Create(path);
        }
    }
}
