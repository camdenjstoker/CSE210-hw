using System;

public class Resumes
{
    public string _name;
    public List<Jobs> _jobs = new List<Jobs>();
    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");

        foreach (Jobs jobs in _jobs)
        {
            jobs.Display();
        }
    }
}