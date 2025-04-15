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
        public static void OpeningTone()
        {
            string filePath = "C:\\Users\\Adria\\source\\repos\\CyberBot\\CyberBot\\Assets\\OpeningTone.wav";
            SoundPlayer tone = new SoundPlayer(filePath);
            tone.Load();
            tone.PlaySync(); // Play the sound synchronously
        }

        public static void LogoPrint() // printing logo
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;  // colour of logo
            string logo = " ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗\r\n██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝\r\n██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   \r\n██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   \r\n╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   \r\n ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝    ╚═╝   \r\n                                                                  \r\n\r\n                           since 2025\r\n ";
            Console.WriteLine(logo);
            Console.ResetColor();
        }

        // test of the speech synthesizer
        public static string? InitializeName()
        {
            bool isValidName = false;
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            while (!isValidName)
            {
                Console.WriteLine("Please tell me your name and we can get started");
                synthesizer.Speak("Please tell me your name and we can get started");

                string usersName = Console.ReadLine();

                if (string.IsNullOrEmpty(usersName))
                {
                    Console.WriteLine("Please enter a valid name to continue");
                    synthesizer.Speak("Please enter a valid name to continue");
                }
                else
                {
                    Console.WriteLine($"Hello {usersName}, How can I help you today?");
                    synthesizer.Speak($"Hello {usersName}, How can I help you today?");
                    string? userResponse = Console.ReadLine();
                    return userResponse;
                }
            }
            return null;
        }
    }
}
