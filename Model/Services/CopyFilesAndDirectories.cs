using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Total_Commander.Model.Services
{
    class CopyFilesAndDirectories : ListService
    {
        #region Metody
        private void CopyFile(string from, string to)
        {
            File.Copy(from, to);
        }
        private void CopyDirectory(string from, string to)
        {
            var subdirectories = Directory.GetDirectories(from);
            var content = Directory.GetFiles(from);
            foreach(string directory in subdirectories)
            {
                string dirname = directory.Substring(directory.LastIndexOf('\\')+1);
                Directory.CreateDirectory(String.Format(@"{0}\{1}", from, directory));
            }
        }
        #endregion
        #region Execute
        public List<string> Execute(string name, string from, string to)
        {
            if (name == "CopyDirectory")
                CopyDirectory(from, to);
            else if (name == "CopyFile")
                CopyFile(from, to);
            return null;
        }
        #endregion
    }
}
