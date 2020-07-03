using Mini_Total_Commander.ViewModel;
using Mini_Total_Commander.ViewModel.BaseClass;
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

namespace Mini_Total_Commander
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var ViewModel = new TotalCommander();
            PanelL.DataContext = ViewModel.LeftPanelVM;
            PanelR.DataContext = ViewModel.RightPanelVM;
            
        }

    }
}
