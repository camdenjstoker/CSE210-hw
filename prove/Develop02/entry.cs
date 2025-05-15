using System;

public class Entry
{
   public string Date;
   public string Prompt;
   public string Response;

   public Entry(string date, string prompt, string response)
   {
    Date = date;
    Prompt = prompt;
    Response = response;
   }

   public override string ToString()
   {
    return $"{Date} - {Prompt}\n{Response}\n";
   }

}