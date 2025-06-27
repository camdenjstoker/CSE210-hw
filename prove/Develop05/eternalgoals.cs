using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
        // No specific state like completion for EternalGoal in constructor
    }

    public override int RecordEvent() 
    {
        Console.WriteLine($"Congratulations! You have recorded progress on '{_shortName}' and earned {_points} points!"); // Keep your message!

        return _points; // Return the base points for this specific event
    }

    public override int GetPoints()
    {
        return _points; // Returns the base points for display purposes
    }

    public override string GetStringRepresentation()
    {
        // Always display as ongoing.
        return $"[ ] {_shortName} ({_description})"; // No completion status, always [ ]
    }
}