// questionBank.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class QuestionBank
{
    private List<Question> _questions = new List<Question>();
    private const string QuestionsFilePath = "questions.csv";

    public QuestionBank()
    {
        LoadQuestionsFromFile();

        if (!_questions.Any())
        {
            AddQuestion(new ShortQuestion("What is the capital of France?", 5, "Paris"));
            AddQuestion(new MultipleQuestion("Which planet is known as the Red Planet?", 10, "Mars", new List<string> { "Earth", "Mars", "Jupiter", "Venus" }));
            AddQuestion(new ShortQuestion("What is 2 + 2?", 2, "4"));
            SaveQuestionsToFile();
        }
    }

    public void AddQuestion(Question question)
    {
        _questions.Add(question);
        Console.WriteLine("Question added to the bank.");
        SaveQuestionsToFile();
    }

    public Question GetRandomQuestion()
    {
        if (!_questions.Any())
        {
            Console.WriteLine("Question bank is empty.");
            return null;
        }
        Random rand = new Random();
        return _questions[rand.Next(_questions.Count)];
    }

    public List<Question> GetRandomQuestions(int count)
    {
        if (count <= 0 || !_questions.Any())
        {
            return new List<Question>();
        }

        Random rand = new Random();
        return _questions.OrderBy(q => rand.Next()).Take(count).ToList();
    }

    public List<Question> GetAllQuestions()
    {
        return new List<Question>(_questions);
    }

    public void RemoveQuestion(Question question)
    {
        if (_questions.Remove(question))
        {
            Console.WriteLine("Question removed from the bank.");
            SaveQuestionsToFile();
        }
        else
        {
            Console.WriteLine("Question not found in the bank.");
        }
    }

    public void SaveQuestionsToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(QuestionsFilePath))
            {
                foreach (var question in _questions)
                {
                    string type = question.GetType().Name;
                    string text = EscapeCsvField(question.GetText());
                    string correctAnswer = EscapeCsvField(question.GetCorrectAnswer());
                    string dateMade = question.GetDateMade().ToString("o");
                    string dateLastEdited = question.GetDateLastEdited().ToString("o");

                    if (question is MultipleQuestion mq)
                    {
                        string choices = EscapeCsvField(string.Join(";", mq.GetChoices()));
                        writer.WriteLine($"{type},{text},{question.GetPoints()},{correctAnswer},{choices},{dateMade},{dateLastEdited}");
                    }
                    else
                    {
                        writer.WriteLine($"{type},{text},{question.GetPoints()},{correctAnswer},,{dateMade},{dateLastEdited}");
                    }
                }
            }
            Console.WriteLine("Questions saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving questions to file: {ex.Message}");
        }
    }

    private void LoadQuestionsFromFile()
    {
        if (File.Exists(QuestionsFilePath))
        {
            try
            {
                foreach (string line in File.ReadLines(QuestionsFilePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length < 5)
                    {
                        Console.WriteLine($"Warning: Malformed line in {QuestionsFilePath}: {line}. Skipping.");
                        continue;
                    }

                    string type = parts[0];
                    string text = UnescapeCsvField(parts[1]);
                    if (!int.TryParse(parts[2], out int points))
                    {
                        Console.WriteLine($"Warning: Invalid points in line: {line}. Skipping.");
                        continue;
                    }
                    string correctAnswer = UnescapeCsvField(parts[3]);
                    DateTime dateMade = DateTime.Now;
                    DateTime dateLastEdited = DateTime.Now;

                    if (parts.Length > 5 && DateTime.TryParse(parts[5], out DateTime parsedDateMade))
                    {
                        dateMade = parsedDateMade;
                    }
                    if (parts.Length > 6 && DateTime.TryParse(parts[6], out DateTime parsedDateLastEdited))
                    {
                        dateLastEdited = parsedDateLastEdited;
                    }


                    Question loadedQuestion = null;
                    if (type == nameof(ShortQuestion))
                    {
                        loadedQuestion = new ShortQuestion(text, points, correctAnswer, dateMade, dateLastEdited);
                    }
                    else if (type == nameof(MultipleQuestion))
                    {
                        if (parts.Length < 7)
                        {
                            Console.WriteLine($"Warning: Malformed MultipleChoice line in {QuestionsFilePath}: {line}. Skipping.");
                            continue;
                        }
                        List<string> choices = UnescapeCsvField(parts[4]).Split(';').Select(c => c.Trim()).ToList();
                        loadedQuestion = new MultipleQuestion(text, points, correctAnswer, choices, dateMade, dateLastEdited);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Unknown question type '{type}' found. Skipping.");
                    }

                    if (loadedQuestion != null)
                    {
                        _questions.Add(loadedQuestion);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading questions from file: {ex.Message}");
            }
        }
    }

    private string EscapeCsvField(string field)
    {
        if (field.Contains(",") || field.Contains(";") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }

    private string UnescapeCsvField(string field)
    {
        if (field.StartsWith("\"") && field.EndsWith("\""))
        {
            return field.Substring(1, field.Length - 2).Replace("\"\"", "\"");
        }
        return field;
    }
}