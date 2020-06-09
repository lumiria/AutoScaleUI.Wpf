using System.Windows;

namespace AutoScaleUI.Sample.Views
{
    /// <summary>
    /// ViewboxWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ViewboxWindow : Window
    {
        public ViewboxWindow()
        {
            InitializeComponent();
        }

        private void ChildContent_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
