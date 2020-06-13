using System.Windows;

namespace AutoScaleUI.Sample.Views
{
    /// <summary>
    /// OriginalWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class OriginalWindow : Window
    {
        public OriginalWindow()
        {
            InitializeComponent();
        }

        private void ChildContent_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
