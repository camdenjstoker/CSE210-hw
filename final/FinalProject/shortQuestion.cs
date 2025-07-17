public class ShortQuestion : Question
{
    public ShortQuestion(string text, int points, string correctAnswer) : base(text, points, correctAnswer)
    {

    }

    public ShortQuestion(string text, int points, string correctAnswer, System.DateTime dateMade, System.DateTime dateLastEdited) : base(text, points, correctAnswer, dateMade, dateLastEdited)
    {

    }
    
    public override void Ask()
    {
        Console.WriteLine($"Short Answer: {GetText()}.");
        Console.Write("Your answer: ");
    }

    public override bool Grade(string userAnswer)
    {
        return userAnswer.Trim().Equals(GetCorrectAnswer().Trim(), System.StringComparison.OrdinalIgnoreCase);
    }
}