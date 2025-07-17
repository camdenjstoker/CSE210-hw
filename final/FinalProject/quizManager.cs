using System;
using System.Collections.Generic;
using System.Linq;

public class QuizManager
{
    private AuthManager _authManager;
    private QuestionBank _questionBank;
    private User _currentUser;
    private List<Test> _assessments = new List<Test>();
    private ITimeUtility _timeUtility;

    public QuizManager(AuthManager authManager, QuestionBank questionBank, ITimeUtility timeUtility)
    {
        _authManager = authManager;
        _questionBank = questionBank;
        _timeUtility = timeUtility;

        if (!_assessments.Any())
        {
            _assessments.Add(new Quiz("Basic C# Quiz", _questionBank.GetRandomQuestions(3), 2));
            _assessments.Add(new Exam("Advanced C# Exam", _questionBank.GetRandomQuestions(5), 60));
        }
    }

    public void Run()
    {
        Console.WriteLine("\n--- Login / Register ---");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.Write("Enter your choice: ");
        string authChoice = Console.ReadLine();

        if (authChoice == "1")
        {
            Console.Write("Enter User ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            _currentUser = _authManager.Login(id, password);
        }
        else if (authChoice == "2")
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New User ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter New Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Role (Student/Instructor): ");
            string role = Console.ReadLine();
            bool registrationSuccess = _authManager.RegisterUser(name, id, password, role);
            if (registrationSuccess)
            {
                _currentUser = _authManager.Login(id, password);
            }
            else
            {
                _currentUser = null;
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Returning to main menu.");
            return;
        }

        if (_currentUser != null)
        {
            Console.WriteLine("\nLogin successful!");
            while (_currentUser != null)
            {
                _currentUser.DisplayDashBoard();

                if (_currentUser.GetRole().Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    StudentDashboard();
                }
                else if (_currentUser.GetRole().Equals("Instructor", StringComparison.OrdinalIgnoreCase))
                {
                    InstructorDashboard();
                }
            }
        }
        else
        {
            Console.WriteLine("Authentication failed. Please try again.");
        }
    }

    private void StudentDashboard()
    {
        while (true)
        {
            Console.WriteLine("\n--- Student Dashboard ---");
            Console.WriteLine("1. View Available Assessments");
            Console.WriteLine("2. Take Assessment");
            Console.WriteLine("3. Logout");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewAvailableAssessments();
                    break;
                case "2":
                    TakeAssessment();
                    break;
                case "3":
                    _currentUser = null;
                    Console.WriteLine("Logged out successfully.");
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void InstructorDashboard()
    {
        while (true)
        {
            Console.WriteLine("\n--- Instructor Dashboard ---");
            Console.WriteLine("1. Manage Questions");
            Console.WriteLine("2. Create New Assessment");
            Console.WriteLine("3. View All Assessments");
            Console.WriteLine("4. Logout");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManageQuestions();
                    break;
                case "2":
                    CreateNewAssessment();
                    break;
                case "3":
                    ViewAllAssessments();
                    break;
                case "4":
                    _currentUser = null;
                    Console.WriteLine("Logged out successfully.");
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void ViewAvailableAssessments()
    {
        Console.WriteLine("\n--- Available Assessments ---");
        if (!_assessments.Any())
        {
            Console.WriteLine("No assessments are currently available.");
            return;
        }

        for (int i = 0; i < _assessments.Count; i++)
        {
            Test test = _assessments[i];
            Console.WriteLine($"{i + 1}. Title: {test.GetTitle()} ({test.GetType().Name})");
            TimeSpan duration = _timeUtility.GetOverallTestTimeSpan(test, 1);
            Console.WriteLine($"   Planned Duration: {duration.TotalMinutes} minutes");
            Console.WriteLine($"   Questions: {test.GetQuestions().Count}");
            Console.WriteLine($"   Total Possible Points: {test.GetTotalPossiblePoints()}");
            if (test is Quiz quiz)
            {
                Console.WriteLine($"   Max Attempts: {quiz.GetMaxAttempts()}");
            }
            else if (test is Exam exam)
            {
                Console.WriteLine($"   Time Limit (min): {exam.GetTimeLimitMinutes()}");
            }
        }
    }

    private void TakeAssessment()
    {
        Console.WriteLine("\n--- Take an Assessment ---");
        ViewAvailableAssessments();
        if (!_assessments.Any())
        {
            return;
        }

        Console.Write("Enter the number of the assessment you want to take: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _assessments.Count)
        {
            Test selectedAssessment = _assessments[choice - 1];
            Console.WriteLine($"\nStarting assessment: {selectedAssessment.GetTitle()}");

            int score = selectedAssessment.ConductTest();
            Console.WriteLine($"\nAssessment completed. Your score: {score} out of {selectedAssessment.GetTotalPossiblePoints()}");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    private void ManageQuestions()
    {
        while (true)
        {
            Console.WriteLine("\n--- Manage Questions ---");
            Console.WriteLine("1. Add New Question");
            Console.WriteLine("2. View All Questions");
            Console.WriteLine("3. Edit Question");
            Console.WriteLine("4. Remove Question");
            Console.WriteLine("5. Back to Instructor Dashboard");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddNewQuestion();
                    break;
                case "2":
                    ViewAllQuestions();
                    break;
                case "3":
                    EditQuestion();
                    break;
                case "4":
                    RemoveQuestion();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private void AddNewQuestion()
    {
        Console.WriteLine("\n--- Add New Question ---");
        Console.Write("Enter question text: ");
        string text = Console.ReadLine();

        Console.Write("Enter points for this question: ");
        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points. Question not added.");
            return;
        }

        Console.Write("Enter correct answer: ");
        string correctAnswer = Console.ReadLine();

        Console.Write("Is this a (1)Short Answer or (2)Multiple Choice question? Enter 1 or 2: ");
        string typeChoice = Console.ReadLine();

        Question newQuestion = null;
        if (typeChoice == "1")
        {
            newQuestion = new ShortQuestion(text, points, correctAnswer);
        }
        else if (typeChoice == "2")
        {
            Console.Write("Enter choices separated by semicolons (e.g., choice1;choice2;choice3): ");
            List<string> choices = Console.ReadLine().Split(';').Select(c => c.Trim()).ToList();
            newQuestion = new MultipleQuestion(text, points, correctAnswer, choices);
        }
        else
        {
            Console.WriteLine("Invalid question type. Question not added.");
            return;
        }

        if (newQuestion != null)
        {
            _questionBank.AddQuestion(newQuestion);
            Console.WriteLine("Question added successfully!");
        }
    }

    private void ViewAllQuestions()
    {
        Console.WriteLine("\n--- All Questions in Bank ---");
        List<Question> allQuestions = _questionBank.GetAllQuestions();
        if (!allQuestions.Any())
        {
            Console.WriteLine("Question bank is empty.");
            return;
        }

        for (int i = 0; i < allQuestions.Count; i++)
        {
            Question q = allQuestions[i];
            Console.WriteLine($"\n{i + 1}. Type: {q.GetType().Name}");
            Console.WriteLine($"   Text: {q.GetText()}");
            Console.WriteLine($"   Points: {q.GetPoints()}");
            Console.WriteLine($"   Correct Answer: {q.GetCorrectAnswer()}");
            if (q is MultipleQuestion mq)
            {
                Console.WriteLine($"   Choices: {string.Join(" | ", mq.GetChoices())}");
            }
            Console.WriteLine($"   Date Made: {q.GetDateMade().ToShortDateString()}");
            Console.WriteLine($"   Last Edited: {q.GetDateLastEdited().ToShortDateString()}");
            Console.WriteLine("-----------------------------------");
        }
    }

    private void EditQuestion()
    {
        Console.WriteLine("\n--- Edit Question ---");
        ViewAllQuestions();
        List<Question> allQuestions = _questionBank.GetAllQuestions();

        if (!allQuestions.Any())
        {
            return;
        }

        Console.Write("Enter the number of the question you want to edit: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= allQuestions.Count)
        {
            Question questionToEdit = allQuestions[choice - 1];

            Console.WriteLine($"\nEditing: {questionToEdit.GetText()}");

            Console.Write($"Enter new text (current: '{questionToEdit.GetText()}'): ");
            string newText = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newText))
            {
                questionToEdit.SetText(newText);
            }

            Console.Write($"Enter new points (current: {questionToEdit.GetPoints()}): ");
            if (int.TryParse(Console.ReadLine(), out int newPoints))
            {
                questionToEdit.SetPoints(newPoints);
            }

            Console.Write($"Enter new correct answer (current: '{questionToEdit.GetCorrectAnswer()}'): ");
            string newAnswer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAnswer))
            {
                questionToEdit.SetCorrectAnswer(newAnswer);
            }

            if (questionToEdit is MultipleQuestion mq)
            {
                Console.Write($"Enter new choices separated by semicolons (current: {string.Join(";", mq.GetChoices())}): ");
                string newChoicesStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newChoicesStr))
                {
                    List<string> newChoices = newChoicesStr.Split(';').Select(c => c.Trim()).ToList();
                    mq.SetChoices(newChoices);
                }
            }

            Console.WriteLine("Question updated successfully!");
            _questionBank.SaveQuestionsToFile();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    private void RemoveQuestion()
    {
        Console.WriteLine("\n--- Remove Question ---");
        ViewAllQuestions();
        List<Question> allQuestions = _questionBank.GetAllQuestions();

        if (!allQuestions.Any())
        {
            return;
        }

        Console.Write("Enter the number of the question you want to remove: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= allQuestions.Count)
        {
            Question questionToRemove = allQuestions[choice - 1];
            _questionBank.RemoveQuestion(questionToRemove);
            Console.WriteLine("Question removed successfully!");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    private void CreateNewAssessment()
    {
        Console.WriteLine("\n--- Create New Assessment ---");
        Console.Write("Enter assessment title: ");
        string title = Console.ReadLine();

        Console.Write("Enter number of questions for this assessment: ");
        if (!int.TryParse(Console.ReadLine(), out int numQuestions) || numQuestions <= 0)
        {
            Console.WriteLine("Invalid number of questions. Assessment not created.");
            return;
        }

        List<Question> questionsForAssessment = _questionBank.GetRandomQuestions(numQuestions);
        if (!questionsForAssessment.Any() || questionsForAssessment.Count < numQuestions)
        {
            Console.WriteLine($"Could not retrieve enough questions. Only {questionsForAssessment.Count} questions available. Assessment not created.");
            return;
        }

        Console.Write("Is this a (1)Quiz or (2)Exam? Enter 1 or 2: ");
        string typeChoice = Console.ReadLine();

        Test newAssessment = null;
        if (typeChoice == "1")
        {
            Console.Write("Enter maximum number of attempts for this Quiz: ");
            if (int.TryParse(Console.ReadLine(), out int maxAttempts) && maxAttempts >= 1)
            {
                newAssessment = new Quiz(title, questionsForAssessment, maxAttempts);
            }
            else
            {
                Console.WriteLine("Invalid max attempts. Quiz not created.");
            }
        }
        else if (typeChoice == "2")
        {
            Console.Write("Enter time limit in minutes for this Exam: ");
            if (int.TryParse(Console.ReadLine(), out int timeLimitMinutes) && timeLimitMinutes > 0)
            {
                newAssessment = new Exam(title, questionsForAssessment, timeLimitMinutes);
            }
            else
            {
                Console.WriteLine("Invalid time limit. Exam not created.");
            }
        }
        else
        {
            Console.WriteLine("Invalid assessment type.");
            return;
        }

        if (newAssessment != null)
        {
            _assessments.Add(newAssessment);
            Console.WriteLine($"Assessment '{title}' created successfully!");
        }
    }

    private void ViewAllAssessments()
    {
        Console.WriteLine("\n--- All Created Assessments ---");
        if (!_assessments.Any())
        {
            Console.WriteLine("No assessments have been created.");
            return;
        }

        for (int i = 0; i < _assessments.Count; i++)
        {
            Test test = _assessments[i];
            Console.WriteLine($"{i + 1}. Title: {test.GetTitle()}");
            Console.WriteLine($"   Type: {test.GetType().Name}");
            TimeSpan duration = _timeUtility.GetOverallTestTimeSpan(test, 1);
            Console.WriteLine($"   Planned Duration: {duration.TotalMinutes} minutes");
            Console.WriteLine($"   Questions: {test.GetQuestions().Count}");
            Console.WriteLine($"   Total Possible Points: {test.GetTotalPossiblePoints()}");
            if (test is Quiz quiz)
            {
                Console.WriteLine($"   Max Attempts: {quiz.GetMaxAttempts()}");
            }
            else if (test is Exam exam)
            {
                Console.WriteLine($"   Time Limit (min): {exam.GetTimeLimitMinutes()}");
            }
        }
    }
}