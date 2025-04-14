using System.Windows;
using KeyListener.ViewModels;
using KeyListener.Services;

namespace KeyListener.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new KeyboardListenerService());
        }
    }
}
