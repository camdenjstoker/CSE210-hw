using System;

public class ChecklistGoal : Goal, IDeadlineGoal
{
    private int _amountCompleted;
    private int _target;
    private int _bonusPoints;
    private DateTime? _dueDate;         
    private DateTime? _completionDate;  
    private int _baseDeadlineBonus;     

    // Constructor for ChecklistGoal with optional deadline parameters
    public ChecklistGoal(string name, string description, int points, int target, int bonusPoints,
                         DateTime? dueDate = null, int baseDeadlineBonus = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonusPoints = bonusPoints;
        _amountCompleted = 0; 
        _dueDate = dueDate;
        _completionDate = null; // Initially not fully completed
        _baseDeadlineBonus = baseDeadlineBonus;
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
        // We calculate this bonus only if the goal is fully completed AND has a deadline.
        if (_dueDate == null || _amountCompleted < _target || _completionDate == null)
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
        // A checklist goal is overdue if it has a due date, is not fully complete, and the due date has passed.
        if (_dueDate == null || _amountCompleted == _target)
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

    // --- Overrides from Goal class ---

    // This is the updated method
    public override int RecordEvent() 
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++; // Increment the completion count
            Console.WriteLine($"Congratulations! You have progressed on '{_shortName}'. You earned {_points} points."); 

            int pointsEarnedThisEvent = _points; // Points for this single increment

            if (_amountCompleted == _target) // Check if the goal is now fully completed
            {
                _completionDate = DateTime.Now; // Set the full completion date

                pointsEarnedThisEvent += _bonusPoints; // Add the checklist completion bonus to this event's points

                Console.WriteLine($"You have completed '{_shortName}' {_target} out of {_target} times and earned a bonus of {_bonusPoints} points!"); // Keep your message!

                // Check for deadline bonus if applicable
                if (_dueDate.HasValue)
                {
                    bool completedOnTime = _completionDate.Value <= _dueDate.Value;
                    if (completedOnTime)
                    {
                        pointsEarnedThisEvent += _baseDeadlineBonus; // Add deadline bonus
                        Console.WriteLine($"You also earned a deadline bonus of {_baseDeadlineBonus} points!"); // Keep your message!

                        int earlyBonus = CalculateEarlyCompletionBonus();
                        if (earlyBonus > 0)
                        {
                            pointsEarnedThisEvent += earlyBonus; // Add early completion bonus
                            Console.WriteLine($"And an early completion bonus of {earlyBonus} points!"); // Keep your message!
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unfortunately, this checklist goal was completed after its deadline, so no deadline bonus."); // Keep your message!
                    }
                }
            }
            return pointsEarnedThisEvent; // Return the total points earned from this specific event
        }
        else
        {
            Console.WriteLine($"You have already completed '{_shortName}' {_target} out of {_target} times. No additional points."); // Keep your message!
            return 0; // Return 0 if already fully complete
        }
    }

    public override int GetPoints()
    {
        // This method should return the total points the goal is worth if fully completed,
        int totalPoints = _amountCompleted * _points; // Points from each increment so far

        if (_amountCompleted == _target)
        {
            totalPoints += _bonusPoints; // Add the checklist completion bonus

            // Add deadline and early completion bonuses if applicable and completed on time
            if (_dueDate.HasValue && _completionDate.HasValue && _completionDate.Value <= _dueDate.Value)
            {
                totalPoints += _baseDeadlineBonus;
                totalPoints += CalculateEarlyCompletionBonus();
            }
        }
        return totalPoints;
    }
    public override string GetStringRepresentation()
    {
        string status = (_amountCompleted == _target) ? "[X]" : "[ ]";
        string deadlineInfo = "";

        if (_dueDate.HasValue)
        {
            if (_amountCompleted == _target) // Fully completed checklist
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
            else // Checklist not fully completed yet
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

        return $"{status} {_shortName} ({_description}) -- Currently completed: {_amountCompleted}/{_target} (Bonus: {_bonusPoints} points){deadlineInfo}";
    }
}