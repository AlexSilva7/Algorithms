using traffic_light;

var semaphoreA = new TrafficLight("Semaphore A");
var semaphoreB = new TrafficLight("Semaphore B");

var crossing = new TrafficCrossing(semaphoreA, semaphoreB);

crossing.Start();

Console.ReadLine();

crossing.Stop();
