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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Total_Commander.View;
using Total_Commander.ViewModel;
using System.IO;
using System.Security.Cryptography;

namespace Total_Commander
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CopyFile(string from, string to)
        {
            if (!File.Exists(from) || !Directory.Exists(to))
                return;
            /*
            string filename = from.Substring(from.LastIndexOf('\\') + 1);
            string destination = String.Format(@"{0}\{1}", to, filename);
            File.Copy(from, destination);
            */
            File.Copy(from, String.Format(@"{0}\{1}", to, from.Substring(from.LastIndexOf('\\') + 1)));
        }
        private void CopyDirectory(string from, string to)
        {
            if (!Directory.Exists(from) || !Directory.Exists(to))
                return;
            
            string dirname = from.Substring(from.LastIndexOf('\\') + 1);
            string destination = String.Format(@"{0}\{1}", to, dirname);
            Directory.CreateDirectory(destination);
            string[] subdirectories, content;
            try {
                content = Directory.GetFiles(from);
                subdirectories = Directory.GetDirectories(from);
                }
            catch(Exception e) { return; }
            foreach (string subdirectory in subdirectories)
            {
                string subdirname = subdirectory.Substring(subdirectory.LastIndexOf('\\') + 1);
                string subdestination = String.Format(@"{0}\{1}", destination, subdirname);
                CopyDirectory(subdirectory, subdestination);
            }
            foreach(string file in content)
            {
                var filename = from.Substring(from.LastIndexOf('\\') + 1);
                string filedestination = String.Format(@"{0}\{1}", destination, filename);
                File.Copy(file, filedestination);
            }
        }

        private void CopyToRight(object sender, RoutedEventArgs e)
        {
            string source = PanelL.GetCurrentPath();
            string destination = PanelR.GetCurrentPath();
            string item = PanelL.GetSelectedItem();
            if (item == "") return;
            if (item.Substring(0, 3) == "<d>") 
                CopyDirectory(source + item.Substring(4), destination);
            else
                CopyFile(source + item, destination);
            PanelR.LoadDirectoryContent();
        }

        private void CopyToLeft(object sender, RoutedEventArgs e)
        {
            string source = PanelR.GetCurrentPath();
            string destination = PanelL.GetCurrentPath();
            string item = PanelR.GetSelectedItem();
            if (item == "") return;
            if (item.Substring(0, 3) == "<d>")
            {
                var dialog = MessageBox.Show($"Skopiować folder {item.Substring(4)} wraz z całą jego zawartością?", "Uwaga", MessageBoxButton.OKCancel);
                if (dialog == MessageBoxResult.Cancel)
                    return;
                CopyDirectory(source + item, destination);
            }
            else
                CopyFile(source + item, destination);
            PanelL.LoadDirectoryContent();
        }
    }
}
