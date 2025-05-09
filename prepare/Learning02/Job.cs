using System;

public class Jobs
{
    public string _jobTitle;
    public string _companyName;
    public int _startYr;
    public int _endYr;

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_companyName}) {_startYr}-{_endYr}");
    }
}