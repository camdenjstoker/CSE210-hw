using System;

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract string GetSaveString();
    public abstract int RecordEvent();

    public virtual int GetPoints()
    {
        return _points;
    }

    public abstract string GetStringRepresentation();


    public string GetShortName()
    {
        return _shortName;
    }

    public string GetDescription()
    {
        return _description;
    }
}