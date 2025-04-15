using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
{
    class Responses
    {
        public static void GeneralQuestions(string userResponse)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer(); // Create a SpeechSynthesizer object for text-to-speech
            while (true)
            {
                switch (userResponse.ToLower()) // Switch cast to handle general questions user would ask
                {
                    case "how are you":
                        synthesizer.SpeakAsync("I am doing well, thank you for asking!");
                        Methods.PrintCenteredStaticText("I am doing well, thank you for asking!"); // uses custom method to print centered text
                        Console.WriteLine();
                        break;
                    case "what is your purpose":
                        synthesizer.SpeakAsync("I am here to assist you with your queries and provide information about staying safe online.");
                        Methods.PrintCenteredStaticText("I am here to assist you with your queries and provide information about staying safe online.");
                        Console.WriteLine();
                        break;
                    case "what can i ask you about":
                        synthesizer.SpeakAsync("You can ask me about online safety, cybersecurity tips, and general information. Try asking how can i keep my password safe or what is phishing");
                        Methods.PrintCenteredStaticText("You can ask me about online safety, cybersecurity tips, and general information. Try asking how can i keep my password safe or what is phishing.");
                        Console.WriteLine();
                        break;
                    case "how can i keep my password safe":
                        synthesizer.SpeakAsync("Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                        Methods.PrintCenteredStaticText("Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                        Console.WriteLine();
                        break;
                    case "what is phishing":
                        synthesizer.SpeakAsync("Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                        Methods.PrintCenteredStaticText("Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                        Console.WriteLine();
                        break;
                    case "how can i browse safely":
                        synthesizer.SpeakAsync("Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                        Methods.PrintCenteredStaticText("Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                        Console.WriteLine();
                        break;
                    case "exit":
                        Methods.PrintCenteredStaticText("Thank you for using CyberBot. Goodbye!");
                        synthesizer.Speak("Thank you for using CyberBot. Goodbye!");
                        Console.WriteLine();
                        return;
                    default:
                        synthesizer.SpeakAsync("I'm sorry, I didn't understand that. Can you please rephrase?");
                        Methods.CenteredUserInput("I'm sorry, I didn't understand that. Can you please rephrase?:  ");
                        Console.WriteLine();
                        break;
                }
                synthesizer.SpeakAsync("Anything else I can help you with."); // Prompt the user for further questions after each case is used until exit is used
                userResponse = Methods.CenteredUserInput("Anything else I can help you with?:  ");
                Console.WriteLine();
            }
        }
    }
}
