using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep 3 World!");
        Random random_generator = new Random();
        int number = random_generator.Next(1, 101);
        int guess;
        string num;


        do
        {
            Console.Write("Guess a number between 1-100: ");
            num = Console.ReadLine();
            guess = int.Parse(num);
            
            if (guess > 100)
            {
                Console.WriteLine("Please put in a valid number. ");
                continue;
            }

            if (guess > number)
            {
                Console.WriteLine("The number is lower than what you guessed.");
            }
            else if (guess < number)
            {
                Console.WriteLine("The number is higher than what you guessed.");
            }
            else 
            {
                Console.WriteLine("Congratulations you guessed the right number!!!");
            }
        } while (guess != number);
    }
}