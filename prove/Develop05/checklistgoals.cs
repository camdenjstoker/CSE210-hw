using System;

// ChecklistGoal inherits from Goal AND implements IDeadlineGoal
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

    public override void RecordEvent()
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++; // Increment the completion count
            Console.WriteLine($"Congratulations! You have progressed on '{_shortName}'. You earned {_points} points.");

            if (_amountCompleted == _target) // Check if the goal is now fully completed
            {
                _completionDate = DateTime.Now; // Set the full completion date

                int totalBonus = _bonusPoints; // Start with the checklist completion bonus

                Console.WriteLine($"You have completed '{_shortName}' {_target} out of {_target} times and earned a bonus of {_bonusPoints} points!");

                // Check for deadline bonus if applicable
                if (_dueDate.HasValue)
                {
                    bool completedOnTime = _completionDate.Value <= _dueDate.Value;
                    if (completedOnTime)
                    {
                        totalBonus += _baseDeadlineBonus;
                        Console.WriteLine($"You also earned a deadline bonus of {_baseDeadlineBonus} points!");

                        int earlyBonus = CalculateEarlyCompletionBonus();
                        if (earlyBonus > 0)
                        {
                            totalBonus += earlyBonus;
                            Console.WriteLine($"And an early completion bonus of {earlyBonus} points!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unfortunately, this checklist goal was completed after its deadline, so no deadline bonus.");
                    }
                }
                // The main program will need to add these total points to the user's score.
            }
        }
        else
        {
            Console.WriteLine($"You have already completed '{_shortName}' {_target} out of {_target} times. No additional points.");
        }
    }

    // --- GetPoints() Override (Addressing your point 6) ---
    public override int GetPoints()
    {
        // This method should return the points earned for the *current* completion,
        // or the *total bonus* if the goal just finished.
        // It's a bit ambiguous based on how it's used by a calling program.
        // Let's make it return the sum of the _points earned *so far* (per increment)
        // plus the checklist _bonusPoints if the target is reached,
        // plus any deadline/early bonus if applicable.

        int totalPoints = _amountCompleted * _points; // Points from each increment

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

    // --- GetStringRepresentation() Override (Addressing your point 4) ---
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