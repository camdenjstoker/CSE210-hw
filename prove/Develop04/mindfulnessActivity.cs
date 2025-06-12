public class MindfulnessActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;

    public MindfulnessActivity() : base("Mindfulness Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
        _prompts = new List<string>
        {
              "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
              "Why was this experience meaningful to you?",
            "How did you feel when it was complete?",
            "What did you learn about yourself through this experience?",
            "Have you ever done anything like this before?",
            "How can you keep this experience in mind in the future?",
            "What did you learn from this experience that applies to other situations?"
        };
    }

    public void RunActivity()
    {
        StartActivity(); // Inherited from base

        string prompt = GetRandomPrompt();
        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine("\nWhen you have something in mind, press Enter to Continue.");
        Console.ReadLine();

        int interval = 0;
        while (interval < GetDuration())
        {
            string question = GetRandomQuestion();
            Console.Write($"\n> {question}");
            DisplaySpinner(5);
            interval += 5;
        }

        EndActivity();
    }

    private string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_prompts.Count);
        return _prompts[index];
    }

    private string GetRandomQuestion()
    {
        Random rand = new Random();
        int index = rand.Next(_questions.Count);
        return _questions[index];
    }
}