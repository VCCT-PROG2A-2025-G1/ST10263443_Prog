using chatBotLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static chatBotLib.Lists;
using static chatBotLib.TaskGetters;

namespace chatBotLib
{
    public class Methods
    {
        public static Action<string> PrintOutput = text => Console.WriteLine(text);
        public static Action ShowTaskManagerWindow;
        public static Action ShowQuizWindow;
        public static Action showActivityLog;
        public static string UserName = "User";
        private static List<TQuestion> questions;
        private static int currentQuestionIndex;
        private static int score;
        public static Func<string, string> PromptUser = prompt =>
        {
            Console.Write(prompt);
            return Console.ReadLine();
        };

        public static string defaultEmotion = "nothing"; // Tracks user sentiment
        public static HashSet<string> GetUserInterest = new HashSet<string>(); // Stores unique interests

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Properties to hold various states and collections related to tasks
        public static ObservableCollection<TaskGetters> Tasks { get; set; } = new ObservableCollection<TaskGetters>();
        public static bool IsAddingTask { get; set; } // Changed from private set to public set
        public static bool AskingForDescription { get; private set; }
        public static bool AskingForDate { get; private set; }
        public static string TempTitle { get; private set; }
        public static string TempDescription { get; private set; }
        public static DateTime? TempDate { get; private set; }
        public static TimeSpan? TempTime { get; private set; }
        public static bool WaitingForDate { get; private set; }
        public static bool AskingForTime { get; private set; }
        public static bool WaitingForTime { get; private set; }
        public static bool IsWaitingForDescription { get; private set; }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to detect user sentiment based on input keywords
        public static string GetSentimentDetection(string userInput)
        {
            userInput = userInput.ToLower(); // Normalize input to lowercase
            if (userInput.Contains("worried") || userInput.Contains("anxious") || userInput.Contains("nervous"))
                return "worried";
            if (userInput.Contains("curious"))
                return "curious";
            if (userInput.Contains("frustrated") || userInput.Contains("annoyed") || userInput.Contains("irritated"))
                return "frustrated";

            return "nothing"; // Default sentiment
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to get the current topic from user input by checking against known topics
        public static string GetCurrentTopic(string input)
        {
            foreach (var topic in Dictionaries.InterestedIn.Keys)
            {
                if (input.IndexOf(topic, StringComparison.OrdinalIgnoreCase) >= 0) // Case-insensitive check
                {
                    return topic;
                }
            }
            return null; // No topic found
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to handle user sentiments and provide appropriate responses based on the last topic discussed
        public static bool HandleSentiments(string userResponse, string lastTopic, string usersName, out string newUserResponse)
        {
            newUserResponse = null;
            string sentiment = GetSentimentDetection(userResponse); // Detect sentiment from user response

            if (sentiment == "nothing" || sentiment == defaultEmotion)
                return false;

            defaultEmotion = sentiment;

            string response = null;

            switch (sentiment) // Switch case to handle different sentiments
            {
                case "worried":
                    response = $"Don't worry, it's natural to be worried about {lastTopic}. Here are some tips to help.";
                    break;
                case "curious":
                    response = $"Since you're curious about {lastTopic}, here are some tips.";
                    break;
                case "frustrated":
                    response = $"Your frustration about {lastTopic} is valid. Let's take it one step at a time.";
                    break;
                default:
                    response = null;
                    break;
            }
            if (response != null) // If a response is generated based on sentiment
            {
                PrintOutput?.Invoke("CyberBot: " + response);

                if (Dictionaries.InterestedIn.ContainsKey(lastTopic)) // Check if the last topic is in the dictionary
                {
                    string tip = Dictionaries.InterestedIn[lastTopic].Invoke();
                    PrintOutput?.Invoke("CyberBot: " + tip);
                }
                newUserResponse = PromptUser?.Invoke($"{usersName}: ");
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to handle user interest in specific topics by checking if the topic is already known
        public static bool HandleUserInterest(ref string userResponse, string usersName)
        {
            if (!userResponse.Contains("interested in")) // Check if the user response contains the phrase "interested in"
                return false;

            int startIndex = userResponse.IndexOf("interested in") + "interested in".Length;
            string interestedTopic = userResponse.Substring(startIndex).Trim();

            if (!GetUserInterest.Contains(interestedTopic)) // Check if the topic is not already known
            {
                GetUserInterest.Add(interestedTopic);
                string confirmation = $"Okay, I'll remember that you're interested in {interestedTopic}.";
                PrintOutput?.Invoke(confirmation);
            }
            else // If the topic is already known
            {
                string alreadyKnown = $"I already know you're interested in {interestedTopic}.";
                PrintOutput?.Invoke(alreadyKnown);
            }

            userResponse = PromptUser?.Invoke($"{usersName}: ");
            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to handle user interest in various topics by checking against a predefined dictionary
        public static void InterestHandler(string interestedIn) // Handles user interest in various topics
        {
            foreach (var key in Dictionaries.InterestedIn.Keys)
            {
                if (interestedIn.Contains(key)) // Check if the user's interest matches a key in the dictionary
                {
                    if (!GetUserInterest.Contains(key))
                    {
                        GetUserInterest.Add(key);
                        PrintOutput?.Invoke($"Okay, I'll remember that you're interested in {key}.");
                    }
                    return;
                }
            }
            PrintOutput?.Invoke("Sorry, I don't have information on that topic.");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to simulate thinking by the bot, printing a message and pausing for a short duration
        public static void Thinking()
        {
            PrintOutput?.Invoke("CyberBot: Pondering question...");
            Thread.Sleep(1200);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to respond with a list of tips, simulating a delay for each tip
        public static async Task RespondWithTipAsync(List<string> tips)
        {
            foreach (var tip in tips)
            {
                Thinking();
                PrintOutput?.Invoke("CyberBot: " + tip);
                await Task.Delay(500);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to center user input in the console window
        public static string CenteredUserInput(string givenPrompt)
        {
            int windowWidth = Console.WindowWidth;
            int promptPadding = (windowWidth - givenPrompt.Length) / 2;

            Console.SetCursorPosition(promptPadding, Console.CursorTop); 
            Console.Write(givenPrompt);

            string userInput = Console.ReadLine();
            return userInput;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to print a centered logo in the console window
        public static void PrintCenteredLogo(string text)
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = Math.Max(0, (windowWidth - text.Length) / 2);
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to print static text centered in the console window, simulating typing effect
        public static void PrintCenteredStaticText(string staticText)
        {
            int windowWidth = Console.WindowWidth;
            int textLength = staticText.Length;
            int leftPadding = Math.Max(0, (windowWidth - textLength) / 2);

            Console.SetCursorPosition(leftPadding, Console.CursorTop);

            Random rand = new Random();
            foreach (char c in staticText)
            {
                Console.Write(c);
                Thread.Sleep(rand.Next(20, 60)); // Simulated typing
            }

            Console.WriteLine();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to add a task with optional date and time, and log the activity
        public static void AddTask(string title, string desc, DateTime? date = null, TimeSpan? time = null)
        {
            if (!string.IsNullOrWhiteSpace(title)) // Check if the title is not empty or whitespace
                {
                    Tasks.Add(new TaskGetters
                {
                    TaskTitle = title,
                    TaskDescription = string.IsNullOrWhiteSpace(desc) ? "No description added" : desc,
                    TaskDate = date,
                    TaskTime = time,
                    IsTaskCompleted = false
                });
                ActivityTracker.Add($"Task '{title}' added to the list.");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to remove a created task from the list and log the activity
        public static void RemoveCreatedTask(TaskGetters task)
        {
            if (task != null && Tasks.Contains(task)) // Check if the task is not null and exists in the list
            {
                Tasks.Remove(task);
                ActivityTracker.Add($"Task '{task.TaskTitle}' removed from the list.");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to mark a task as completed, updating its status and logging the activity
        public static void CompleteTask(TaskGetters task)
        {
            if (task != null && Tasks.Contains(task)) // Check if the task is not null and exists in the list
            {
                task.IsTaskCompleted = true;
                ActivityTracker.Add($"Task '{task.TaskTitle}' marked as completed.");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to view all tasks in a formatted list, including their status, title, description, date, and time
        public static List<string> ViewTasks()
        {
            return Tasks.Select(t =>
            {
                string dateText = t.TaskDate.HasValue ? t.TaskDate.Value.ToString("dd-MM-yyyy") : "No date added";
                string timeText = t.TaskTime.HasValue ? t.TaskTime.Value.ToString(@"hh\:mm") : "No time added";
                string descText = string.IsNullOrEmpty(t.TaskDescription) ? "No description added" : t.TaskDescription;

                return $"{(t.IsTaskCompleted ? "[Yes]" : "[No]")} {t.TaskTitle} - {descText} | Due: {dateText} {timeText}";
            }).ToList();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to get the collection of tasks, allowing external access to the task list
        public static ObservableCollection<TaskGetters> GetTasks()
        {
            return Tasks;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to handle the task flow, guiding the user through adding a task with optional description, date, and time
        public static string TaskFlow(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null; // Check if input is empty or whitespace

            if (IsAddingTask) // Is the bot adding a task
            {
                string[] parts = input.Split(new[] { " - " }, 2, StringSplitOptions.None); // Split input into title and description
                TempTitle = parts[0].Trim();
                TempDescription = parts.Length > 1 ? parts[1].Trim() : "No description added"; // Set description if provided

                if (string.IsNullOrWhiteSpace(TempTitle))
                {
                    return "Please provide a title for the task (format: Title - Description). If you want to skip description, just enter the title.";
                }

                IsAddingTask = false;
                AskingForDate = true;
                return "Would you like to add a due date for this task?";
            }

            if (AskingForDate) // If the user adds a date
            {
                AskingForDate = false;
                if (input.ToLower().Contains("yes"))
                {
                    WaitingForDate = true;
                    return "Please enter the due date (format: dd-MM-yyyy)";
                }
                AddTask(TempTitle, TempDescription);
                return "Task added without a date.";
            }

            if (WaitingForDate) // If the user is waiting for a date input
            {
                if (DateTime.TryParse(input, out DateTime date))
                {
                    TempDate = date;
                    WaitingForDate = false;
                    AskingForTime = true;
                    return "Would you like to set a specific time?";
                }
                TempDescription = null;
                IsAddingTask = true;
                return "Invalid date format. Please try again (e.g., 10-06-2025).";
            }

            if (AskingForTime) // If the user is asked for a time input
            {
                AskingForTime = false;
                if (input.ToLower().Contains("yes"))
                {
                    WaitingForTime = true;
                    return "Please enter the time (format: HH:mm)";
                }
                AddTask(TempTitle, TempDescription, TempDate);
                return "Task added with date, no specific time.";
            }

            if (WaitingForTime) // If the user is waiting for a time input
            {
                if (TimeSpan.TryParse(input, out TimeSpan time))
                {
                    TempTime = time;
                    WaitingForTime = false;
                    AddTask(TempTitle, TempDescription, TempDate, TempTime);
                    return "Task added with date and time.";
                }
                return "Invalid time format. Please use HH:mm.";
            }

            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to clear temporary task state variables, resetting the task input flow
        private static void ClearTempTaskState()
        {
            TempTitle = null;
            TempDescription = null;
            TempDate = null;
            TempTime = null;
            IsAddingTask = false;
            AskingForDate = false;
            WaitingForDate = false;
            AskingForTime = false;
            WaitingForTime = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to handle natural language processing for task-related user input
        public static bool NLPReaction(string UserInput, out string title, out string desc, out DateTime? date)
        {
            title = null;
            desc = null;
            date = null;

            UserInput = UserInput.ToLower();

            foreach (var trigger in Lists.triggerWords) // Looks for trigger words in user input
            {
                if (UserInput.StartsWith(trigger))
                {
                    string details = UserInput.Substring(trigger.Length).Trim(); // Extracts details after the trigger word

                    foreach (var dateDetail in Dictionaries.DateTimeQuestions.Keys) // Checks for date-related keywords
                    {
                        if (details.Contains(dateDetail))
                        {
                            date = Dictionaries.DateTimeQuestions[dateDetail].Invoke();
                            details = details.Replace(dateDetail, "").Trim();
                            date = ExtractTimeFromInput(details, date.Value, out details);
                            break;
                        }
                    }
                    title = details;
                    return true;
                }
            }
            return false;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Method to initialize the quiz functionality, setting up questions and resetting the score
        public static void CyberQuizlerFunctions()
        {
            // Initialize quiz
            questions = TriviaQuestions.GetTQuestions();
            currentQuestionIndex = 0;
            score = 0;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to get the current question in the quiz
        public static TQuestion GetCurrentQuestion()
        {
            if (questions != null && currentQuestionIndex < questions.Count)
            {
                return questions[currentQuestionIndex];
            }

            return null;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to check the user's answer against the correct answer for the current question
        public static bool CheckAnswer(int selectedIndex)
        {
            TQuestion question = GetCurrentQuestion();

            if (question == null)
            {
                return false;
            }

            bool isCorrect = selectedIndex == question.CorrectOptionSelected;

            if (isCorrect)
            {
                score++;
            }

            currentQuestionIndex++;

            return isCorrect;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to check if the quiz is complete by checking if all questions have been answered
        public static bool IsQuizComplete()
        {
            return questions != null && currentQuestionIndex >= questions.Count;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to get the current score of the quiz
        public static int GetScore()
        {
            return score;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to reset the quiz, clearing questions and resetting the score
        public static int GetTotalQuestions()
        {
            if (questions != null)
            {
                return questions.Count;
            }

            return 0;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to get a question at a specific index, returning null if the index is out of bounds
        public static TQuestion GetQuestionAtIndex(int index)
        {
            if (questions != null && index >= 0 && index < questions.Count)
                return questions[index];
            return null;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to get the next question in the quiz, returning null if there are no more questions
        public static TQuestion GetPreviousQuestion()
        {
            return GetQuestionAtIndex(currentQuestionIndex - 1);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to get the current index of the question being answered in the quiz
        public static int GetCurrentIndex()
        {
            return currentQuestionIndex;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Class to track activities, storing a log of actions performed by the bot
        public static class ActivityTracker
        {
            private static readonly List<string> log = new List<string>();

            public static void Add(string description)
            {
                string entry = $"{DateTime.Now:HH:mm:ss} - {description}";
                log.Add(entry);

                // Keep only the last 10 entries
                if (log.Count > 10)
                {
                    log.RemoveAt(0);
                }
            }

            public static List<string> GetLog()
            {
                return new List<string>(log); // Return a copy
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // Method to extract time from user input, adjusting the time based on AM/PM notation and returning a DateTime object
        public static DateTime? ExtractTimeFromInput(string input, DateTime UserAddedDate, out string UserInput)
        {
            UserInput = input;
            var timeMatch = Regex.Match(input, @"\bat (\d{1,2})(:(\d{2}))?\s*(am|pm)?\b");

            if (timeMatch.Success)
            {
                int hour = int.Parse(timeMatch.Groups[1].Value);
                int minute = timeMatch.Groups[3].Success ? int.Parse(timeMatch.Groups[3].Value) : 0;
                string ampm = timeMatch.Groups[4].Value;

                if (ampm == "pm" && hour < 12) hour += 12;
                if (ampm == "am" && hour == 12) hour = 0;

                UserInput = input.Replace(timeMatch.Value, "").Trim();

                return new DateTime(UserAddedDate.Year, UserAddedDate.Month, UserAddedDate.Day, hour, minute, 0);
            }

            return UserAddedDate;
        }
    }
}
// ---------------------End Of File ----------------------------//