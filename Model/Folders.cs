using System.Collections.Generic;
using System.IO;

namespace UsedDiskSpace.Model
{
    public class Folders:List<FolderItem>
    {
        public static Folders Create(string path)
        {
            Folders folders = new Folders();
            foreach (var folder in Directory.GetDirectories(path))
            {
                try
                {
                    var files = Directory.GetDirectories(folder);
                    folders.Add(new FolderItem(folder));
                }
                catch { }
            }
            return folders;
        }
    }
}
