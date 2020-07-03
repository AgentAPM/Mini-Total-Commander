using Mini_Total_Commander.ViewModel;
using Mini_Total_Commander.ViewModel.BaseClass;
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
        TCpanelVM viewModel;

        #region konstruktory
        public TCpanel()
        {
            InitializeComponent();
        }
        #endregion
        #region własności publiczne
        public string TotalPath {
            get { return "WIP"; }
            set { }
        }
        public TCpanelVM ViewModel
        {
            get { return (TCpanelVM)GetValue(getViewModel); }
            set { SetValue(getViewModel, value); }
        }

    #endregion
    public static readonly DependencyProperty getViewModel =
        DependencyProperty.Register("ViewModel", typeof(TCpanelVM), typeof(TCpanel), new PropertyMetadata(null));

        private void ChooseDrive(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Opened(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
