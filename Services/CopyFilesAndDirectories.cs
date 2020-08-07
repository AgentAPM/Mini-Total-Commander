using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace MiniTotalCommander.Services
{
    class CopyService
    {
        private event Action DirectoryContentInaccessible;
        private event Action<string> ItemAlreadyExists; 
        public static void CopyFile(string source, string destination)
        {
            File.Copy(source, destination);
        }
        public static void CopyDirectory(string source, string destination, bool recursive=true)
        {
            DirectoryInfo sourceInfo=null, destInfo=null;
            FileInfo[] filesToCopy=null; DirectoryInfo[] directoriesToCopy = null;
            try
            {
                sourceInfo = new DirectoryInfo(source);
                destInfo = Directory.CreateDirectory(destination + "\\" + sourceInfo.Name);
                filesToCopy = sourceInfo.GetFiles();
                if(recursive) directoriesToCopy = sourceInfo.GetDirectories();
            }
            catch (Exception) { }
            foreach (var f in sourceInfo.GetFiles())
                CopyFile(f.FullName, destination + "\\" + f.Name);
            if(recursive)
                foreach (var d in sourceInfo.GetDirectories())
                    CopyDirectory(d.FullName, destination + "\\" + d.Name);
        }
    }
}
