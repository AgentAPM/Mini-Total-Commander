using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Total_Commander.View
{
    public partial class FileBrowser : UserControl
    {
        private ItemCollection drives { get { return drivePick.Items; } }
        private string currentDrive { get { return (string)drivePick.SelectedItem; } }
        string _path;
        private string path { 
            get { return _path; } 
            set { _path = value; dirPath.Content = _path; } 
        }
        private ItemCollection contents { get { return dirContent.Items; } }
        private int index { get { return dirContent.SelectedIndex; } set { dirContent.SelectedIndex = value; } }
        private string selectedItem { get { return (string)dirContent.SelectedItem; } }



        public FileBrowser()
        {
            InitializeComponent();

            LoadDrives();
            InitDrive();
        }
        public void LoadDrives()
        {
            drives.Clear();
            foreach (var drive in DriveInfo.GetDrives()) 
                drives.Add(drive.Name);
        }
        public void LoadDirectoryContent()
        {
            contents.Clear();
            contents.Add("..");
            string[] getDirectories, getFiles;
            try
            {
                getDirectories = Directory.GetDirectories(path);
                getFiles = Directory.GetFiles(path);
            } catch(Exception e) //If directory content can't be accessed, exit the directory
                { ExitDirectory(); return; }
            //Load the content to the table otherwise
            foreach (string name in getDirectories)
                contents.Add("<d> " + name.Substring(name.LastIndexOf('\\') + 1)); //Append directory name
            foreach (string name in getFiles)
                contents.Add(name.Substring(name.LastIndexOf('\\') + 1)); //Append file name
            index = -1;
        }
        private void ExitDirectory()
        {

            int secondLastSlash = path.Length - 2;  //Last character will be a backslash due to convention
            while (secondLastSlash >= 0 && path[secondLastSlash] != '\\') --secondLastSlash; //Find second to last slash character
            if (secondLastSlash == -1) return;     //If there's no second to last slash, we are in the root and can't go back
            string newpath = path.Substring(0, secondLastSlash+1);
            if (!Directory.Exists(newpath)) return;
            path = newpath;
            LoadDirectoryContent();
        }
        private void EnterDirectory(string newDir)
        {
            string newpath = String.Format(@"{0}{1}\",path,newDir);
            if (!Directory.Exists(newpath)) return;
            path = newpath;
            LoadDirectoryContent();
        }

        private void Event_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (selectedItem == "..")
                ExitDirectory();
            else if (selectedItem.Substring(0, 3) == "<d>")
                EnterDirectory(selectedItem.Substring(4));
        }
        private void InitDrive()
        {
            if (drivePick.SelectedIndex < 0) return;
            path = (string)drivePick.Items.GetItemAt(drivePick.SelectedIndex);
            LoadDirectoryContent();
        }
        private void InitDrive(int pos)
        {
            drivePick.SelectedIndex = pos;
            path = (string)drivePick.Items.GetItemAt(drivePick.SelectedIndex);
            LoadDirectoryContent();
        }

        private void ReloadDrives(object sender, EventArgs e)
        {
            LoadDrives();
        }

        private void NewDrivePicked(object sender, SelectionChangedEventArgs e)
        {
            InitDrive();
        }

        public string GetCurrentPath()
        {
            return path;
        }
        public string GetSelectedItem()
        {
            if (selectedItem == "..")
                return "";
            return selectedItem;
        }
    }
}
