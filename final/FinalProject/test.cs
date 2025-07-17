using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Test
{
    protected string _title;
    protected List<Question> _questions;
    protected int _lastAttemptScore;
    public Test(string title, List<Question> questions)
    {
        _title = title;
        _questions = new List<Question>(questions);
        _lastAttemptScore = 0;
    }

    public string GetTitle()
    {
        return _title;
    }

    public List<Question> GetQuestions()
    {
        return new List<Question>(_questions);
    }

    public List<Question> GetAllQuestions()
    {
        return new List<Question>(_questions);
    }

    public abstract int ConductTest();
    public abstract int CalculateGrade();

    public int GetTotalPossiblePoints()
    {
        return _questions.Sum(q => q.GetPoints());
    }
}