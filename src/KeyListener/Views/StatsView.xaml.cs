using System.Windows;
using KeyListener.ViewModels;

namespace KeyListener.Views
{
    public partial class StatsView : Window
    {
        public StatsView(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
