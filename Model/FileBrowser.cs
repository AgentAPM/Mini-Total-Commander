using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mini_Total_Commander.Model
{
    class FileBrowser
    {
        #region Pola prywatne

        #endregion
        #region Konstruktory
        public FileBrowser()
        {

        }

        #endregion
        #region Własności publiczne

        public string EnterDirectory(string path, string dirname)
        {
            string newdir = String.Format(@"{0}\{1}", path, dirname);
            if (Directory.Exists(newdir))
                return newdir;
            else
                return path;
        }
        public string ExitDirectory(string path)
        {
            int newlen = path.LastIndexOf('\\') - 1;
            string newdir = path.Substring(0, newlen);
            if (Directory.Exists(newdir))
                return newdir;
            else
                return path;
        }

        public List<string> GetDrives()
        {
            List<string> output = new List<string>();
            output.Add("GetDrives");
            /*
            var content = DriveInfo.GetDrives();
            foreach (var drive in content)
                output.Add(drive.Name);
            *//*
            foreach (var drive in DriveInfo.GetDrives())
                output.Add(drive.Name);                                 //Append drive name
                */
            return output;
        }
        public List<string> GetDirectories(string path)
        {
            List<string> output = new List<string>();
            foreach (string name in Directory.GetDirectories(path))
                output.Add(name.Substring(name.LastIndexOf('\\') + 1)); //Append directory name
            return output;
        }
        public List<string> GetFiles(string path)
        {
            List<string> output = new List<string>();
            foreach (string name in Directory.GetFiles(path))
                output.Add(name.Substring(name.LastIndexOf('\\') + 1)); //Append file name
            return output;
        }
        #endregion
    }
}
