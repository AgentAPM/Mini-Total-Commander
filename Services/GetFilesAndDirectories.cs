using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniTotalCommander.Services
{
    class FileBrowser
    {
        public List<string> GetFiles(string path)
        {
            List<string> output = new List<string>();
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            foreach (var f in files)
                output.Add(f.Name);
            return output;
        }
        public List<string> GetDirectories(string path)
        {
            List<string> output = new List<string>();
            DirectoryInfo[] dirs = new DirectoryInfo(path).GetDirectories();
            foreach (var d in dirs)
                output.Add(d.Name);
            return output;
        }
        public List<string> GetDrives()
        {
            List<string> output = new List<string>();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var dr in drives)
                output.Add(dr.Name);
            return output;
        }
        public string ChangeDirectory(string path)
        {
            if (path == null || path == "") throw (new Exception("Null or empty source"));
            try
            {
                DirectoryInfo destination = new DirectoryInfo(path);
                return destination.FullName; //Ensure the path is valid and simple
            }
            catch (Exception) { throw; }
        }
        public string ChangeDirectory(string source, string enter)
        {
            if (source == null || source == "") throw (new Exception("Null or empty source"));
            if (enter == null || enter == "") throw (new Exception("Null or empty enter"));
            try
            {
                DirectoryInfo destination = new DirectoryInfo(source + "\\" + enter);
                return destination.FullName; //Ensure the path is valid and simple
            }
            catch (Exception) { throw; }
        }
        public bool IsDir(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.GetDirectories(path);
                    Directory.GetFiles(path);
                }
                catch (Exception) { return false; }
                return true;
            }
            else return false;
        }
        public bool IsFile(string path)
        {
            return File.Exists(path);
        }
    }
}
