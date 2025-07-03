using System;

public class SimpleGoal : Goal, IDeadlineGoal
{
    private bool _isComplete;
    private DateTime? _dueDate;
    private DateTime? _completionDate;
    private int _baseDeadlineBonus;

    public SimpleGoal(string name, string description, int points, DateTime? dueDate = null, int baseDeadlineBonus = 0) : base(name, description, points)
    {
        _isComplete = false;
        _dueDate = dueDate;
        _completionDate = null; // Initially not fully completed
        _baseDeadlineBonus = baseDeadlineBonus;
    }

    // This is the updated method
    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            _completionDate = DateTime.Now; // Set completion date when the goal is marked complete

            int pointsEarned = _points; // Start with the base points for completing this goal

            Console.WriteLine($"Congratulations! You have completed '{_shortName}' and earned {_points} points!"); 

            // Check for deadline bonus if applicable
            if (_dueDate.HasValue) // Only apply if a deadline was set
            {
                // Check if completion was on or before the due date
                bool completedOnTime = _completionDate.Value <= _dueDate.Value;
                if (completedOnTime)
                {
                    pointsEarned += _baseDeadlineBonus; // Add the base deadline bonus
                    Console.WriteLine($"You also earned a deadline bonus of {_baseDeadlineBonus} points!");

                    int earlyBonus = CalculateEarlyCompletionBonus(); // Calculate early bonus
                    if (earlyBonus > 0)
                    {
                        pointsEarned += earlyBonus; // Add the early completion bonus
                        Console.WriteLine($"And an early completion bonus of {earlyBonus} points!"); 
                    }
                }
                else
                {
                    Console.WriteLine("Unfortunately, this goal was completed after its deadline, so no deadline bonus."); 
                }
            }
            return pointsEarned; // Return the total points earned from this specific event
        }
        else
        {
            Console.WriteLine($"You have already completed '{_shortName}'. No additional Points awarded.");
            return 0; // Return 0 points if already complete and no new points are awarded
        }
    }


    public override string GetStringRepresentation()
    {
        string status = _isComplete ? "[x]" : "[ ]";
        string deadlineInfo = "";

        if (_dueDate.HasValue)
        {
            if (_isComplete) // Fully completed simple goal
            {
                deadlineInfo = $" (Completed: {_completionDate.Value.ToShortDateString()}";
                if (_completionDate.Value <= _dueDate.Value)
                {
                    deadlineInfo += $" - By Deadline! Base Bonus: {_baseDeadlineBonus}";
                    int earlyBonus = CalculateEarlyCompletionBonus();
                    if (earlyBonus > 0)
                    {
                        deadlineInfo += $", Early Bonus: {earlyBonus}";
                    }
                }
                deadlineInfo += $")";
            }
            else // Not completed yet
            {
                deadlineInfo = $" (Due: {_dueDate.Value.ToShortDateString()}";
                if (IsOverdue())
                {
                    deadlineInfo += " - OVERDUE";
                }
                else
                {
                    int? daysLeft = GetDaysRemaining();
                    if (daysLeft.HasValue)
                    {
                        deadlineInfo += $" - {daysLeft.Value} days left";
                    }
                }
                deadlineInfo += $")";
            }
        }
        return $"{status} {_shortName} ({_description}){deadlineInfo}";
    }

    public override string GetSaveString()
    {
        // The format for saving needs to include all properties, including deadline info
        // Format: SimpleGoal:name,description,points,isComplete,dueDateString,completionDateString,baseDeadlineBonus
        
        string dueDateStr = _dueDate.HasValue ? _dueDate.Value.ToString("yyyy-MM-dd") : "null";
        string completionDateStr = _completionDate.HasValue ? _completionDate.Value.ToString("yyyy-MM-dd") : "null";

        return $"SimpleGoal:{_shortName},{_description},{_points},{_isComplete},{dueDateStr},{completionDateStr},{_baseDeadlineBonus}";
    }

    // --- Explicit Implementation of IDeadlineGoal properties ---
    DateTime IDeadlineGoal.DueDate => _dueDate ?? DateTime.MinValue;

    DateTime? IDeadlineGoal.CompletionDate
    {
        get { return _completionDate; }
        set { _completionDate = value; }
    }

    int IDeadlineGoal.BaseDeadlineBonus => _baseDeadlineBonus;

    // --- Implementation of IDeadlineGoal methods ---
    public int CalculateEarlyCompletionBonus()
    {
        // calculate this bonus only if the goal is fully completed AND has a deadline.
        if (_dueDate == null || _completionDate == null)
        {
            return 0; // No deadline, not fully complete, or completion date not set
        }

        if (_completionDate.Value > _dueDate.Value)
        {
            return 0; // Completed after due date, no early bonus.
        }

        TimeSpan timeRemaining = _dueDate.Value - _completionDate.Value;
        int daysEarly = (int)Math.Floor(timeRemaining.TotalDays);

        // 10 points for consistancy
        return daysEarly * 10;
    }

    public bool IsOverdue()
    {
        // A simple goal is overdue if it has a due date, is not complete, and the due date has passed.
        if (_dueDate == null || _isComplete)
        {
            return false;
        }
        return DateTime.Now > _dueDate.Value;
    }

    public int? GetDaysRemaining()
    {
        if (_dueDate == null)
        {
            return null;
        }
        TimeSpan timeDifference = _dueDate.Value - DateTime.Now;
        return (int)Math.Ceiling(timeDifference.TotalDays);
    }
    

}