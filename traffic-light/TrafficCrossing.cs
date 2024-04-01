namespace traffic_light
{
    public class TrafficCrossing
    {
        private TrafficLight _semaphoreA;
        private TrafficLight _semaphoreB;
        public TrafficCrossing(TrafficLight semaphoreA, TrafficLight semaphoreB)
        {
            _semaphoreA = semaphoreA;
            _semaphoreB = semaphoreB;
        }

        public void Start()
        {
            _semaphoreA.Start(new Dictionary<ColorTrafficLight, int>
            {
                { ColorTrafficLight.GREEN, 3000 },
                { ColorTrafficLight.YELLOW, 2000 },
                { ColorTrafficLight.RED, 5000 }
            });

            _semaphoreB.Start(new Dictionary<ColorTrafficLight, int>
            {
                { ColorTrafficLight.RED, 5000 },
                { ColorTrafficLight.GREEN, 3000 },
                { ColorTrafficLight.YELLOW, 2000 }
            });
        }

        public void Stop()
        {
            _semaphoreA.Stop();
            _semaphoreB.Stop();
        }
    }
}
