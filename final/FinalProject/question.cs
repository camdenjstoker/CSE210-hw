using System;

public abstract class Question
{
    protected string _text;
    protected int _point;
    protected string _correctAnswer;
    protected DateTime _dateMade;
    protected DateTime _dateLastEdited;

    public Question(string text, int points, string correctAnswer)
    {
        _text = text;
        _point = points;
        _correctAnswer = correctAnswer;
        _dateMade = DateTime.Now;
        _dateLastEdited = DateTime.Now;
    }

    public Question(string text, int points, string correctAnswer, DateTime dateMade, DateTime dateLastEdited)
    {
        _text = text;
        _point = points;
        _correctAnswer = correctAnswer;
        _dateMade = dateMade;
        _dateLastEdited = dateLastEdited;
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

    public DateTime GetDateMade()
    {
        return _dateMade;
    }

    public DateTime GetDateLastEdited()
    {
        return _dateLastEdited;
    }

    public void SetText(string text)
    {
        _text = text;
        _dateLastEdited = DateTime.Now;
    }
    public void SetPoints(int points)
    {
        _point = points;
        _dateLastEdited = DateTime.Now;
    }
    public void SetCorrectAnswer(string answer)
    {
        _correctAnswer = answer;
        _dateLastEdited = DateTime.Now;
    }

    public void SetDateLastEdited(DateTime date)
    {
        _dateLastEdited = date;
    }

    public abstract void Ask();
    public abstract bool Grade(string userAnswer);
}