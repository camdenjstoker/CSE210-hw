using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep 4 World!");
        int num;
        string number; 
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a number. Enter 0 when finished: ");
        
        do
        {

            Console.Write("Enter a number: ");
            number = Console.ReadLine();
            num = int.Parse(number);
            if (num != 0)
            {
                numbers.Add(num);
            }

        }while (num != 0);
        int max_number = numbers[0];
        int total = 0; 

        foreach (int n in numbers)
        {
            if (n > max_number)
            {
                max_number = n;
            }
            total += n;  
        }

        double average = (double)total / numbers.Count;

        Console.WriteLine($"The biggest number is {max_number}. ");
        Console.WriteLine($"The Sum of the numbers is {total}. ");
        Console.WriteLine($"The averge is {average}. ");

    }
}