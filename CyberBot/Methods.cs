using CyberBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
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
                if (input.Contains(topic, StringComparison.OrdinalIgnoreCase))
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

            return "northing";
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string defaultEmotion = "nothing"; // Default emotion to track the user's sentiment, initialized to "nothing".

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static bool HandleSentiments(string userResponse, string topic, string usersName, out string newUserResponse) // Handles the user's sentiments based on their response
        {
            newUserResponse = null;
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            string sentiment = GetSentimentDetection(userResponse);

            if (sentiment == "neutral" || sentiment == defaultEmotion)
                return false;

            defaultEmotion = sentiment;

            string response = sentiment switch // Switch expression to determine the response based on the user's sentiment
            {
                "worried" => $"Don't worry, it's natural to be worried about {topic}. Here are some tips to help.",
                "curious" => $"Since you're curious about {topic}, here are some tips.",
                "frustrated" => $"Your frustration about {topic} is valid. Let's take it one step at a time.",
                _ => null
            };

            if (response != null) // If a response is generated based on the sentiment
            {
                synthesizer.SpeakAsync(response);
                PrintCenteredStaticText("CyberBot: " + response);

                if (Dictionaries.InterestedIn.ContainsKey(topic))
                {
                    string tip = Dictionaries.InterestedIn[topic].Invoke();
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