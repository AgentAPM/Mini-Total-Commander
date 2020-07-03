using Mini_Total_Commander.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mini_Total_Commander.ViewModel
{
    public class TCpanelVM : BaseClass.ViewModelBase
    {
        #region pola prywatne
        FileBrowser Model = new FileBrowser();

        private List<string> availableDrives;
        private List<string> directoryContent;
        private string currentDrive;
        private string currentPath;
        private string currentContent;
        const string GoBack = "..";
        const string DirectoryPrefix = "<d> ";
        #endregion
        #region własności publiczne
        public List<string> AvailableDrives
        {
            get
            {
                availableDrives = Model.GetDrives();
                onPropertyChanged(nameof(AvailableDrives));
                return availableDrives;
            }
        }
        private void UpdateDirectoryContent()
        {
                directoryContent = new List<string>();
                directoryContent.Add(GoBack);
                var Dirs = Model.GetDirectories(currentPath);
                for (int i = Dirs.Count; i-- > 0; Dirs[i] = DirectoryPrefix + Dirs[i]);
                directoryContent.Concat(Dirs);
                var Files = Model.GetFiles(currentPath);
                for (int i = Files.Count; i-- > 0; Files[i] = DirectoryPrefix + Files[i]);
                directoryContent.Concat(Files);
        }
        public List<string> DirectoryContent
        {
            get
            {
                return directoryContent;
            }
        }
        public string CurrentDrive
        {
            get { return currentDrive; }
            set
            {
                currentDrive = value;
                onPropertyChanged(nameof(CurrentDrive));
            }
        }
        public string CurrentPath
        {
            get { return currentPath; }
            set
            {
                currentPath = value;
                onPropertyChanged(nameof(CurrentPath));
            }
        }
        public string CurrentContent    // Selected file or directory
        {
            get { return currentContent; }
            set
            {
                currentContent = value;
                onPropertyChanged(nameof(CurrentContent));
            }
        }
        public void LoadDrive()
        {
            CurrentPath = currentDrive;
            var t = CurrentContent;
        }


        public string TestProperty
        {
            get { return "test"; }
        }
        #endregion

    }
}
