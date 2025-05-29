using System;

/// <summary>
/// Represents a vehicle on the track.
/// </summary>
public class Vehicle
{
    public string name;
    public string description;

    /// <summary>
    /// Fuel level in liters (e.g., 0 to 100)
    /// </summary>
    public double fuelLevel;

    /// <summary>
    /// Fuel efficiency in km per liter (e.g., 5 to 25)
    /// </summary>
    public double fuelEfficiency;

    /// <summary>
    /// Speed in m/s on straight stretches (e.g., 20 to 100)
    /// </summary>
    public double speedStraight;

    /// <summary>
    /// Speed in m/s on corners (left or right) (e.g., 10 to 60)
    /// </summary>
    public double speedCorner;

    /// <summary>
    /// Current speed in m/s
    /// </summary>
    public double currentSpeed;

    /// <summary>
    /// Position on the track in meters
    /// </summary>
    public double position;

    /// <summary>
    /// The track this vehicle is driving on
    /// </summary>
    public Track track;

    public Vehicle(string name, string description, double fuelLevel, double fuelEfficiency,
        double speedStraight, double speedCorner, Track track)
    {
        this.name = name;
        this.description = description;
        this.fuelLevel = fuelLevel;
        this.fuelEfficiency = fuelEfficiency;
        this.speedStraight = speedStraight;
        this.speedCorner = speedCorner;
        this.track = track;
        this.position = 0;
        this.currentSpeed = 0;
    }

    public void Advance()
    {
        if (fuelLevel <= 0)
            return;

        char direction = track.GetDirectionAtPosition(position);
        currentSpeed = (direction == 'S') ? speedStraight : speedCorner;

        double time = 1; // 1 second
        double distance = currentSpeed * time;
        double kmTraveled = distance / 1000.0;

        position += distance;
        fuelLevel -= kmTraveled / fuelEfficiency;
    }

    public void Display()
    {
        char direction = track.GetDirectionAtPosition(position);
        string directionStr = direction switch
        {
            'L' => "Turning Left",
            'R' => "Turning Right",
            'S' => "Going Straight",
            'F' => "Finished",
            _ => "Unknown"
        };

        Console.WriteLine($"{name,-10} | Fuel: {fuelLevel,6:F2}L | Speed: {currentSpeed,5:F1} m/s | " +
                          $"Position: {position,6:F1} m | {directionStr}");
    }
}

