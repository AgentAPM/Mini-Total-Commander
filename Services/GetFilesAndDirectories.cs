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
        public static string[] GetFiles(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            string[] output = new string[files.Length];
            int i = 0; foreach (var f in files)
                output[i++] = f.Name;
            return output;
        }
        public static string[] GetDirectories(string path)
        {
            DirectoryInfo[] dirs = new DirectoryInfo(path).GetDirectories();
            string[] output = new string[dirs.Length];
            int i = 0; foreach (var d in dirs)
                output[i++] = d.Name;
            return output;
        }
        public static string[] GetDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            string[] output = new string[drives.Length];
            int i = 0; foreach (var d in drives)
                output[i++] = d.Name;
            return output;
        }
        public static string ChangeDirectory(string path)
        {
            if (path == null || path == "") throw (new Exception("Null or empty source"));
            try
            {
                DirectoryInfo destination = new DirectoryInfo(path);
                return destination.FullName; //Ensure the path is valid and simple
            }
            catch (Exception) { throw; }
        }
        public static string ChangeDirectory(string source, string enter)
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
        public static bool IsDir(string path)
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
        public static bool IsFile(string path)
        {
            return File.Exists(path);
        }
    }
}
