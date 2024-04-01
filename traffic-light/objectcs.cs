namespace trafficLight;

public enum ColorTrafficLight
{
    GREEN = 10,
    YELLOW = 14,
    RED = 12
}

public class TrafficLight
{
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
        lock(_lock)
        {
            _status = newColor;
            Console.ForegroundColor = (ConsoleColor)_status;
            Console.WriteLine($"{DateTime.Now.TimeOfDay} :: {_name} {_status}");
        }
    }

    public void Start(Dictionary<ColorTrafficLight, int> dictStatusTimer)
    {
        _running = true;
        
        while(_running)
        {
            foreach(var item in dictStatusTimer)
            {
                ChangeStatus(item.Key);
                Thread.Sleep(item.Value);
            }
        }
    }

    public void Stop()
    {
        _running = false;
    }
}

public class TrafficCrossing
{
    private TrafficLight _semaphoreA;
    private TrafficLight _semaphoreB;
    private Thread? _threadA;
    private Thread? _threadB; 
    public TrafficCrossing(TrafficLight semaphoreA, TrafficLight semaphoreB)
    {
        _semaphoreA = semaphoreA;
        _semaphoreB = semaphoreB;
    }

    public void Start()
    {
        _threadA = new Thread(() =>
        {
            _semaphoreA.Start(new Dictionary<ColorTrafficLight, int> 
            {
                { ColorTrafficLight.GREEN, 3000 },
                { ColorTrafficLight.YELLOW, 2000 },
                { ColorTrafficLight.RED, 5000 }
            });
        });
        
        Thread.Sleep(10);

        _threadB = new Thread(() =>
        {
            _semaphoreB.Start(new Dictionary<ColorTrafficLight, int> 
            {
                { ColorTrafficLight.RED, 5000 },
                { ColorTrafficLight.GREEN, 3000 },
                { ColorTrafficLight.YELLOW, 2000 }
            });
        });

        _threadA.Start();
        _threadB.Start();
    }

    public void Stop()
    {
        _semaphoreA.Stop();
        _semaphoreB.Stop();

        _threadA?.Join();
        _threadB?.Join();
    }
}
