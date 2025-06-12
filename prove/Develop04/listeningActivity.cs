public class ListeningActivity : Activity
{
    private List<string> _reflectionPrompts;

    public ListeningActivity() : base("Listening Activity", "This activity will help you be mindful by focusing on the sounds you hear around you.")
    {
        _reflectionPrompts = new List<string>
        {
            "What did you hear that you hadn't noticed before?",
            "Did any sound surprise you?",
            "Were there any sounds that calmed or distracted you?",
            "How did focusing on sound change your awareness?"
        };
    }

    public void RunActivity()
    {
        StartActivity();

        Console.WriteLine("\nYou will now have time to close your eyes and simply listen.");
        Console.Write("The session begins in: ");
        DisplayCountdown(5);

        Console.Clear();
        Console.WriteLine("Start listening now...");
        DisplaySpinner(GetDuration());  // Spinner acts as silent time while user listens

        Console.Clear();
        Console.WriteLine("Now take a moment to reflect.");
        string prompt = GetRandomPrompt();
        Console.WriteLine($"--- {prompt} ---");
        DisplaySpinner(5);  // Give user some time to reflect

        EndActivity();
    }

    private string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_reflectionPrompts.Count);
        return _reflectionPrompts[index];
    }


}
