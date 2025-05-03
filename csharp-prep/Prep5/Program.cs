using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep 5 World!");

        string name = UserName();
        Welcome();

        int favNum = FavNumber();
        int squared = SquaredNumber(favNum);

        Console.WriteLine($"{name} Your favorite number sqaured is: {squared}");
        
        static string UserName()
        {
            Console.Write("What is your name? ");
            string user = Console.ReadLine();
            if (user == "")
            {
                Console.WriteLine("Please enter you name: ");
                user = Console.ReadLine();
            }
            
            return user;
        }
        static void Welcome ()
        {
            Console.WriteLine($"Welcome to the program.");
        }
        static int FavNumber ()
        {
            Console.Write("What is your faovrite number? ");
            int number = int.Parse(Console.ReadLine());
            return number;
        }
        static int SquaredNumber(int number)
        {
            return number * number;
        }
    }
}