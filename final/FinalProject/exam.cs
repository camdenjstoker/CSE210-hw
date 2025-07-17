using System;
using System.Collections.Generic;
using System.Linq;

public class Exam : Test
{
    private int _timeLimitMinutes;

    public Exam(string title, List<Question> questions, int timeLimitMinutes) : base(title, questions)
    {
        if (timeLimitMinutes <= 0)
        {
            Console.WriteLine("Warning: Exam time limit must be greater than 0 minutes. Setting to 60 minutes.");
            _timeLimitMinutes = 60;
        }
        else
        {
            _timeLimitMinutes = timeLimitMinutes;
        }
    }

    public int GetTimeLimitMinutes()
    {
        return _timeLimitMinutes;
    }

    public override int ConductTest()
    {
        int score = 0;
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddMinutes(_timeLimitMinutes);

        Console.WriteLine($"\n--- Starting Exam: {GetTitle()} ---");
        Console.WriteLine($"You have {_timeLimitMinutes} minutes to complete this exam.");
        Console.WriteLine("-----------------------------------");

        foreach (var question in _questions)
        {
            TimeSpan timeLeft = endTime - DateTime.Now;

            if (timeLeft.TotalMinutes <= 0)
            {
                Console.WriteLine("\nTime's up! The exam has ended.");
                break;
            }

            Console.WriteLine($"\n--- Question (Time Left: {timeLeft.Minutes}m {timeLeft.Seconds}s) ---");

            question.Ask();
            string userAnswer = Console.ReadLine();

            timeLeft = endTime - DateTime.Now;
            if (timeLeft.TotalMinutes <= 0)
            {
                Console.WriteLine("\nTime's up! Your answer to the last question was not recorded.");
                break;
            }

            if (question.Grade(userAnswer))
            {
                Console.WriteLine("Correct! 🎉");
                score += question.GetPoints();
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct answer was: {question.GetCorrectAnswer()}");
            }
            Console.WriteLine("-----------------------------------");
        }

        Console.WriteLine($"\n--- Exam Finished: {GetTitle()} ---");
        Console.WriteLine($"You scored {score} out of {GetTotalPossiblePoints()} points.");
        _lastAttemptScore = score;
        return score;
    }

    public override int CalculateGrade()
    {
        return _lastAttemptScore;
    }
}