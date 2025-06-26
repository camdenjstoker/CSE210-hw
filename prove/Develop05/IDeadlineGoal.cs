using System;

public interface IDeadlineGoal
{
    DateTime DueDate { get; } // The target deadline for the goal (nullable)

    DateTime? CompletionDate { get; set; } // When the goal was actually completed (nullable, set on completion)

    int BaseDeadlineBonus { get; } // Fixed bonus for meeting the deadline

    int CalculateEarlyCompletionBonus(); // Calculates extra points for early completion

    bool IsOverdue(); // Checks if the goal is currently past its deadline

    int? GetDaysRemaining(); // Days left until deadline, or days past due (nullable if no deadline)
}