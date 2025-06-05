using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cyberBotv2
{
    internal class Dictionaries
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
    }
}

//--------END OF FILE--------//
