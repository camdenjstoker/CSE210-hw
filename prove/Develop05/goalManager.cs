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
                // This will use the new GetStringRepresentation for saving
                outputFile.WriteLine($"{goal.GetType().Name}:{goal.GetStringRepresentation()}");
            }
        }
        Console.WriteLine($"Goals saved to {filename} successfully!");
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename of the goal file? ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _goals.Clear(); // Clear existing goals before loading
            string[] lines = File.ReadAllLines(filename);

            _score = int.Parse(lines[0]); // Load the score from the first line

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string goalType = parts[0];
                string goalData = parts[1];

                string[] dataParts = goalData.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                switch (goalType)
                {
                    case "SimpleGoal":
                        string name = dataParts[0].Substring(dataParts[0].IndexOf(' ') + 1); 
                        string description = dataParts[1].Substring(0, dataParts[1].Length - 1); 
                        int points = int.Parse(dataParts[2]);
                        _goals.Add(new SimpleGoal(name, description, points));
                        break;
                    case "EternalGoal":
                        name = dataParts[0].Substring(dataParts[0].IndexOf(' ') + 1);
                        description = dataParts[1].Substring(0, dataParts[1].Length - 1);
                        points = int.Parse(dataParts[2]);
                        _goals.Add(new EternalGoal(name, description, points));
                        break;
                    case "ChecklistGoal":
                        name = dataParts[0].Substring(dataParts[0].IndexOf(' ') + 1);
                        description = dataParts[1].Substring(0, dataParts[1].Length - 1);
                        points = int.Parse(dataParts[2]);
                        int amountCompleted = int.Parse(dataParts[3].Split('/')[0]);
                        int target = int.Parse(dataParts[3].Split('/')[1].Split(' ')[0]); 
                        int bonus = int.Parse(dataParts[4]); 
                        ChecklistGoal loadedChecklistGoal = new ChecklistGoal(name, description, points, target, bonus);
                        // Manually set amountCompleted for loaded goal
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