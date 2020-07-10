using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Total_Commander.Model.Services
{
    class GetDrivesFilesAndDirectories : ListService
    {
        #region Metody
        public List<string> GetDrives()
        {
            var content = DriveInfo.GetDrives();
            List<string> output = new List<string>();
            foreach (var drive in content)
                output.Add(drive.Name);

            return output;
        }
        public List<string> GetDirectories(string path)
        {
            var content = Directory.GetDirectories(path);
            List<string> output = new List<string>();
            foreach (string name in content)
                output.Add(name.Substring(name.LastIndexOf('\\')+1));
            return output; ;
        }
        public List<string> GetFiles(string path)
        {
            var content = Directory.GetFiles(path);
            List<string> output = new List<string>();
            foreach (string name in content)
                output.Add(name.Substring(name.LastIndexOf('\\')+1));
            return output; ;
        }
        #endregion
        #region Execute
        public List<string> Execute(string name)
        {
            if (name == "GetDrives")
                return GetDrives();
            return null;
        }
        public List<string> Execute(string name, string path)
        {
            if (name == "GetDirectories")
                return GetDirectories(path);
            if (name == "GetFiles")
                return GetFiles(path);
            return null;
        }
        #endregion
    }
}
