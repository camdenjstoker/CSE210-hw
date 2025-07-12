
public class ShortQuestion : Question
{
    public ShortQuestion(string text, int points, string correctAnswer) : base(text, points, correctAnswer)
    {

    }
    
    public override void Ask()
    {
        Console.WriteLine($"Short Answer: {GetText()}.");
        Console.Write("Your answer: ");
    }

    public override bool Grade(string userAnswer)
    {
        return userAnswer.Trim().Equals(GetCorrectAnswer().Trim(), StringComparison.OrdinalIgnoreCase);
    }
}