using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Total_Commander.Model.Services
{
    class BrowseDirectories : ListService
    {
        #region Metody
        private string EnterDirectory(string path, string dirname)
        {
            string newdir = String.Format(@"{0}\{1}", path, dirname);
            if (Directory.Exists(newdir))
                return newdir;
            else
                return path;
        }
        private string ExitDirectory(string path)
        {
            int newlen = path.LastIndexOf(@"\") - 1;
            string newdir = path.Substring(0, newlen);
            if (Directory.Exists(newdir))
                return newdir;
            else
                return path;
        }
        #endregion
        #region Execute
        public List<string> Execute(string name, string path, string dir)
        {
            if (name == "EnterDirectory")
            {
                List<string> output = new List<string>();
                output.Add(EnterDirectory(path, dir));
            }
            return null;
        }
        public List<string> Execute(string name, string path)
        {
            if (name == "ExitDirectory")
            {
                List<string> output = new List<string>();
                output.Add(ExitDirectory(path));
            }
            return null;
        }
        #endregion
    }
}
