using System.Collections.ObjectModel;
using System.Windows.Input;
using KeyListener.Interfaces;
using KeyListener.Models;
using KeyListener.Helpers;

namespace KeyListener.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IKeyBoardListener _keyboardService;

        public ObservableCollection<KeyPressStat> Stats { get; set; } = new();

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ShowStatsCommand { get; }

        public MainViewModel(IKeyBoardListener keyboardService)
        {
            _keyboardService = keyboardService;

            StartCommand = new RelayCommand(_ => _keyboardService.Start());
            StopCommand = new RelayCommand(_ => _keyboardService.Stop());
            ShowStatsCommand = new RelayCommand(_ => ShowStats());
        }

        private void ShowStats()
        {
            Stats.Clear();
            var data = _keyboardService.GetStats();

            foreach (var item in data)
            {
                Stats.Add(new KeyPressStat
                {
                    Key = item.Key,
                    Count = item.Value
                });
            }
        }
    }
}
