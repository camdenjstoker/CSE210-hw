using System;
using System.Collections.Generic;

public class MultipleQuestion : Question
{
    private List<string> _choices;

    public MultipleQuestion(string text, int points, string correctAnswer, List<string> choices) : base(text, points, correctAnswer)
    {
        _choices = new List<string> (choices);
    }

    public List<string> GetChoices() => new List<string>(_choices);
    public void SetChoices(List<string> choices) => _choices = new List<string>(choices);

    public override void Ask()
    {
        Console.WriteLine($"(Multiple Choice) {GetText()}");
        for (int i = 0; i < _choices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_choices[i]}");
        }
        Console.Write("Enter the number of your choice: ");
    }

    public override bool Grade(string userAnswer)
    {
        if (int.TryParse(userAnswer, out int choiceNum)
            && choiceNum >= 1 
            && choiceNum <= _choices.Count)
        {
            string selected = _choices[choiceNum - 1];
            return selected.Equals(GetCorrectAnswer(), StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
}