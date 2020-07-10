using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Total_Commander.Model;
using Total_Commander.Model.Services;
using Total_Commander.ViewModel.BaseClass;

namespace Total_Commander.ViewModel
{
    class VMFileManager : BaseClass.ViewModelBase
    {
        #region Pola prywatne
        private string leftPath;
        private string rightPath;
        private RelayCommand CopyR;
        private RelayCommand CopyL;

        private string dir;
        private MTotalCommander Model;
        #endregion

        #region Konstruktor
        public VMFileManager()
        {
            Model = new MTotalCommander();
            Model.AddService(new GetDrivesFilesAndDirectories());
            Model.AddService(new BrowseDirectories());
            Model.AddService(new CopyFilesAndDirectories());
        }
        #endregion
        #region Własności publiczne
        public RelayCommand KopiujDoLewego
        {
            get
            {
                
                //Model.Copy(rightPath, leftPath);
                return CopyR;
            }
        }
        public RelayCommand KopiujDoPrawego
        {
            get
            {
                //Model.Copy(leftPath, rightPath);
                return CopyL;
            }
        }
        #endregion
        
        public string Dir
        {
            get => dir;
            set {
                dir = value;
                onPropertyChanged(nameof(dir));
            }
        }
        public string[] Contents
        {
            get => Directory.GetFiles(dir);
        } 
    }
}
