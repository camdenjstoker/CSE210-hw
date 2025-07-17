using System;

public abstract class User
{
    protected string _name;
    protected string _id;
    protected string _passwordHash;

    protected User(string name, string id, string passwordHash)
    {
        _name = name;
         _id = id;
        _passwordHash = passwordHash;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetPasswordHash()
{
    return _passwordHash;
}

    public bool SetName(string newName)
    {
        // Validation logic
        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Error: Name cannot be null or empty. Name was not set.");
            return false;
        }

        // If validation passes, set the name
        _name = newName;
        Console.WriteLine($"Name successfully set to: {_name}");
        return true;
    }


    public string GetID()
    {
        return _id;
    }

    public virtual bool authenticator(string enteredPassword)
    {
        return _passwordHash == enteredPassword;
    }

    public abstract void DisplayDashBoard();

    public abstract string GetRole();



}