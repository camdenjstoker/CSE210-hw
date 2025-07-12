 
public class Instructor : User
{
    public Instructor(string name, string id, string passwordHash) : base(name, id, passwordHash)
    {
        
    }

    public override string GetRole()
    {
        return "Instructor";
    }
    public override void DisplayDashBoard()
    {
        Console.WriteLine($"Welcome, {_name} to the Instructor dashboard.");
    }
}
 