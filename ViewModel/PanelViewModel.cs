using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniTotalCommander.Services;
using MiniTotalCommander.ViewModel.Base;

namespace MiniTotalCommander.ViewModel
{
    class PanelViewModel : ViewModelBase
    {
        #region pola prywatne
        FileBrowser fileBrowser;

        List<string> itemslist;
        int selectedindex;
        List<string> driveslist;
        string totalpath;

        #endregion
        #region własności
        public PanelViewModel ViewModel
        {
            get { return this; }
        }
        public FileBrowser GetService
        {
            get
            {
                if (fileBrowser == null)
                    fileBrowser = new FileBrowser();
                return fileBrowser;
            }
        }
        #endregion
        #region elementy widoku
        public List<string> DrivesList { 
            get {
                return GetService.GetDrives();
            } 
        }
        public string SelectedDrive { get; }
        public string TotalPath {
            get { return totalpath; } 
            set {
                if (GetService.IsDir(value))
                {
                    totalpath = value;
                    onPropertyChanged(nameof(TotalPath));
                } 
            } 
        }
        public List<string> ItemsList {
            get { return itemslist; } 
            set { itemslist = value; onPropertyChanged(nameof(ItemsList)); } 
        }
        public int SelectedIndex { 
            get { return selectedindex; } 
            set { selectedindex = value; onPropertyChanged(nameof(SelectedIndex)); } 
        }
        public string SelectedItem { get; }
        public bool DirSelected { get; }
        #endregion
        #region metody
        public bool IsPathValid()
        {
            return TotalPath != null && TotalPath != "";
        }
        private void ChangeDir()
        {
            if(SelectedIndex > -1)
                TotalPath = GetService.ChangeDirectory(TotalPath,SelectedItem);  
        }
        private void ReloadDrives()
        {
            driveslist = GetService.GetDrives();
        }
        #endregion
        #region komendy
        public void DoubleClickItem()
        {
            GetService.ChangeDirectory(TotalPath, SelectedItem);
        }
            
        #endregion
    }
}
