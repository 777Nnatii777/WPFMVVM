using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;
using KeyListener.Interfaces;
using KeyListener.Models;
using KeyListener.Helpers;
using KeyListener.Views;

namespace KeyListener.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IKeyBoardListener _keyboardService;
        private readonly Stopwatch _stopwatch = new();
        private DispatcherTimer? _timer;

        private string _elapsedTime = "00:00:00.000";
        public string ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyPressStat> Stats { get; set; } = new();

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ShowStatsCommand { get; }

        public MainViewModel(IKeyBoardListener keyboardService)
        {
            _keyboardService = keyboardService;

            StartCommand = new RelayCommand(_ => Start());
            StopCommand = new RelayCommand(_ => Stop());
            ShowStatsCommand = new RelayCommand(_ => ShowStats());
        }

        private void Start()
        {
            _keyboardService.Start();

            _stopwatch.Reset();
            _stopwatch.Start();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            _timer.Tick += (s, e) =>
            {
                ElapsedTime = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");
            };

            _timer.Start();
        }

        private void Stop()
        {
            _keyboardService.Stop();
            _stopwatch.Stop();
            _timer?.Stop();
        }

        private void ShowStats()
        {
            Stats.Clear();
            var data = _keyboardService.GetStats();
            double total = data.Values.Sum();

            foreach (var item in data)
            {
                Stats.Add(new KeyPressStat
                {
                    Key = item.Key,
                    Count = item.Value,
                    Percentage = Math.Round((item.Value / total) * 100, 2)
                });
            }

            var statsWindow = new StatsView(this);
            statsWindow.Show();
        }
    }
}
