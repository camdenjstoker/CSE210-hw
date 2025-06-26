using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {

    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Congratulations! You have recorded progress on '{_shortName}' and earned {_points} points!");
    }

    public override int GetPoints()
    {
        return _points;
    }

    public override string GetStringRepresentation()
    {
        // Always display as ongoing.
        return $"[ ] {_shortName} ({_description})"; // No completion status, always [ ]
    }
}