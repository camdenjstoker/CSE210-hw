using System;

public class Track
{
    // Array of directions: 'L' = Left, 'R' = Right, 'S' = Straight
    public char[] directions;

    // Each segment of the track is this many meters long
    public int segmentLength;

    public Track(char[] directions, int segmentLength)
    {
        this.directions = directions;
        this.segmentLength = segmentLength;
    }

    // Get the direction at a given position in meters
    public char GetDirectionAtPosition(double position)
    {
        int segmentIndex = (int)(position / segmentLength);
        if (segmentIndex >= directions.Length)
        {
            return 'F'; // Finished
        }
        return directions[segmentIndex];
    }
}

