using System.Windows;

namespace CardDesigner.UI
{
    public partial class WindowStyles : ResourceDictionary
    {
        public WindowStyles()
        {
            InitializeComponent();
        }

        private void OnCloseClick(object sender, RoutedEventArgs eventArgs)
        {
            Window window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.Close();
        }

        private void OnMinimizeClick(object sender, RoutedEventArgs eventArgs)
        {
            Window window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.WindowState = WindowState.Minimized;

        }

        private void OnMaximizeRestoreClick(object sender, RoutedEventArgs eventArgs)
        {
            Window window = (Window)((FrameworkElement)sender).TemplatedParent;

            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }
    }
}
