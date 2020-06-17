using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mini_Total_Commander.Model;
using Mini_Total_Commander.ViewModel.BaseClass;

namespace Mini_Total_Commander.ViewModel
{
    class TotalCommander : BaseClass.ViewModelBase
    {
        #region pola prywatne
        FileManager Model = new FileManager();
        TCpanelVM  leftVM;
        TCpanelVM rightVM;

        private RelayCommand copyR;
        private RelayCommand copyL;
        const string GoBack = "..";
        const string DirectoryPrefix = "<d> ";
        private string pathL;
        private string pathR;

        #endregion
        #region konstruktory
        public TotalCommander()
        {

        }
        #endregion
        #region Selection
        #endregion
        #region własności publiczne
        public TCpanelVM leftPanel
        {
            set { leftVM = value; onPropertyChanged(nameof(leftPanel)); }
            get { return leftVM; }
        }
        public TCpanelVM rightPanel
        {
            set { rightVM = value; onPropertyChanged(nameof(rightPanel)); }
            get { return rightVM; }
        }
        private void Copy(TCpanelVM from, TCpanelVM to)
        {

        }
        public RelayCommand CopyR
        {
            get
            {/*
                if (selectedL > 0 && selectedL < DcountL)
                    Model.CopyDirectory(String.Format(@"{0}\{1}",pathL,selnameL), pathR);*/
                Console.WriteLine("CopyR");
                return copyR;
            }
        }
        public RelayCommand CopyL
        {
            get
            {
                Console.WriteLine("CopyL");
                return copyL;
            }
        }
        public List<string> GetDirectoryContent(string path)
        {
            var subdirs = Model.GetDirectories(path);
            var files = Model.GetFiles(path);
            for (int i=subdirs.Count;i>0;)
                subdirs[--i] = DirectoryPrefix + subdirs[i];
            List<string> output = new List<string>(1+subdirs.Count+files.Count);
            output.Add(GoBack);
            output.Concat(subdirs).Concat(files);
            return output;
        }
        public string Changedir(string path, string position)
        {
            if (position.Equals(GoBack))
                return Model.ExitDirectory(path);
            else
                return Model.EnterDirectory(path, position.Substring(DirectoryPrefix.Length));  //Ignore the directory prefix from the name
        }
        #endregion
        #region Commands
        private RelayCommand fillDrives;
        #endregion
        #region List content
        public ICommand FillDrivesBox
        {
            get
            {
                if (fillDrives == null)
                {

                }

                return fillDrives;
            }
        }
        #endregion
    }
}
