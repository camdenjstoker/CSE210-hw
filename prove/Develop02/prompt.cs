using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private List<string> _prompts;
    private Random _random;

    public PromptGenerator()
    {
        // Prompts list
        _prompts = new List<string>()
        {
            "What is one miracle you saw today? ",
            "If you could do something different today, what would it be? ",
            "What is something new you learned today? ",
            "What is the funniest thing that happened today? ",
            "How was your day and why? "
        };
        _random = new Random();
    }

    // Actual randomizer of prompts
    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}
