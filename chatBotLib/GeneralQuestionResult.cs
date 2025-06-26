using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatBotLib
{
    public class GeneralQuestionResult // Represents the result of handling a general question in a chatbot context.
    {
        public List<string> Responses { get; set; }
        public bool EndConversation { get; set; }

        public GeneralQuestionResult()
        {
            Responses = new List<string>();
            EndConversation = false;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // This class handles general questions and responses in a chatbot context.
        public static class GeneralQuestionsHandler
        {
            public static GeneralQuestionResult HandleUserInput(string userResponse, string usersName)
            {
                var result = new GeneralQuestionResult();

                if (string.IsNullOrWhiteSpace(userResponse))
                {
                    result.Responses.Add("I didn't catch that. Could you repeat?");
                    return result;
                }

                string lowerInput = userResponse.ToLower();

                if (lowerInput.Contains("hello") || lowerInput.Contains("hi"))
                {
                    result.Responses.Add($"Hello, {usersName}! How can I help you?");
                }
                else if (lowerInput.Contains("bye"))
                {
                    result.Responses.Add("Goodbye! Talk to you later.");
                    result.EndConversation = true;
                }
                else
                {
                    result.Responses.Add("That's an interesting question. Let me think about it...");
                    result.Responses.Add("For now, I don't have an answer, but I'm learning every day.");
                }

                return result;
            }
        }

    }
}

//-----------END OF FILE-----------//
