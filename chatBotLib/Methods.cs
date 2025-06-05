using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using chatBotLib;

namespace chatBotLib
{
    public class Methods
    {
        public static Action<string> PrintOutput = text => Console.WriteLine(text);
        public static Action<string> SpeakOutput = text => new SpeechSynthesizer().SpeakAsync(text);
        public static Action ShowTaskManagerWindow;
        public static string UserName = "User";
        public static Func<string, string> PromptUser = prompt =>
        {
            Console.Write(prompt);
            return Console.ReadLine();
        };

        public static string defaultEmotion = "nothing"; // Tracks user sentiment
        public static HashSet<string> GetUserInterest = new HashSet<string>(); // Stores unique interests

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

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

        public static bool HandleSentiments(string userResponse, string lastTopic, string usersName, out string newUserResponse)
        {
            newUserResponse = null;
            string sentiment = GetSentimentDetection(userResponse);

            if (sentiment == "nothing" || sentiment == defaultEmotion)
                return false;

            defaultEmotion = sentiment;

            string response = null; // Initialize response to null

            switch (sentiment)
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
            if (response != null) // If a response is generated based on the sentiment
            {
                SpeakOutput?.Invoke(response);
               PrintOutput?.Invoke("CyberBot: " + response);

                if (Dictionaries.InterestedIn.ContainsKey(lastTopic))
                {
                    string tip = Dictionaries.InterestedIn[lastTopic].Invoke();
                    SpeakOutput?.Invoke(tip);
                    PrintOutput?.Invoke("CyberBot: " + tip);
                }
                newUserResponse = PromptUser?.Invoke($"{usersName}: "); // Prompt user for new input
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static bool HandleUserInterest(ref string userResponse, string usersName)
        {
            if (!userResponse.Contains("interested in"))
                return false;

            int startIndex = userResponse.IndexOf("interested in") + "interested in".Length;
            string interestedTopic = userResponse.Substring(startIndex).Trim();

            if (!GetUserInterest.Contains(interestedTopic))
            {
                GetUserInterest.Add(interestedTopic);
                string confirmation = $"Okay, I'll remember that you're interested in {interestedTopic}.";
                PrintOutput?.Invoke(confirmation);
            }
            else
            {
                string alreadyKnown = $"I already know you're interested in {interestedTopic}.";
                PrintOutput?.Invoke(alreadyKnown);
            }

            userResponse = PromptUser?.Invoke($"{usersName}: ");
            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static void InterestHandler (string interestedIn) // Handles user interest in various topics
        {
            foreach (var key in Dictionaries.InterestedIn.Keys)
            {
                if (interestedIn.Contains(key)) // Case-insensitive check
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

        public static void Thinking()
        {
            PrintOutput?.Invoke("CyberBot: Pondering question...");
            Thread.Sleep(1200);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static async Task RespondWithTipAsync(List<string> tips)
        {
            foreach (var tip in tips)
            {  
                Thinking();
                SpeakOutput?.Invoke(tip);
                PrintOutput?.Invoke("CyberBot: " + tip);
                await Task.Delay(500);
            }
        }

        public static string CenteredUserInput(string givenPrompt)
        {
            int windowWidth = Console.WindowWidth; // calculates the width of the console window
            int promptPadding = (windowWidth - givenPrompt.Length) / 2; // calculates the padding needed to center the prompt

            Console.SetCursorPosition(promptPadding, Console.CursorTop); // sets the cursor position to the center of the console
            Console.Write(givenPrompt); // prints the prompt with padding

            string userInput = Console.ReadLine(); // reads the user input
            return userInput; // returns the user input
        }

        public static void PrintCenteredLogo(string text)
        {
            int windowWidth = Console.WindowWidth;
            int leftPadding = Math.Max(0, (windowWidth - text.Length) / 2);
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }

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

        public static ObservableCollection<TaskGetters> Tasks { get; } = new ObservableCollection<TaskGetters>();

        public static void AddTask(string taskTitle, string taskDescription, DateTime? taskDate = null, TimeSpan? taskTime = null)
        {
            if (!string.IsNullOrEmpty(taskTitle) && !string.IsNullOrEmpty(taskDescription))
            {
                Tasks.Add(new TaskGetters
                {
                    TaskTitle = taskTitle,
                    TaskDescription = taskDescription,
                    TaskDate = (DateTime)taskDate,
                    TaskTime = (TimeSpan)taskTime,
                    IsTaskCompleted = false
                });
            }
        }

        public static void RemoveCreatedTask(TaskGetters task)
        {
            if (task != null && Tasks.Contains(task))
            {
                Tasks.Remove(task);
            }
        }

        public static void CompleteTask(TaskGetters task)
        {
            if (task != null && Tasks.Contains(task)) {
                task.IsTaskCompleted = true;
            }
        }

        public static List<string> ViewTasks()
        {
            return Tasks.Select(t => $"{(t.IsTaskCompleted ? "[Yes]" : "[No]")} {t.TaskTitle} - {t.TaskDescription}").ToList();
        }

        public static ObservableCollection<TaskGetters> GetTasks()
        {
            return Tasks; // assuming Tasks is ObservableCollection<TaskGetters>
        }

    }
}

// ---------------------End Of File ----------------------------//
