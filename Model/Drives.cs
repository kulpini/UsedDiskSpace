using System.Collections.Generic;
using System.IO;

namespace UsedDiskSpace.Model
{
    public class Drives : List<DriveItem>
    {
        public static Drives Create()
        {
            Drives drivesList = new Drives();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                if (drive.IsReady)
                {
                    drivesList.Add(new DriveItem(drive.RootDirectory.FullName));
                }
            }
            return drivesList;
        }
    }
}
