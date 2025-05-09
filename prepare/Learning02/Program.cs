using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Learning 02 World!");

        Jobs job1 = new Jobs();
        job1._jobTitle = "Software Engineer";
        job1._companyName = "Microsoft";
        job1._startYr = 2019;
        job1._endYr = 2022;

        Jobs job2 = new Jobs();
        job2._jobTitle = "Manager";
        job2._companyName = "Apple";
        job2._startYr = 2022;
        job2._endYr = 2023;

        Resumes myResume = new Resumes();
        myResume._name = "Camden Stoker";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}