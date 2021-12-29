using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UsedDiskSpace.Model
{
    public class FolderItem : FileSystemItem
    {
        public string Name
        {
            get { return FullPath.Substring(FullPath.LastIndexOf('\\') + 1); }
        }
        public long Size { get; }
        public FolderItem(string path) : base(path)
        {
            var task = Task.Run(() => GetSize(path));
            if (task.Wait(TimeSpan.FromMilliseconds(800)))
                Size = task.Result;
            else
                Size = 0;
        }
        private long GetSize(string path)
        {
            try
            {
                return Directory.GetFiles(path, "*", SearchOption.AllDirectories).Sum(n => n.Length);
            }
            catch
            {
                return 0;
            }
        }
    }
}
