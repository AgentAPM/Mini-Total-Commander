using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mini_Total_Commander.Model
{
    class FileManager
    {
        #region Pola prywatne

        #endregion
        #region Konstruktory
        public FileManager()
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

        public void CopyFile(string from, string to)
        {
            if (!File.Exists(from) || !Directory.Exists(to))
                return;
            /*
            string filename = from.Substring(from.LastIndexOf('\\') + 1);
            string destination = String.Format(@"{0}\{1}", to, filename);
            File.Copy(from, destination);
            */
            File.Copy(from, String.Format(@"{0}\{1}", to, from.Substring(from.LastIndexOf('\\') + 1)));
        }
        public void CopyDirectory(string from, string to)
        {
            if (!Directory.Exists(from) || !Directory.Exists(to))
                return;
            /*
            string dirname = from.Substring(from.LastIndexOf('\\') + 1);
            string destination = String.Format(@"{0}\{1}", to, dirname);
            Directory.CreateDirectory(destination);
            var subdirectories = Directory.GetDirectories(from);
            var content = Directory.GetFiles(from);
            foreach (string subdirectory in subdirectories)
            {
                string subdirname = subdirectory.Substring(subdirectory.LastIndexOf('\\') + 1);
                string subdestination = String.Format(@"{0}\{1}", destination, subdirname);
                CopyDirectory(subdirectory, subdestination);
            }
            foreach(string file in content)
            {
                var filename = from.Substring(from.LastIndexOf('\\') + 1);
                string filedestination = String.Format(@"{0}\{1}", destination, filename);
                File.Copy(file, filedestination);
            }
            */
            string destination = String.Format(@"{0}\{1}", to, from.Substring(from.LastIndexOf('\\') + 1));
            Directory.CreateDirectory(destination);
            foreach (string subdirectory in Directory.GetDirectories(from))
                CopyDirectory(subdirectory, String.Format(@"{0}\{1}", destination, subdirectory.Substring(subdirectory.LastIndexOf('\\') + 1)));
            foreach(string file in Directory.GetFiles(from))
                CopyFile(file, destination);
        }
        public List<string> GetDrives()
        {
            List<string> output = new List<string>();
            /*
            var content = DriveInfo.GetDrives();
            foreach (var drive in content)
                output.Add(drive.Name);
            */
            foreach (var drive in DriveInfo.GetDrives())
                output.Add(drive.Name);                                 //Append drive name

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
