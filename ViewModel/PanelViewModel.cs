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
        private string totalpath;
        public string TotalPath {
            get {
                return totalpath;
            }
            set {
                totalpath = currentdir.TryChangeDir(value);
                onPropertyChanged(nameof(TotalPath));
                //ItemList should be updated together with the path
                itemlist = null;
                onPropertyChanged(nameof(ItemList));
            }
        }

        private ObservableCollection<string> itemlist;
        public ObservableCollection<string> ItemList {
            get {
                if (itemlist == null) 
                    itemlist = new ObservableCollection<string>(currentdir.StringArray);
                return itemlist;
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
                    doubleclickitemRC = new RelayCommand(arg => { EnterCurrentPath(); }, arg => true);
                return doubleclickitemRC;
            }
        }


        ObservableCollection<string> drivelist;
        public ObservableCollection<string> DrivesList {
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
        public string SelectedDrive
        {
            get { return DrivesList[SelectedDriveIndex]; }
        }


        private RelayCommand driveboxopenedRE;
        public RelayCommand DriveBoxOpened {
            get {
                if (driveboxopenedRE == null)
                    driveboxopenedRE = new RelayCommand(arg => { LoadAvailableDrives(); }, arg => true);
                return driveboxopenedRE; 
            }
        }

        private RelayCommand driveselectionchangedRE;
        public RelayCommand DriveSelectionChanged {
            get {
                if (driveselectionchangedRE == null)
                    driveselectionchangedRE = new RelayCommand(arg => { EnterDriveRoot(); }, arg => true);
                return driveboxopenedRE; 
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
            ObservableCollection<string> output = new ObservableCollection<string>();
            foreach (var x in FileBrowser.GetDrives())
                output.Add(x);
            DrivesList = output;
        }
        public void EnterDriveRoot() { 
            TotalPath = SelectedDrive;
        }
        public void EnterCurrentPath() 
        {
            TotalPath = FileBrowser.ChangeDirectory(TotalPath, currentdir.GetAt(SelectedItemIndex));
        }

        #endregion
        #region własności
        public bool Valid { get { return currentdir.Valid; } }
        #endregion
    }
}
