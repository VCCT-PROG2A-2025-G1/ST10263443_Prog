using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberBot
{
    internal class Lists
    {
        public static string PhishingTips()
        {
            List<string> phishingTips = new List<string>
                {
                    "Always verify the sender's email address before clicking any links.",
                    "Avoid opening attachments from unknown or suspicious sources.",
                    "Look for poor grammar or spelling mistakes in emails—they're often a red flag.",
                    "Never provide personal information in response to unsolicited requests.",
                    "Use multi-factor authentication to add an extra layer of security."
                };
            Random rand = new Random();
            return phishingTips[rand.Next(phishingTips.Count)];
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string PasswordTips()
        {
            List<string> passwordTips = new List<string>
                {
                    "Use a mix of uppercase and lowercase letters, numbers, and symbols.",
                    "Avoid using easily guessable information like birthdays or names.",
                    "Change your passwords regularly and avoid reusing them across different accounts.",
                    "Consider using a password manager to keep track of your passwords securely.",
                    "Enable two-factor authentication whenever possible for added security."
                };
            Random rand = new Random();
            return passwordTips[rand.Next(passwordTips.Count)];
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string OnlineSafetyTips()
        {
            List<string> onlineSafetyTips = new List<string>
                {
                    "Always use secure websites (look for 'https://' in the URL).",
                    "Be cautious when clicking on links in emails or messages from unknown sources.",
                    "Keep your software and antivirus programs up to date.",
                    "Use strong, unique passwords for each of your accounts.",
                    "Be mindful of what personal information you share online."
                };
            Random rand = new Random();
            return onlineSafetyTips[rand.Next(onlineSafetyTips.Count)];
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string ScamTips()
        {
            // List of tips to avoid scams
            List<string> scamTips = new List<string>
                {
                    "Be cautious of unsolicited messages or phone calls asking for personal information.",
                    "Research any offers or requests before taking action.",
                    "Trust your instincts—if something seems too good to be true, it probably is.",
                    "Use reputable sources to verify the legitimacy of any claims.",
                    "Report any suspicious activity to the appropriate authorities."
                };
            Random rand = new Random();
            return scamTips[rand.Next(scamTips.Count)];
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public static string PrivacyTips()
        {
            // List of tips to ensure online privacy

            List<string> privacyTips = new List<string>
                {
                    "Review and adjust your privacy settings on social media platforms.",
                    "Be cautious about sharing personal information publicly.",
                    "Use strong passwords and enable two-factor authentication.",
                    "Regularly check your online accounts for any unauthorized activity.",
                    "Consider using a VPN for added security when browsing."
                };
            Random rand = new Random();
            return privacyTips[rand.Next(privacyTips.Count)];
        }

    }
}

//----------End Of File----------//
