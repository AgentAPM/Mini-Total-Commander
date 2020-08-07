using MiniTotalCommander.ViewModel;
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

namespace MiniTotalCommander
{
    public partial class PanelView : UserControl
    {
        public PanelView()
        {
            InitializeComponent();
        }
        #region własności zależne
        #region Path Label
            public string TotalPath
            {
                get { return (string)GetValue(TotalPathDP); }
                set { SetValue(TotalPathDP, value); }
            }
            public static DependencyProperty TotalPathDP = DependencyProperty.Register
                ("TotalPath", typeof(string), typeof(PanelView), new PropertyMetadata());
        #endregion
        #region Drive Select
            public string[] DrivesList
            {
                get { return (string[])GetValue(DrivesListDP); }
                set { SetValue(DrivesListDP, value); }
            }
            public static DependencyProperty DrivesListDP = DependencyProperty.Register
                ("DrivesList", typeof(string[]), typeof(PanelView), new PropertyMetadata());

            public int SelectedDriveIndex
            {
                get { return (int)GetValue(SelectedDriveIndexDP); }
                set { SetValue(SelectedDriveIndexDP, value); }
            }
            public static DependencyProperty SelectedDriveIndexDP = DependencyProperty.Register
                ("SelectedDriveIndex", typeof(int), typeof(PanelView), new PropertyMetadata());

            public ICommand DriveListOpen
            {
                get { return (ICommand)GetValue(DriveListOpenDP); }
                set { SetValue(DriveListOpenDP, value); }
            }
            public static readonly DependencyProperty DriveListOpenDP = DependencyProperty.Register
                ("DriveListOpen", typeof(ICommand), typeof(PanelView), new PropertyMetadata());


            private void CallDrivesOpen(object sender, EventArgs e)
            {
                RaiseEvent(new RoutedEventArgs(PanelView.DrivesOpenRE));
            }
            public event RoutedEventHandler DrivesOpen
            {
                add { AddHandler(DrivesOpenRE, value); }
                remove { RemoveHandler(DrivesOpenRE, value); }
            }
            public static readonly RoutedEvent DrivesOpenRE =
            EventManager.RegisterRoutedEvent(nameof(DrivesOpen),
                         RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                         typeof(PanelView));
        #endregion
        #region Directory View
            public IEnumerable<object> ItemsList
            {
                get { return (IEnumerable<object>)GetValue(ItemsListDP); }
                set { SetValue(ItemsListDP, value); }
            }
            public static DependencyProperty ItemsListDP = DependencyProperty.Register
                ("ItemsList", typeof(IEnumerable<object>), typeof(PanelView), new PropertyMetadata());


            public int SelectedItemIndex
            {
                get { return (int)GetValue(SelectedItemIndexDP); }
                set { SetValue(SelectedItemIndexDP, value); }
            }
            public static DependencyProperty SelectedItemIndexDP = DependencyProperty.Register
                ("SelectedItemIndex", typeof(int), typeof(PanelView), new PropertyMetadata());


            private void CallDoubleClickItem(object sender, MouseButtonEventArgs e)
            {
                RaiseEvent(new RoutedEventArgs(PanelView.DoubleClickItemRE));
            }
            public event RoutedEventHandler DoubleClickItem
            {
                add { AddHandler(DoubleClickItemRE, value); }
                remove { RemoveHandler(DoubleClickItemRE, value); }
            }
            public static readonly RoutedEvent DoubleClickItemRE =
            EventManager.RegisterRoutedEvent(nameof(DoubleClickItem),
                         RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                         typeof(PanelView));
        #endregion

        #endregion

    }
}
