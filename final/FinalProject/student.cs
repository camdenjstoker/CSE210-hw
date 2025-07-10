
public class Student : User
{

    public Student(string name, string id, string passwordHash) : base(name, id, passwordHash)
    {
        
    }

    public override string GetRole()
    {
        return "Student";
    }
    public override void DisplayDashBoard()
    {
        Console.WriteLine($"Welcome, {name} to the student dashboard.");
    }
}