using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
{
    class Greeting
    {
        // This method plays a sound file when the program starts
        public static void OpeningTone()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "OpeningTone.wav");
            // Path to the sound file
            SoundPlayer tone = new SoundPlayer(filePath); // Create a SoundPlayer object with the file path
            tone.Load(); // Load the sound file
            tone.PlaySync(); // Play the sound synchronously
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public static void LogoPrint() // This method prints the logo in the console
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Set the text color to dark yellow

            string logo =
                "     ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗\n" +
                "   ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝\n" +
                " ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   \n" +
                " ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   \n" +
                " ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   \n" +
                "  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝    ╚═╝   \n" +
                "\n" +
                "since 2025"; // Logo text

            foreach (string line in logo.Split('\n')) // split the logo into lines and print each line
            {
                Methods.PrintCenteredLogo(line.TrimEnd()); // Print each line centered
            }

            Console.ResetColor(); // Reset the console color to default
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        /// This method initializes the user's name and greets them
        public static (string? userResponse, string usersName) InitializeName()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer(); // Create a SpeechSynthesizer object for text-to-speech

            while (true) // Loop until a valid name is provided
            {
                synthesizer.SpeakAsync("Please tell me your name and we can get started"); // Prompt the user for their name
                string usersName = Methods.CenteredUserInput("Please tell me your name and we can get started:  ");
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(usersName)) // Check if the name is empty
                {
                    synthesizer.SpeakAsync("Please enter a valid name to continue"); // Prompt the user to enter a valid name
                    Methods.PrintCenteredStaticText("Please enter a valid name to continue");
                    Console.WriteLine();
                    continue;
                }
                else
                {
                    synthesizer.SpeakAsync($"Hello {usersName}, How can I help you today?"); // Greet the user with their name
                    Methods.PrintCenteredStaticText($"Hello {usersName}, How can I help you today?");
                    string? userResponse = Methods.CenteredUserInput($"{usersName}:");
                    return (userResponse, usersName);
                }
            }
        }
    }
}

// ---------------------End Of File ----------------------------//
