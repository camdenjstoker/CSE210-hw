using System;

public class Entry
{
   public string _Date;
   public string _Prompt;
   public string _Response;

   // Formatting the entries
   public Entry(string date, string prompt, string response)
   {
    _Date = date;
    _Prompt = prompt;
    _Response = response;
   }

   // Putting the code to work
   public override string ToString()
   {
    return $"{_Date} - {_Prompt}\n{_Response}\n";
   }

}
