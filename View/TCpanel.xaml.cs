using Mini_Total_Commander.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Mini_Total_Commander.View
{
    public partial class TCpanel : UserControl
    {
        #region konstruktory
        public TCpanel()
        {
            this.DataContext = new TCpanelVM();
            InitializeComponent();
        }
        #endregion
    }
}
