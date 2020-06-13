using System.Windows;
using AutoScaleUI.Sample.Views;

namespace AutoScaleUI.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OriginalButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new OriginalWindow());
        }

        private void ViewboxButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new ViewboxWindow());
        }

        private void AutoScaleButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new AutoScaleWindow());
        }

        private void AutoScaleWithBaseSizeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new AutoScaleWithBaseSizeWindow());
        }


        private void ShowWindow(Window window)
        {
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
    }
}
