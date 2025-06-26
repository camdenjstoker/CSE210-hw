using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
         // Main application loop for the menu
        while (true)
        {
            Console.Clear(); 
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start Breathing Activity");
            Console.WriteLine("  2. Start Reflection Activity"); 
            Console.WriteLine("  3. Start Listening Activity");
            Console.WriteLine("  4. Start Listing Activity");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine(); // Read user's menu choice

            // Using if-else if-else statements to handle different choices
            if (choice == "1")
            {
                // Instantiate and run the Breathing Activity
                BreathingActivity breathingActivity = new BreathingActivity();
                breathingActivity.RunActivity();
            }
            else if (choice == "2")
            {
                // Instantiate and run the Mindfulness Activity (which serves as Reflection)
                MindfulnessActivity mindfulnessActivity = new MindfulnessActivity();
                mindfulnessActivity.RunActivity();
            }
            else if (choice == "3")
            {
                // Instantiate and run the Listening Activity
                ListeningActivity listeningActivity = new ListeningActivity();
                listeningActivity.RunActivity();
            }
            else if (choice == "4")
            {
                // Instantiate and run the Listening Activity
                ListingActivity listingActivity = new ListingActivity();
                listingActivity.RunActivity();
            }
            else if (choice == "5")
            {
                // Exit the application
                Console.WriteLine("\nThank you for using the Mindfulness App. Goodbye!");
                return; // Exit the Main method, terminating the program
            }
            else
            {
                // Handle invalid input
                Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 4.");
                Thread.Sleep(2000); // Pause for 2 seconds so the user can read the message
            }
        }
    }
}