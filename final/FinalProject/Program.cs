using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize core components
        AuthManager authManager = new AuthManager();
        QuestionBank questionBank = new QuestionBank();
        ITimeUtility timeUtility = new BasicUtility();

        // Initialize QuizManager with its dependencies
        QuizManager quizManager = new QuizManager(authManager, questionBank, timeUtility);

        // Main application loop
        while (true)
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Login / Register");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                quizManager.Run(); 
            }
            else if (choice == "2")
            {
                Console.WriteLine("Exiting application.");
                break; 
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}