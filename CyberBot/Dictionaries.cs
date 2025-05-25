using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
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
