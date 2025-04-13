using System.Windows;
using KeyListener.Services;
using KeyListener.ViewModels;

namespace KeyListener
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
