// GoalManager.cs
using System;
using System.Collections.Generic; // Required for List<T>
using System.IO; // Required for file operations (Saving/Loading)

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nYou have {_score} points.");
        Console.WriteLine("Goals:");
        ListGoals(); // Calls the ListGoals method to show active goals
    }

    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You currently have no goals.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStringRepresentation()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("\nThe types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string goalTypeChoice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        DateTime? dueDate = null;
        int baseDeadlineBonus = 0;

        // Ask for deadline details only for Simple and Checklist Goals
        if (goalTypeChoice == "1" || goalTypeChoice == "3")
        {
            Console.Write("Does this goal have a deadline? (yes/no) ");
            string hasDeadline = Console.ReadLine().ToLower();
            if (hasDeadline == "yes")
            {
                Console.Write("Enter due date (MM/DD/YYYY): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime parsedDate))
                {
                    dueDate = parsedDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Deadline will not be set.");
                }

                Console.Write("Enter base deadline bonus points (e.g., 500 for a significant bonus): ");
                if (int.TryParse(Console.ReadLine(), out int parsedBonus))
                {
                    baseDeadlineBonus = parsedBonus;
                }
                else
                {
                    Console.WriteLine("Invalid bonus amount. Base deadline bonus will be 0.");
                }
            }
        }


        switch (goalTypeChoice)
        {
            case "1": // Simple Goal
                _goals.Add(new SimpleGoal(name, description, points, dueDate, baseDeadlineBonus));
                break;
            case "2": // Eternal Goal
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3": // Checklist Goal
                Console.Write("How many times does this goal need to be completed for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for completing it that many times? ");
                int bonusPoints = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonusPoints, dueDate, baseDeadlineBonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
        Console.WriteLine("Goal created successfully!");
    }

    public void RecordEvent()
    {
        Console.WriteLine("\nWhich goal did you accomplish?");
        ListGoals(); // Show the list of goals to choose from

        Console.Write("Enter the number of the goal: ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex > 0 && goalIndex <= _goals.Count)
        {
            Goal selectedGoal = _goals[goalIndex - 1]; // Get the selected goal (adjust for 0-based index)

            // It expects RecordEvent to return the points earned.
            int pointsEarned = selectedGoal.RecordEvent(); // Call RecordEvent on the specific goal
            _score += pointsEarned; // Add the points returned from the event to the total score

            Console.WriteLine($"\nEvent recorded! You earned {pointsEarned} points. Your current total score is {_score}.");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score); // Save the current score first

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetSaveString()); 
            }
        }
        Console.WriteLine($"Goals saved to {filename} successfully!");
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _goals.Clear(); // Clear existing goals before loading
            string[] lines = File.ReadAllLines(filename);

            _score = int.Parse(lines[0]); // First line is always the score

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(':');
                string goalType = parts[0];
                string[] dataParts = parts[1].Split(','); // Split the data part by comma

                string name = dataParts[0];
                string description = dataParts[1];
                int points = int.Parse(dataParts[2]);

                DateTime? dueDate = null;
                DateTime tempDueDate;
                // Check if dueDate string exists and can be parsed
                if (dataParts.Length > 4 && DateTime.TryParse(dataParts[4], out tempDueDate)) // dueDateString is at index 4 for Simple/Checklist, 3 for Eternal
                {
                    dueDate = tempDueDate;
                }
                // Handle "null" string for dates
                else if (dataParts.Length > 4 && dataParts[4].ToLower() == "null")
                {
                    dueDate = null;
                }

                DateTime? completionDate = null;
                DateTime tempCompletionDate;
                // Check if completionDate string exists and can be parsed
                if (dataParts.Length > 5 && DateTime.TryParse(dataParts[5], out tempCompletionDate)) // completionDateString is at index 5 for Simple/Checklist
                {
                    completionDate = tempCompletionDate;
                }
                // Handle "null" string for dates
                else if (dataParts.Length > 5 && dataParts[5].ToLower() == "null")
                {
                    completionDate = null;
                }


                int baseDeadlineBonus = 0;
                if (dataParts.Length > 6) // baseDeadlineBonus is at index 6 for Simple/Checklist
                {
                    baseDeadlineBonus = int.Parse(dataParts[6]);
                }


                switch (goalType)
                {
                    case "SimpleGoal":
                        bool isComplete = bool.Parse(dataParts[3]); // isComplete is at index 3
                        SimpleGoal loadedSimpleGoal = new SimpleGoal(name, description, points, dueDate, baseDeadlineBonus);
                        
                        // Set completion status and completion date
                        if (isComplete)
                        {

                            loadedSimpleGoal.RecordEvent(); // Marks as complete and sets date to NOW
                            if (completionDate.HasValue)
                            {
                                // Overwrite the DateTime.Now set by RecordEvent with the loaded date
                                ((IDeadlineGoal)loadedSimpleGoal).CompletionDate = completionDate.Value;
                            }
                        }
                        _goals.Add(loadedSimpleGoal);
                        break;

                    case "EternalGoal":
                        // Eternal goals don't have additional state to load beyond constructor
                        _goals.Add(new EternalGoal(name, description, points));
                        break;

                    case "ChecklistGoal":
                        int amountCompleted = int.Parse(dataParts[3]); // amountCompleted is at index 3
                        int target = int.Parse(dataParts[4]); // target is at index 4
                        int bonus = int.Parse(dataParts[5]); // bonus is at index 5
                        
                        ChecklistGoal loadedChecklistGoal = new ChecklistGoal(name, description, points, target, bonus, dueDate, baseDeadlineBonus);
                        
                        // Manually set amountCompleted for loaded goal using the new setter
                        loadedChecklistGoal.SetAmountCompleted(amountCompleted); 

                        // Set completion date if available for ChecklistGoal (only if target reached)
                        if (completionDate.HasValue && amountCompleted == target)
                        {
                            ((IDeadlineGoal)loadedChecklistGoal).CompletionDate = completionDate.Value;
                        }

                        _goals.Add(loadedChecklistGoal);
                        break;

                    default:
                        Console.WriteLine($"Unknown goal type: {goalType}");
                        break;
                }
            }
            Console.WriteLine($"Goals loaded from {filename} successfully!");
        }
        else
        {
            Console.WriteLine($"File '{filename}' not found.");
        }
    }
}