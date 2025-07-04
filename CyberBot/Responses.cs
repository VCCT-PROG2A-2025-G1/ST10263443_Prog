﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
{
    class Responses
    {
        static string lastTopic = "";
        static string lastSentiment = "";

        public static void GeneralQuestions(string userResponse, string usersName)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            while (true)
            {
                bool sentimentResponseGiven = false; // Flag to check if a sentiment response has been given

                string sentiment = Methods.GetSentimentDetection(userResponse); // Detects the sentiment of the user's response
                string topic = Methods.GetCurrentTopic(userResponse); // Gets the current topic from the user's response

                string newResponse; // Variable to hold the new response after handling sentiments
                if (Methods.HandleSentiments(userResponse, topic, usersName, out newResponse)) // sentiment method
                {
                    userResponse = newResponse;
                    continue; 
                }

                if (Methods.HandleUserInterest(ref userResponse, usersName)) // User interest method
                {
                    continue;
                }

                switch (userResponse.ToLower()) // Switch cast to handle general questions user would ask
                {
                    case string answer when answer.Contains("how") && answer.Contains("you"):
                        synthesizer.SpeakAsync("I am doing well, thank you for asking!");
                        Methods.PrintCenteredStaticText("CyberBot: I am doing well, thank you for asking!"); // uses custom method to print centered text
                        break;
                    case string answer when answer.Contains("explain more"):
                        if (!string.IsNullOrEmpty(lastTopic))
                        {
                            synthesizer.SpeakAsync($"Sure, let's dive deeper into {lastTopic}.");
                            string tip = Dictionaries.InterestedIn.ContainsKey(lastTopic) ? Dictionaries.InterestedIn[lastTopic].Invoke() : "I'm still learning more about that.";
                            Methods.PrintCenteredStaticText("CyberBot: " + tip);
                        }
                        break;
                    case string answer when answer.Contains("what") && answer.Contains("purpose"):
                        synthesizer.SpeakAsync( "I am here to assist you with your queries and provide information about staying safe online.");
                        Methods.PrintCenteredStaticText("CyberBot: I am here to assist you with your queries and provide information about staying safe online.");
                        break;
                    case string answer when answer.Contains("what") && answer.Contains("ask"):
                        synthesizer.SpeakAsync("You can ask me about online safety, cybersecurity tips, and general information. Try asking how can i keep my password safe or what is phishing");
                        Methods.PrintCenteredStaticText("CyberBot: You can ask me about online safety, cybersecurity tips, and general information. Try asking how can i keep my password safe or what is phishing.");
                        break;
                    case string answer when answer.Contains("password") && answer.Contains("safe"):
                        synthesizer.SpeakAsync("Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                        Methods.PrintCenteredStaticText("CyberBot: Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                        lastTopic = topic;
                        break;
                    case string answer when answer.Contains("what") && answer.Contains("phishing"):
                        synthesizer.SpeakAsync("Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                        Methods.PrintCenteredStaticText("CyberBot: Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                        lastTopic = topic;
                        break;
                    case string answer when answer.Contains("browse") && answer.Contains("safely"):
                        synthesizer.SpeakAsync("Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                        Methods.PrintCenteredStaticText("ChatBot: Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                        break;
                    case string answer when answer.Contains("avoid") && answer.Contains("scam"):
                        synthesizer.SpeakAsync("ChatBot: Be cautious with unfamiliar links, never share personal info with strangers, and always verify who you're dealing with.");
                        Methods.PrintCenteredStaticText("Be cautious with unfamiliar links, never share personal info with strangers, and always verify who you're dealing with.");
                        break;
                    case string answer when answer.Contains("ensure") && answer.Contains("privacy"):
                        synthesizer.SpeakAsync("Adjust your privacy settings, avoid oversharing, and use secure tools like VPNs and strong passwords.");
                        Methods.PrintCenteredStaticText("ChatBot: Adjust your privacy settings, avoid oversharing, and use secure tools like VPNs and strong passwords.");
                        break;
                    case string answer when answer.Contains("tips"):
                        bool foundTopic = false; // Check if the user is interested in a specific topic
                        foreach (var topics in Dictionaries.InterestedIn.Keys) // Iterate through the dictionary keys
                        {
                            if (answer.Contains(topics)) //topic is found
                            {

                               if (Methods.GetUserInterest.Contains(topics)) //Checks if the topic is an interest of the user
                                {
                                    synthesizer.SpeakAsync($"Since you're interested in {topics}, here are some tips:");
                                    Methods.PrintCenteredStaticText($"ChatBot : Since you're interested in {topics}, here is a tip:");
                                }
                                string tip = Dictionaries.InterestedIn[topics].Invoke();  // Retrieves the tip from the dictionary
                                synthesizer.SpeakAsync(tip);
                                Methods.PrintCenteredStaticText("ChatBot: " + tip);
                                break;
                            }
                        }
                        break;
                    case string answer when answer.Contains("phishing") && answer.Contains("tips"):
                        string phishingTip = Lists.PhishingTips();
                        synthesizer.SpeakAsync(phishingTip);
                        Methods.PrintCenteredStaticText("CyberBot: " + phishingTip);
                        break;
                    case string answer when answer.Contains("password") && answer.Contains("tips"):
                        string passwordTip = Lists.PasswordTips();
                        synthesizer.SpeakAsync(passwordTip);
                        Methods.PrintCenteredStaticText("CyberBot: " + passwordTip);
                        break;
                    case string answer when answer.Contains("safety") && answer.Contains("tips"):
                        string safetyTip = Lists.OnlineSafetyTips();
                        synthesizer.SpeakAsync(safetyTip);
                        Methods.PrintCenteredStaticText("CyberBot: " + safetyTip);
                        break;
                    case string answer when answer.Contains("scam") && answer.Contains("tips"):
                        string scamTip = Lists.PhishingTips();
                        synthesizer.SpeakAsync(scamTip);
                        Methods.PrintCenteredStaticText("CyberBot: " + scamTip);
                        break;
                    case string answer when answer.Contains("privacy") && answer.Contains("tips"):
                        string privacyTip = Lists.PrivacyTips();
                        synthesizer.SpeakAsync(privacyTip);
                        Methods.PrintCenteredStaticText("CyberBot: " + privacyTip);
                        break;
                    case string answer when answer.Contains("exit") || answer.Contains("goodbye"):
                        Methods.PrintCenteredStaticText($"ChatBot: Thank you for using CyberBot {usersName}. Goodbye!");
                        synthesizer.Speak("Thank you for using CyberBot. Goodbye!");
                        Console.WriteLine();
                        return;
                    default:
                        if (!sentimentResponseGiven)
                        {
                            synthesizer.SpeakAsync("I'm sorry, I didn't understand that. Can you please rephrase?");
                            Methods.PrintCenteredStaticText("CyberBot: I'm sorry, I didn't understand that. Can you please rephrase?:  ");
                        }
                            break;
                }
                synthesizer.SpeakAsync("Anything else I can help you with.");
                 Methods.PrintCenteredStaticText("CyberBot: Anything else I can help you with?:  ");
                userResponse=Methods.CenteredUserInput($"{usersName}:");
            }
        }
    }
}

// ---------------------End Of File ----------------------------//
