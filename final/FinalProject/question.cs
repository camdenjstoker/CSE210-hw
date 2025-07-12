using System;

public abstract class Question
{
    protected string _text;

    protected int _point;

    protected string _correctAnswer;

    public Question(string text, int points, string correctAnswer)
    {
        _text = text;
        _point = points;
        _correctAnswer = correctAnswer;
    }

    public string GetText()
    {
        return _text;
    }
    public int GetPoints()
    {
        return _point;
    }
    public string GetCorrectAnswer()
    {
        return _correctAnswer;
    }

    public void SetText(string text)
    {
        _text = text;

    }
    public void SetPoints(int points)
    {
        _point = points;
    }
    public void SetCorrectAnswer(string answer)
    {
        _correctAnswer = answer;
    }

    public abstract void Ask();
    public abstract bool Grade(string userAnswer);

}

