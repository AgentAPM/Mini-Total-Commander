using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MiniTotalCommander.Services;
using MiniTotalCommander.ViewModel.Base;

namespace MiniTotalCommander.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        #region modele widoku
        private PanelViewModel panelLvm;
        public PanelViewModel PanelLvm { get { return panelLvm; } }
        private PanelViewModel panelRvm;
        public PanelViewModel PanelRvm { get { return panelRvm; } }

        #endregion
        #region elementy widoku
        private RelayCommand copyLrc;
        public RelayCommand CopyL {
            get {
                if (copyLrc == null)
                    copyLrc = new RelayCommand( arg => { Copy(panelRvm, panelLvm); }, 
                                                arg => { return panelLvm.Valid && panelRvm.Valid; });
                return copyLrc;
            }
        }
        private RelayCommand copyRrc;
        public RelayCommand CopyR {
            get {
                if (copyRrc == null)
                    copyRrc = new RelayCommand( arg => { Copy(panelLvm, panelRvm); }, 
                                                arg => { return panelLvm.Valid && panelRvm.Valid; });
                return copyLrc;
            }
        }
        #endregion

        #region metody
        private void Copy(PanelViewModel from, PanelViewModel to)
        {
            if (from.SelectedItem == null || from.SelectedItem == "" || !to.Valid)
                return;
            if (FileBrowser.IsDir(to.SelectedItem)) {
                if (FileBrowser.IsFile(from.SelectedItem)) {
                    CopyService.CopyFile(from.SelectedItem, to.TotalPath);
                }
                else if (FileBrowser.IsDir(from.SelectedItem)) {
                    var dialog = MessageBox.Show($"Przekopiować folder wraz z całą jego zawartością?", "Uwaga", MessageBoxButton.YesNoCancel);
                    if (dialog != MessageBoxResult.Cancel)
                    {
                        CopyService.CopyDirectory(from.SelectedItem, to.TotalPath, dialog == MessageBoxResult.Yes);
                        to.TotalPath = to.TotalPath; //Reload the panel
                    }
                }
            }
        }
        #endregion

        public MainViewModel()
        {
            panelLvm = new PanelViewModel();
            panelRvm = new PanelViewModel();
        }
    }
}
