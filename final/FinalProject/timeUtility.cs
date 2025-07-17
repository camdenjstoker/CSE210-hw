using System;

public interface ITimeUtility
{
    TimeSpan GetOverallTestTimeSpan(Test test, int attemptNumber);
}