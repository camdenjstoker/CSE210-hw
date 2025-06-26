using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _listingPrompts;

    public ListingActivity() : base("Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _listingPrompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public void RunActivity()
    {
        StartActivity(); // Show welcome message and ask for duration

        string prompt = GetRandomPrompt();
        Console.WriteLine("\nList as many responses as you can to the following prompt:");
        Console.WriteLine($">>> {prompt} <<<");
        Console.Write("You may begin in: ");
        DisplayCountdown(5);

        Console.WriteLine("\nStart listing items. Press Enter after each one:");

        int count = CollectItems(GetDuration());

        Console.WriteLine($"\nYou listed {count} items.");
        EndActivity(); // Show completion message
    }

    private string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_listingPrompts.Count);
        return _listingPrompts[index];
    }

    private int CollectItems(int duration)
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                itemCount++;
            }
        }

        return itemCount;
    }
}
