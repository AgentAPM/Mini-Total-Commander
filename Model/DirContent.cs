using MiniTotalCommander.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.Model
{
    class DirContent
    {
        #region pola prywatne
        bool loaded;

        string currentpath;
        string[] subfiles;
        string[] subdirs;
        string[] stringlist;
        bool cangoback;

        const string goback = "..";
        const string dirprefix = "<d> ";
        const string fileprefix = "";
        #endregion
        #region metody
        public DirContent(string path)
        {
            ChangeDir(path);
        }
        public string GetAt(int index)
        {
            // index < 0 is outside range
            if (index < 0) return "";
            // index == 0 may now point to the go back
            if (cangoback)
                if (index == 0)
                    return "..";
                else
                    --index;
            // index == 0 now points to the first directory
            if (index < subdirs.Length)
                return subdirs[index];
            index -= subdirs.Length;
            // index == 0 now points to the first file
            if (index < subfiles.Length)
                return subfiles[index];
            index -= subfiles.Length;
            // index >= 0 is outside range
            return "";
        }

        public string ChangeDir(string path)
        {
            //  Take data from a path
            //  if problem occured, fill values empty
            if (path == null || path == "" || !Directory.Exists(path)) {
                loaded = false;
                cangoback = false;
                currentpath = "";
                subfiles = new string[0];
                subdirs = new string[0];
                stringlist = null;
            } else {
                try {
                    cangoback = Directory.GetParent(path) != null;
                    currentpath = FileBrowser.ChangeDirectory(path);
                    subfiles = FileBrowser.GetFiles(path);
                    subdirs = FileBrowser.GetDirectories(path);
                    stringlist = null;
                    loaded = true;
                } catch (Exception) {
                    ChangeDir(null);
                }
            }
            return currentpath;
        }
        public string TryChangeDir(string path)
        {
            //  Take data from a path
            //  if problem occured, keep the previous state
            if (path != null && path != "" || !Directory.Exists(path)) {
                try
                {
                    bool tryCGB = Directory.GetParent(path) != null;
                    string tryCP = FileBrowser.ChangeDirectory(path);
                    string[] trySF = FileBrowser.GetFiles(path);
                    string[] trySD = FileBrowser.GetDirectories(path);
                    //If a problem accured, the values won't be assigned
                    cangoback = tryCGB;
                    currentpath = tryCP;
                    subdirs = trySD;
                    subfiles = trySF;
                    loaded = true;
                } catch (Exception) {}
            }
            return currentpath;
        }
        public string[] BuildStringArray()
        {
            if (currentpath == null) return new string[0];
            int i = 0; string[] output;
            if (cangoback) {
                output = new string[1 + subdirs.Length + subfiles.Length];
                output[i++] = goback;
            } else {
                output = new string[subdirs.Length + subfiles.Length];
            }
            foreach (var d in subdirs)
                output[i++] = dirprefix + d;
            foreach (var f in subfiles)
                output[i++] = fileprefix + f;
            return output;
        }
        #endregion
        #region własności publiczne
        public bool Valid { get { return loaded; } }
        public string[] StringArray
        {
            get {
                if (stringlist == null)
                    stringlist = BuildStringArray();
                return stringlist;
            }
        }
        public string TotalPath
        {
            get { return currentpath; }
        }
        #endregion
    }
}
