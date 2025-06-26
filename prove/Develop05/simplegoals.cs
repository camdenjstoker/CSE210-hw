using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"You have completed '{_shortName}' and earned {_points} points!!!");
        }
        else
        {
            Console.WriteLine($"You have already completed {_shortName}. No additional Points awarded.");
        }
    }

    public override string GetStringRepresentation()
    {
        string status = _isComplete ? "[x]" : "[ ]";
        return $"{status} {_shortName} ({_description})";
    }
}