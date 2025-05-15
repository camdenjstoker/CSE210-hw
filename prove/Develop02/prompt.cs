using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private List<string> prompts;
    private Random random;

    public PromptGenerator()
    {
        prompts = new List<string>()
        {
            "What is one miracle you saw today? ",
            "If you could do something different today, what would it be? ",
            "What is something new you learned today? ",
            "What is the funniest thing that happened today? ",
            "How was your day and why? "
        };
        random = new Random();
    }

    public string GetRandomPrompt()
    {
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}