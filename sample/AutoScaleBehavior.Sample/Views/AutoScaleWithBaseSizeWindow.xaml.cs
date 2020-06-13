using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoScaleUI.Sample.Views
{
    /// <summary>
    /// AutoScaleWithBaseSizeWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AutoScaleWithBaseSizeWindow : Window
    {
        public AutoScaleWithBaseSizeWindow()
        {
            InitializeComponent();
        }

        private void ChildContent_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
