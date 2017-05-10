using System.Windows;
using System.Windows.Input;

namespace picture_review_and_move
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e) => FocusManager.SetFocusedElement(MainElement, sender as FrameworkElement);
    }
}
