using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chatBotLib.Methods;

namespace chatBotLib
{
    public class Responses

    {
        private static string userInput;

        public static async Task<GeneralQuestionResult> GeneralQuestionsAsync(
           string userResponse,
           string usersName,
            GettersAndSetters setters)
        {
            userInput = userResponse; // Store the user input for later use
            var result = new GeneralQuestionResult();

            string sentiment = Methods.GetSentimentDetection(userResponse);
            string topic = Methods.GetCurrentTopic(userResponse);

   
            if (Methods.HandleSentiments(userResponse, topic, usersName, out string newResponse))
            {
                result.Responses.Add(newResponse);
                result.EndConversation = false; // or true, depending on your flow
                return result; // Fixed: Return the result directly instead of wrapping it in Task.FromResult
            }

            if (Methods.HandleUserInterest(ref userResponse, usersName))
            {
                result.Responses.Add($"Okay, I'll remember that you're interested in {userResponse}."); 
                result.EndConversation = false; // or true, depending on your flow
                return result; 
            }


            if (Methods.IsAddingTask || Methods.AskingForDate || Methods.WaitingForDate || Methods.AskingForTime || Methods.WaitingForTime)
            {
                string botReply = Methods.TaskFlow(userResponse); 
                result.Responses.Add(botReply ?? "Something went wrong in the task flow.");
                result.EndConversation = false;
                return result;
            }

            if (Methods.NLPReaction(userResponse, out string title, out string desc, out DateTime? date))
            {
                Methods.AddTask(title, desc, date);
                result.Responses.Add($"Got it! I've added the task: \"{title}\" {(date.HasValue ? $"for {date.Value:dd-MM-yyyy}" : "")}.");
                result.EndConversation = false;
                return result;
            }

            switch (userResponse.ToLower()) // Switch cast to handle general questions user would ask
            {
                case string answer when answer.Contains("how") && answer.Contains("you"):
                    result.Responses.Add("I am doing well, thank you for asking!. What can I do for you today?");
                    ActivityTracker.Add("User asked how the bot is doing");
                    break;

                case string answer when answer.Contains("explain") || (answer.Contains("tell") && answer.Contains("more")):
                    if (!string.IsNullOrEmpty(setters.LastTopic))
                    {
                        string tip = Dictionaries.InterestedIn.ContainsKey(setters.LastTopic)
                            ? Dictionaries.InterestedIn[setters.LastTopic].Invoke()
                            : "I'm still learning more about that.";
                        result.Responses.Add($"Sure, let's dive deeper into {setters.LastTopic}. {tip}");
                        ActivityTracker.Add($"User asked for more information about {setters.LastTopic}");
                    }
                    break;

                case string answer when answer.Contains("what") && answer.Contains("purpose"):
                    result.Responses.Add("I am here to assist you with your queries and provide information about staying safe online.");
                    ActivityTracker.Add("User asked about the bot's purpose");
                    break;

                case string answer when answer.Contains("what") && answer.Contains("ask"):
                    result.Responses.Add("You can ask me about online safety, cybersecurity tips, and general information. Try asking how can I keep my password safe or what is phishing.");
                    ActivityTracker.Add("User asked about what they can ask the bot");
                    break;

                case string answer when answer.Contains("password") && answer.Contains("safe"):
                    result.Responses.Add("Use a mix of letters, numbers, and symbols. Avoid using personal information.");
                    setters.LastTopic = topic;
                    ActivityTracker.Add("User asked about password safety");
                    break;

                case string answer when answer.Contains("what") && answer.Contains("phishing"):
                    result.Responses.Add("Phishing is a method used by cybercriminals to trick you into giving them your personal information.");
                    setters.LastTopic = topic;
                    ActivityTracker.Add("User asked about phishing");
                    break;

                case string answer when answer.Contains("browse") && answer.Contains("safely"):
                    result.Responses.Add("Use secure websites, avoid clicking on suspicious links, and keep your software updated.");
                    setters.LastTopic = topic;
                    ActivityTracker.Add("User asked about safe browsing practices");
                    break;

                case string answer when answer.Contains("avoid") && answer.Contains("scam"):
                    result.Responses.Add("Be cautious with unfamiliar links, never share personal info with strangers, and always verify who you're dealing with.");
                    setters.LastTopic = topic;
                    ActivityTracker.Add("User asked about avoiding scams online");
                    break;

                case string answer when answer.Contains("ensure") && answer.Contains("privacy"):
                    result.Responses.Add("Adjust your privacy settings, avoid oversharing, and use secure tools like VPNs and strong passwords.");
                    setters.LastTopic = topic;
                    ActivityTracker.Add("User asked about ensuring privacy online");
                    break;

                case string answer when answer.Contains("tips"):
                    {
                        bool found = false;

                        // 1️⃣ Check for direct topic match in user's message
                        foreach (var interestedTopic in Dictionaries.InterestedIn.Keys)
                        {
                            if (answer.Contains(interestedTopic))
                            {
                                string tip = Dictionaries.InterestedIn[interestedTopic].Invoke();
                                result.Responses.Add($"Here’s a tip about {interestedTopic}: {tip}");
                                setters.LastTopic = interestedTopic; // Update recent context
                                found = true;
                                break;
                            }
                        }

                        // 2️⃣ If no match, try last discussed topic
                        if (!found && !string.IsNullOrEmpty(setters.LastTopic))
                        {
                            if (Dictionaries.InterestedIn.TryGetValue(setters.LastTopic, out var tipFunc))
                            {
                                string tip = tipFunc.Invoke();
                                result.Responses.Add($"Since we were talking about {setters.LastTopic}, here’s a tip: {tip}");
                                found = true;
                            }
                        }

                        // 3️⃣ If still no match, fall back to user interests
                        if (!found && setters.Interests != null)
                        {
                            foreach (var interest in setters.Interests)
                            {
                                if (Dictionaries.InterestedIn.TryGetValue(interest, out var tipFunc))
                                {
                                    string tip = tipFunc.Invoke();
                                    result.Responses.Add($"Based on your interests, here’s a tip about {interest}: {tip}");
                                    found = true;
                                    break;
                                }
                            }
                        }

                        // 4️⃣ If nothing at all found
                        if (!found)
                        {
                            result.Responses.Add("I’m not sure what to give tips on. Could you tell me which topic you mean?");
                        }

                        break;
                    }

                case string answer when answer.Contains("phishing") && answer.Contains("tips"):
                    Methods.Thinking();
                    string phishingTip = Lists.PhishingTips();
                     result.Responses.Add(phishingTip);
                    ActivityTracker.Add("User requested phishing tips");
                    break;

                case string answer when answer.Contains("password") && answer.Contains("tips"):
                    Methods.Thinking();
                    string passwordTip = Lists.PasswordTips();
                    result.Responses.Add(passwordTip);
                    ActivityTracker.Add("User requested password tips");
                    break;

                case string answer when answer.Contains("safety") && answer.Contains("tips"):
                    Methods.Thinking();
                    string safetyTip = Lists.OnlineSafetyTips();
                    result.Responses.Add(safetyTip);
                    ActivityTracker.Add("User requested online safety tips");
                    break;

                case string answer when answer.Contains("scam") && answer.Contains("tips"):
                    string scamTip = Lists.ScamTips();
                    Methods.Thinking();
                    result.Responses.Add(scamTip);
                    ActivityTracker.Add("User requested scam tips");
                    break;

                case string answer when answer.Contains("privacy") && answer.Contains("tips"):
                    string privacyTip = Lists.PrivacyTips();
                    Methods.Thinking();
                    result.Responses.Add(privacyTip);
                    ActivityTracker.Add("User requested privacy tips");
                    break;

                case string answer when answer.Contains("add") && answer.Contains("task"):
                    result.Responses.Add("Please provide a title for the task (format: Title - Description). If you want to skip description, just enter the title.");
                    setters.TaskInput = true;
                    Methods.IsAddingTask = true;
                    result.EndConversation = false;
                    ActivityTracker.Add("User started adding a task");
                    break;

                case string answer when answer.Contains("view") && answer.Contains("tasks"):
                    Methods.ShowTaskManagerWindow?.Invoke();
                    ActivityTracker.Add("User viewed the task manager");
                    break;

                case string answer when answer.Contains("play") && answer.Contains("game"):
                    Methods.ShowQuizWindow?.Invoke();
                    ActivityTracker.Add("User started the trivia game");
                    break;
                case string answer when answer.Contains("view") && answer.Contains("log"):
                    Methods.showActivityLog?.Invoke();
                    break;
                case string answer when answer.Contains("exit") || answer.Contains("goodbye"):
                    result.Responses.Add($"Thank you for using CyberBot {usersName}. Goodbye!");
                    result.EndConversation = true;
                    break;

                default:
                    result.Responses.Add("I'm sorry, I didn't understand that. Can you please rephrase?");
                    break;
            }
            return result;
        }
        }
}

// ---------------------End Of File ----------------------------//
