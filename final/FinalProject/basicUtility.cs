using System;

public class BasicUtility : ITimeUtility
{
    public TimeSpan GetOverallTestTimeSpan(Test test, int attemptNumber)
    {
        if (test is Exam exam)
        {
            // For an Exam, the duration is its set time limit
            return TimeSpan.FromMinutes(exam.GetTimeLimitMinutes());
        }
        else if (test is Quiz quiz)
        {
            return TimeSpan.FromMinutes(30); // Example: 30 minutes per quiz
        }
        
        return TimeSpan.FromMinutes(60); 
    }
}