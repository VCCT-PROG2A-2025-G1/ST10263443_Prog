using chatBotLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace chatBotLib
{
    public static class Greeting // Added a class to encapsulate methods
    {
        public static void OpeningTone()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "OpeningTone.wav");
                if (File.Exists(filePath))
                {
                    using (SoundPlayer tone = new SoundPlayer(filePath))
                    {
                        tone.Load();
                        tone.PlaySync();
                    }
                }
                else
                {
                    Methods.PrintOutput?.Invoke("Opening tone file not found.");
                }
            }
            catch (Exception ex)
            {
                Methods.PrintOutput?.Invoke("Error playing sound: " + ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static void LogoPrint()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            string logo =
                "     ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗\n" +
                "   ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝\n" +
                " ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   \n" +
                " ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   \n" +
                " ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   \n" +
                "  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝    ╚═╝   \n" +
                "\n" +
                "since 2025";

            foreach (string line in logo.Split('\n'))
            {
                Methods.PrintCenteredStaticText(line.TrimEnd());
            }

            Console.ResetColor();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static (string userResponse, string usersName) InitializeName()
        {
            while (true)
            {
                Methods.SpeakOutput?.Invoke("Please tell me your name and we can get started");
                string usersName = Methods.PromptUser?.Invoke("Please tell me your name and we can get started: ");
                Methods.PrintOutput?.Invoke("");

                if (string.IsNullOrWhiteSpace(usersName))
                {
                    Methods.SpeakOutput?.Invoke("Please enter a valid name to continue");
                    Methods.PrintOutput?.Invoke("Please enter a valid name to continue\n");
                    continue;
                }

                Methods.SpeakOutput?.Invoke($"Hello {usersName}, how can I help you today?");
                Methods.PrintCenteredStaticText($"Hello {usersName}, how can I help you today?");
                string userResponse = Methods.PromptUser?.Invoke($"{usersName}: ");
                return (userResponse, usersName);
            }
        }
    }
}
