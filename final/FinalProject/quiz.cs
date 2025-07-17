using System;
using System.Collections.Generic;
using System.Linq;

public class Quiz : Test
{
    private int _maxAttempts;

    public Quiz(string title, List<Question> questions, int maxAttempts) : base(title, questions)
    {
        if (maxAttempts < 1)
        {
            Console.WriteLine("Max attepts must be set to at least 1 attempt. Setting to 1 attempt now.");
            _maxAttempts = 1;
        }
        else
        {
            _maxAttempts = maxAttempts;
        }
    }

    public int GetMaxAttempts()
    {
        return _maxAttempts;
    }

    public void SetMaxAttempts(int newMaxAttempts)
    {
        if (newMaxAttempts < 1)
        {
            Console.WriteLine("The maximum amount of attempts cannot be less than 1. Please try again");
        }
        else
        {
            _maxAttempts = newMaxAttempts;
            Console.WriteLine($"The max amount of attempts for {_title} is set to {newMaxAttempts}");
        }
    }


    public override int ConductTest()
    {
    int score = 0;
    int currentAttempt = 1;

    Console.WriteLine($"\n--- Starting Quiz: {GetTitle()} (Attempt {currentAttempt}/{_maxAttempts}) ---");

    foreach (var question in _questions)
    {
        question.Ask();
        string userAnswer = Console.ReadLine();

        if (question.Grade(userAnswer))
        {
            Console.WriteLine("Correct! Well done!");
            score += question.GetPoints();
        }
        else
        {
            Console.WriteLine($"Incorrect. The correct answer was: {question.GetCorrectAnswer()} ");
        }
        Console.WriteLine("-----------------------------------");
    }

    Console.WriteLine($"--- Quiz Finished: {GetTitle()} ---");
    Console.WriteLine($"You scored {score} out of {GetTotalPossiblePoints()} points.");
    _lastAttemptScore = score;
    return score; 
    }

    public override int CalculateGrade()
    {
        return _lastAttemptScore;
    }
}