using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cyberBotv2
{
    internal class Methods
    {
        public static void BorderPrint() // Prints border around logo
        {
            int width = Console.WindowWidth;
            int height = 10;

            // Draw top border
            Console.Write("+");
            for (int i = 0; i < width - 2; i++) Console.Write("-");
            Console.WriteLine("+");

            // Draw sides
            for (int i = 0; i < height - 2; i++)
            {
                Console.Write("|");
                for (int j = 0; j < width - 2; j++) Console.Write(" ");
                Console.WriteLine("|");
            }

            // Draw bottom border
            Console.Write("+");
            for (int i = 0; i < width - 2; i++) Console.Write("-");
            Console.WriteLine("+");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static void PrintCenteredLogo(string LogoText) // Prints logo in center
        {
            int windowWidth = Console.WindowWidth;
            int lineLength = LogoText.Length;
            int padding = Math.Max((windowWidth - lineLength) / 2, 0);

            Console.SetCursorPosition(padding, Console.CursorTop);
            Console.WriteLine(LogoText);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public static string CenteredUserInput(string givenPrompt)
        {
            int windowWidth = Console.WindowWidth; // calculates the width of the console window
            int promptPadding = (windowWidth - givenPrompt.Length) / 2; // calculates the padding needed to center the prompt

            Console.SetCursorPosition(promptPadding, Console.CursorTop); // sets the cursor position to the center of the console
            Console.Write(givenPrompt); // prints the prompt with padding

            string userInput = Console.ReadLine(); // reads the user input
            return userInput; // returns the user input
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static void PrintCenteredStaticText(string staticText) // Prints text in the center
        {
            int windowWidth = Console.WindowWidth;
            int textLength = staticText.Length;
            int leftPadding = (windowWidth - textLength) / 2;

            // Ensure padding is at least 0
            leftPadding = Math.Max(0, leftPadding);

            // Move the cursor to the center padding
            Console.SetCursorPosition(leftPadding, Console.CursorTop);

            Random rand = new Random();

            // Print one character at a time
            foreach (char c in staticText)
            {
                Console.Write(c);
                Thread.Sleep(rand.Next(20, 60)); // Simulate typing speed variation
            }

            Console.WriteLine();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static HashSet<string> GetUserInterest = new HashSet<string>(); // A HashSet to store user interests, ensuring uniqueness and fast lookups.

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static void InterestHandler(string interestedIn) // Handles user interest in various topics
        {
            foreach (var key in Dictionaries.InterestedIn.Keys)
            {
                if (interestedIn.Contains(key))
                {
                    if (!GetUserInterest.Contains(key))
                    {
                        GetUserInterest.Add(key);
                        PrintCenteredStaticText($"Okay, I'll remember that you're interested in {key}.");
                    }
                    return;
                }
            }

            PrintCenteredStaticText("Sorry, I don't have information on that topic.");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetCurrentTopic(string input) // Determines the current topic of interest based on user input
        {
            foreach (var topic in Dictionaries.InterestedIn.Keys)
            {
                if (input.IndexOf(topic, StringComparison.OrdinalIgnoreCase) >= 0) // Replace Contains with IndexOf for case-insensitive comparison
                {
                    return topic;
                }
            }

            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetSentimentDetection(string userInput) // Detects the sentiment of the user's input
        {
            userInput.ToLower();

            if (userInput.Contains("worried") || userInput.Contains("anxious") || userInput.Contains("nervous"))
                return "worried";
            if (userInput.Contains("curious"))
                return "curious";
            if (userInput.Contains("frustrated") || userInput.Contains("annoyed") || userInput.Contains("irritated"))
                return "frustrated";

            return "nothing";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string defaultEmotion = "nothing"; // Default emotion to track the user's sentiment, initialized to "nothing".

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static bool HandleSentiments(string userResponse, string lastTopic, string usersName, out string newUserResponse) // Handles the user's sentiments based on their response
        {
            newUserResponse = null;
            var synthesizer = new SpeechSynthesizer();

            string sentiment = GetSentimentDetection(userResponse);

            if (sentiment == "neutral" || sentiment == defaultEmotion)
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
                synthesizer.SpeakAsync(response);
                PrintCenteredStaticText("CyberBot: " + response);

                if (Dictionaries.InterestedIn.ContainsKey(lastTopic))
                {
                    string tip = Dictionaries.InterestedIn[lastTopic].Invoke();
                    synthesizer.SpeakAsync(tip);
                    PrintCenteredStaticText("CyberBot: " + tip);
                }

                newUserResponse = CenteredUserInput($"{usersName}:");
                return true;
            }

            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static bool HandleUserInterest(ref string userResponse, string usersName) // Handles the user's interest in a specific topic
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            if (!userResponse.Contains("interested in"))
                return false;

            int startIndex = userResponse.IndexOf("interested in") + "interested in".Length; // Finds the index of the phrase "interested in" and adds its length to get the start index of the topic
            string interestedTopic = userResponse.Substring(startIndex).Trim();

            if (!GetUserInterest.Contains(interestedTopic))
            {
                GetUserInterest.Add(interestedTopic);
                string confirmation = $"Okay, I'll remember that you're interested in {interestedTopic}.";
                synthesizer.SpeakAsync(confirmation);
                PrintCenteredStaticText(confirmation);
            }
            else
            {
                string alreadyKnown = $"I already know you're interested in {interestedTopic}.";
                synthesizer.SpeakAsync(alreadyKnown);
                PrintCenteredStaticText(alreadyKnown);
            }

            synthesizer.SpeakAsync("Anything else I can help you with?");
            PrintCenteredStaticText("CyberBot: Anything else I can help you with?:  ");

            userResponse = CenteredUserInput($"{usersName}:");

            return true;
        }

        public static void Thinking()
        {
            Console.Write("Pondering question");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(400);
                Console.Write(".");
            }
            Console.WriteLine();
        }


    }
}

// ---------------------End Of File ----------------------------//
