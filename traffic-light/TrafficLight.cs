using System.Threading;

namespace traffic_light
{
    public enum ColorTrafficLight
    {
        GREEN = 10,
        YELLOW = 14,
        RED = 12
    }
    public class TrafficLight
    {
        private CancellationTokenSource? _cancellationTokenSource;
        private readonly string _name;
        private bool _running;
        private ColorTrafficLight _status;
        private static readonly object _lock = new object();
        public TrafficLight(string name)
        {
            _name = name;
            _running = false;
        }

        public void ChangeStatus(ColorTrafficLight newColor)
        {
            lock (_lock)
            {
                _status = newColor;
                Console.ForegroundColor = (ConsoleColor)_status;
                Console.WriteLine($"{DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss")} :: {_name} {_status}");
            }
        }

        public void Start(Dictionary<ColorTrafficLight, int> dictStatusTimer)
        {
            _running = true;
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (_running)
                {
                    foreach (var item in dictStatusTimer)
                    {
                        ChangeStatus(item.Key);
                        Thread.Sleep(item.Value);
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _running = false;
            _cancellationTokenSource?.Cancel();
        }
    }
}
