using System;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        // initiat class being called for methods 
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        // to set the loop
        bool _Running = true; 

        while (_Running)
        {
            // The beginning
            Console.WriteLine($" 1. Write new Entry. \n 2. Display. \n 3. Save Entries. \n 4. Load Entries. \n 5. Leave Journal. ");
            Console.Write("Choose what you would like to do: ");
            string _Choose = Console.ReadLine();
            int _Choice = int.Parse(_Choose);
            Console.WriteLine("");


            // Option 1-- Make a new entry
            if (_Choice == 1)
            {
                string prompt = promptGenerator.GetRandomPrompt();
                Console.WriteLine($"\nPrompt: {prompt}");
                Console.Write("Your Response: ");
                string response = Console.ReadLine();
                string date = DateTime.Now.ToShortDateString();

                Entry entry = new Entry(date,prompt, response);
                journal.AddEntry(entry);
                
            }

            //  Option 2-- Display Journal Entries
            else if (_Choice == 2)
            {
                journal.DisplayEntries();
            }

            // Option 3-- Save Journal
            else if (_Choice == 3)
            {
                Console.Write("Enter filename to save to: ");
                string saveFile = Console.ReadLine();
                journal.SaveToFile(saveFile);
            }

            // Option 4-- Load Journal
            else if (_Choice == 4)
            {
                Console.Write("Enter filename to load: ");
                string loadFile = Console.ReadLine();
                journal.LoadFromFile(loadFile);
            }

            // Option 5-- Leave Journal
            else if (_Choice == 5)
            {
                Console.WriteLine("See you later. \nCome back soon.");
                _Running = false;
            }

            // Correction if wrong input is given
            else
            {
                Console.WriteLine("Invalid response. Please enter a number between 1-5. ");
            }
        }
    }
}