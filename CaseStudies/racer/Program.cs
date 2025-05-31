using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Create a sample track: Straight, Left, Right, Straight
        char[] trackPattern = { 'S', 'S', 'S', 'S', 'S', 'R', 'L', 'S', 'S', 'S', 'S', 'R', 'L', 'S', 'L', 'R', 'S', 'S', 'R', 'L', 'S' };
    
        Track raceTrack = new Track(trackPattern, 100); // Each segment = 100m

        // Create vehicles
        Vehicle car1 = new Vehicle("RedBolt", "Fast on straights", 50, 10, 50, 30, raceTrack);
        Vehicle car2 = new Vehicle("CornerKing", "Great cornering", 50, 12, 30, 45, raceTrack);

        // Main simulation loop
        foreach (trackPattern chars in raceTrack)
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

