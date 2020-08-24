using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MiniTotalCommander.Model;
using MiniTotalCommander.Services;
using MiniTotalCommander.ViewModel.Base;

namespace MiniTotalCommander.ViewModel
{
    class PanelViewModel : ViewModelBase
    {
        #region modele
        DirContent currentdir;

        #endregion
        #region elementy widoku
        public string TotalPath {
            get {
                return currentdir.TotalPath;
            }
            set {
                currentdir.TotalPath = value;
                onPropertyChanged(nameof(TotalPath));
                onPropertyChanged(nameof(ItemList));
                //ItemList should be updated together with the path
            }
        }

        public string[] ItemList {
            get {
                return currentdir.StringArray;
            }
        }
        private int itemindex;
        public int SelectedItemIndex
        {
            get { return itemindex; }
            set {
                itemindex = value;
                onPropertyChanged(nameof(SelectedItemIndex));
            }
        }
        public string SelectedItem { get { return currentdir.GetAt(SelectedItemIndex); } }
        private RelayCommand doubleclickitemRC;
        public RelayCommand DoubleClickItem {
            get {
                if (doubleclickitemRC == null)
                    doubleclickitemRC = new RelayCommand(arg => { TotalPath += "\\" + currentdir.GetAt(SelectedItemIndex); },
                                                            arg => currentdir.IsDir(SelectedItemIndex));
                return doubleclickitemRC;
            }
        }


        string[] drivelist;
        public string[] DrivesList {
            get { return drivelist; }
            private set {
                drivelist = value;
                onPropertyChanged(nameof(DrivesList));
            }
        }
        private int driveindex;
        public int SelectedDriveIndex {
            get { return driveindex; }
            set {
                driveindex = value;
                onPropertyChanged(nameof(SelectedDriveIndex));
            }
        }
        string lastselecteddrive="";
        public string SelectedDrive
        {
            get {
                try {
                    string trySD = DrivesList[SelectedDriveIndex];
                    lastselecteddrive = trySD;
                } catch (Exception) { }
                return lastselecteddrive;
            }
        }


        private RelayCommand driveboxopenedRC;
        public RelayCommand DriveBoxOpened {
            get {
                if (driveboxopenedRC == null)
                    driveboxopenedRC = new RelayCommand(arg => { LoadAvailableDrives(); }, arg => true);
                return driveboxopenedRC; 
            }
        }

        private RelayCommand driveselectionchangedRC;
        public RelayCommand DriveSelectionChanged {
            get {
                if (driveselectionchangedRC == null)
                    driveselectionchangedRC = new RelayCommand(arg => { TotalPath = SelectedDrive; }, arg => true);
                return driveselectionchangedRC; 
            }
        }
        #endregion
        #region metody
        public PanelViewModel()
        {
            currentdir = new DirContent(null);
        }
        public void LoadAvailableDrives()
        {
            SelectedDriveIndex = -1;
            DrivesList = FileBrowser.GetDrives();
        }

        #endregion
        #region własności
        public bool Valid { get { return currentdir.Valid; } }
        public bool Copiable {
            get {
                return (  currentdir.IsFile(SelectedItemIndex)  )
                    || (  (currentdir.IsDir(SelectedItemIndex))
                          && (currentdir.CanGoBack || !(SelectedItemIndex == 0)));
                //Selected item is copiable if and only if it's a file, or a directory excluding GoBack
            }
        }
        public bool IsDir => currentdir.IsDir(SelectedItemIndex);
        public bool IsFile => currentdir.IsFile(SelectedItemIndex);
        #endregion
    }
}
