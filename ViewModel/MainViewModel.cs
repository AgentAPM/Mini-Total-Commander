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
        #region dane paneli
        PanelViewModel panelL;
        PanelViewModel panelR;
        public PanelViewModel PanelL => panelL;
        public PanelViewModel PanelR => panelR;
        CopyService copyService;
        public CopyService Model
        {
            get { if (copyService == null) copyService = new CopyService(); return copyService; }
            set { copyService = value; }
        }
        FileBrowser fileBrowser;
        public FileBrowser Validation
        {
            get { if (fileBrowser == null) fileBrowser = new FileBrowser(); return fileBrowser; }
            set { fileBrowser = value; }
        }
        #endregion
        #region komendy
        RelayCommand copyL;
        RelayCommand copyR;
        public RelayCommand CopyL
        {
            get
            {
                if (copyL == null)
                    copyL = new RelayCommand(
                                            arg => { Copy(PathR, PathL); }, 
                                            arg => true
                                            );
                return copyL;
            }
        }
        public RelayCommand CopyR
        {
            get
            {
                if (copyR == null)
                    copyR = new RelayCommand(
                                            arg => { Copy(PathL, PathR); }, 
                                            arg => true
                                            );
                return copyR;
            }
        }

        private void Copy(string from, string to)
        {
            if (Validation.IsDir(to))
            {
                if (Validation.IsFile(from))
                {
                    copyService.CopyFile(from, to);
                }
                else if (Validation.IsDir(from))
                {
                    var dialog = MessageBox.Show($"Przekopiować folder wraz z całą jego zawartością?", "Uwaga", MessageBoxButton.YesNoCancel);
                    if (dialog != MessageBoxResult.Cancel) 
                        copyService.CopyDirectory(from, to, dialog == MessageBoxResult.Yes);
                }
                    
            }
        }
        #endregion

        public MainViewModel()
        {
            panelL = new PanelViewModel();
            panelR = new PanelViewModel();
        }
    }
}
