using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatBotLib
{
    public class Dictionaries
    {
        //Dictionary to hold various cybersecurity topics and their corresponding tips retrieval methods.
        public static Dictionary<string, Func<string>> InterestedIn = new Dictionary<string, Func<string>>
        {
            { "phishing", () => Lists.PhishingTips() },
            { "password", () => Lists.PasswordTips() },
            {"online safety", () => Lists.OnlineSafetyTips() },
            { "scam", () => Lists.ScamTips() },
            {"privacy", () => Lists.PrivacyTips() },
        };

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Dictionary to hold various cybersecurity topics and their corresponding questions retrieval methods.
        public static Dictionary<string, Func<DateTime>> DateTimeQuestions = new Dictionary<string, Func<DateTime>>
        {
            { "today", () => DateTime.Today },
            { "tomorrow", () => DateTime.Today.AddDays(1) },
            { "next week", () => DateTime.Today.AddDays(7) },
            { "next month", () => DateTime.Today.AddDays(30) },
        };
    }
}

//--------END OF FILE--------//
