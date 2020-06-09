using System.Windows;
using System.Windows.Controls;

namespace AutoScaleUI.Sample.Views
{
    /// <summary>
    /// ChildContent.xaml の相互作用ロジック
    /// </summary>
    public partial class ChildContent : UserControl
    {
        public ChildContent()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Close;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close?.Invoke(this, e);
        }
    }
}
