using System.IO;

namespace UsedDiskSpace.Model
{
    public class DriveItem : FileSystemItem
    {
        public string Name { get; }
        public DriveItem(string path) : base(path)
        {
            DriveInfo driveInfo = new DriveInfo(path);
            Name = driveInfo.Name;
        }
    }
}
