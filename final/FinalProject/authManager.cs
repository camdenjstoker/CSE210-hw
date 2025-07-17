using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class AuthManager
{
    private List<User> _users = new List<User>();
    private const string UsersFilePath = "users.csv";

    public AuthManager()
    {
        LoadUsersFromFile();

        if (!_users.Any())
        {
            RegisterUser("Alice Smith", "alice", "pass123", "Student");
            RegisterUser("Bob Johnson", "bob", "pass456", "Instructor");
            SaveUsersToFile();
        }
    }

    public bool RegisterUser(string name, string id, string password, string role)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
        {
            Console.WriteLine("All fields are required for registration.");
            return false;
        }

        if (_users.Any(u => u.GetID().Equals(id, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"User with ID '{id}' already exists.");
            return false;
        }

        User newUser;
        string passwordHash = password;

        if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
        {
            newUser = new Student(name, id, passwordHash);
        }
        else if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
        {
            newUser = new Instructor(name, id, passwordHash);
        }
        else
        {
            Console.WriteLine("Invalid role specified. User not registered.");
            return false;
        }

        _users.Add(newUser);
        SaveUsersToFile();
        Console.WriteLine($"User '{name}' registered successfully as {role}.");
        return true;
    }

    public User Login(string id, string password)
    {
        User user = _users.FirstOrDefault(u => u.GetID().Equals(id, StringComparison.OrdinalIgnoreCase));

        if (user != null && user.GetPasswordHash() == password)
        {
            return user;
        }
        Console.WriteLine("Invalid ID or password.");
        return null;
    }

    private void SaveUsersToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(UsersFilePath))
            {
                foreach (var user in _users)
                {
                    writer.WriteLine($"{user.GetName()},{user.GetID()},{user.GetRole()},{user.GetPasswordHash()}");
                }
            }
            Console.WriteLine("Users saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users to file: {ex.Message}");
        }
    }

    private void LoadUsersFromFile()
    {
        if (File.Exists(UsersFilePath))
        {
            try
            {
                foreach (string line in File.ReadLines(UsersFilePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        string name = parts[0];
                        string id = parts[1];
                        string role = parts[2];
                        string passwordHash = parts[3];

                        User loadedUser;
                        if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
                        {
                            loadedUser = new Student(name, id, passwordHash);
                        }
                        else if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
                        {
                            loadedUser = new Instructor(name, id, passwordHash);
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Unknown role '{role}' found for user '{id}' during load. User skipped.");
                            continue;
                        }
                        _users.Add(loadedUser);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Malformed line in {UsersFilePath}: {line}. Skipping.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users from file: {ex.Message}");
            }
        }
    }
}