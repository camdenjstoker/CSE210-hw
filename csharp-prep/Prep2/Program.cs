using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep 2 World!");
        Console.WriteLine("");
        Console.Write("What is your percentage in the class? [Write without % sign]: ");
        string value = Console.ReadLine();
        int percent = int.Parse(value);
        string letter = "";

        if (percent >= 90)
        {
            letter = "A";
        } 
        else if (percent >= 80 && percent < 90)
        {
            letter = "B";
        }
        else if (percent >= 70 && percent < 80)
        {
            letter = "C";
        }
        else if (percent >= 60 && percent < 70)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        Console.WriteLine($"Your current grade is {letter}.");
        if (letter == "A")
        {
            Console.WriteLine("You are passing the class. ");
        }
        else if (letter == "B")
        {
            Console.WriteLine("You are passing the class. ");
        }
        else if (letter == "C")
        {
            Console.WriteLine("You are passing the class. ");
        }
        else
        {
            Console.WriteLine("You are not passing the class. ");
        }
       
    }
}