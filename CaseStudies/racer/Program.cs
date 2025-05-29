using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Create a sample track: Straight, Left, Right, Straight
        char[] trackPattern = { 'S', 'S', 'S', 'S', 'S', 'S', 'L', 'S', 'S', 'S', 'S', 'R', 'S', 'S', 'L', 'R' };
        Track raceTrack = new Track(trackPattern, 100); // Each segment = 100m

        // Create vehicles
        Vehicle car1 = new Vehicle("RedBolt", "Fast on straights", 50, 10, 40, 25, raceTrack);
        Vehicle car2 = new Vehicle("CornerKing", "Great cornering", 50, 12, 30, 35, raceTrack);

        // Main simulation loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Race Car Simulation (each step = 1 second)\n");

            car1.Advance();
            car2.Advance();

            car1.Display();
            car2.Display();

            Console.WriteLine("\nPress Ctrl+C to stop...");
            Thread.Sleep(1000);
        }
    }
}

