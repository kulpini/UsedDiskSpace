using System.Collections.Generic;
using System.IO;

namespace UsedDiskSpace.Model
{
    public class Files : List<FileItem>
    {
        public static Files Create(string path)
        {
            Files files = new Files();
            foreach (string file in Directory.GetFiles(path))
            {
                files.Add(new FileItem(file));
            }
            return files;
        }
    }
}
