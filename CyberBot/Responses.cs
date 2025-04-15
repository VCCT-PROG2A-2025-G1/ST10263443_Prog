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
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            while (true)
            {
                switch (userResponse.ToLower())
                {
                    case "how are you":
                        Console.WriteLine("I am doing well, thank you for asking!");
                        synthesizer.Speak("I am doing well, thank you for asking!");
                        break;
                    case "what is your purpose":
                        Console.WriteLine("I am here to assist you with your queries and provide information about staying safe online.");
                        synthesizer.Speak("I am here to assist you with your queries and provide information about staying safe online.");
                        break;
                    case "what can i ask you about":
                        Console.WriteLine("You can ask me about online safety, cybersecurity tips, and general information. Try asking how can i keep my password safe or what is phishing");
                        synthesizer.Speak("You can ask me about online safety, cybersecurity tips, and general information. Try asking how can i keep my password safe or what is phishing");
                        break;
                    case "how can i keep my password safe":
                        Console.WriteLine("Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                        synthesizer.Speak("Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                        break;
                    case "what is phishing":
                        Console.WriteLine("Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                        synthesizer.Speak("Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                        break;
                    case "how can i browse safely":
                        Console.WriteLine("Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                        synthesizer.Speak("Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                        break;
                    case "exit":
                        Console.WriteLine("Thank you for using CyberBot. Goodbye!");
                        synthesizer.Speak("Thank you for using CyberBot. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("I'm sorry, I didn't understand that. Can you please rephrase?");
                        synthesizer.Speak("I'm sorry, I didn't understand that. Can you please rephrase?");
                        break;
                }
                Console.WriteLine("Anything else I can help you with.");
                synthesizer.Speak("Anything else I can help you with.");
                userResponse = Console.ReadLine();
            }
        }
    }
}
