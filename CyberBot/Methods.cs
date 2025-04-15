using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
{
    internal class Methods
    {
        public static void PrintCenteredLogo(string logoText)
        {
            int windowWidth = Console.WindowWidth; // calculates the width of the console window
            int padding = (windowWidth - logoText.Length) / 2; // calculates the padding needed to center the text
            Console.WriteLine(new string(' ', Math.Max(0, padding)) + logoText); // prints the text with padding
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

        public static void PrintCenteredStaticText(string staticText)
        {
            int windowWidth = Console.WindowWidth; // calculates the width of the console window
            int textLength = staticText.Length; // calculates the length of the text
            int leftPadding = (windowWidth - textLength) / 2; // calculates the padding needed to center the text

            Console.WriteLine(new string(' ', Math.Max(0, leftPadding)) + staticText); // prints the text with padding
        }
    }
}
