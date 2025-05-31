using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."
            ),
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."
            ),
            new Scripture(
                new Reference("Psalm", 23, 1, 3),
                "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: he leadeth me beside the still waters. He restoreth my soul: he leadeth me in the paths of righteousness for his name's sake."
            ),
            new Scripture(
                new Reference("Romans", 8, 28),
                "And we know that all things work together for good to them that love God, to them who are the called according to his purpose."
            ),
            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."
            )
        };

        Random rand = new Random();
        bool keepGoing = true;

        while (keepGoing)
        {
            Scripture scripture = scriptures[rand.Next(scriptures.Count)];

            while (!scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    keepGoing = false;
                    break;
                }

                scripture.HideRandomWords(3);
            }

            if (!keepGoing)
                break;

            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nAll words are hidden.");
            Console.Write("Type 'reset' to try a new verse or 'quit' to exit: ");
            string choice = Console.ReadLine().ToLower();

            if (choice != "reset")
            {
                keepGoing = false;
            }
        }

        Console.WriteLine("\nGoodbye! Thanks for memorizing.");
    }
}

