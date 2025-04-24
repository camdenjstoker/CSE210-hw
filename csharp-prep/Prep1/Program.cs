using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep 1 World!");
        Console.WriteLine("");
        Console.Write("What is you first name? ");
        string f_name = Console.ReadLine();
        Console.Write("What is your last name? ");
        string l_name = Console.ReadLine();
        Console.WriteLine($"Your name is {l_name}, {f_name} {l_name}.");

    }
}