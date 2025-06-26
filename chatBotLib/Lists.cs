using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static chatBotLib.TaskGetters;

namespace chatBotLib
{
    public class Lists
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

        public static string ScamTips() // Method to provide tips on avoiding scams
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

        public static string PrivacyTips() // Method to provide tips on maintaining online privacy
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

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public static List<String> triggerWords = new List<string> // List of trigger words for task reminders
            {
                "remind me to",
                "make sure to",
                "could you remind me",
                "don't let me forget"
            };

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public static List<String> dateIndicators = new List<string> // List of date indicators for task scheduling
            {
                "on",
                "at",
                "by",
                "before"
            };

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        public static class TriviaQuestions // Class to hold trivia questions for the chatbot
        {
            public static List<TQuestion> GetTQuestions()
            {
                List<TQuestion> questions = new List<TQuestion>();

                // Question 1 – True/False: Correct answer = True (index 0)
                questions.Add(new TQuestion(
                    "Phishing attacks often rely on tricking users into clicking malicious links or providing sensitive information.",
                    new List<string> { "True", "False" },
                    0,
                    "Phishing is a type of social engineering attack where attackers impersonate trusted entities to deceive users into revealing credentials, clicking harmful links, or downloading malware."
                ));

                // Question 2 – Multiple Choice: Correct answer = "!Qw8$zR#2L" (index 2)
                questions.Add(new TQuestion(
                    "Which of these is a strong password?",
                    new List<string> { "password123", "123456", "!Qw8$zR#2L", "john1985" },
                    2,
                    "Strong passwords include a mix of uppercase, lowercase, numbers, and symbols, and avoid common patterns or personal info."
                ));

                // Question 3 – Multiple Choice: Correct answer = "Worm" (index 1)
                questions.Add(new TQuestion(
                    "What type of malware replicates itself to spread to other computers?",
                    new List<string> { "Trojan", "Worm", "Spyware", "Ransomware" },
                    1,
                    "A worm is a standalone malware that replicates itself to spread to other devices, often exploiting vulnerabilities without user interaction."
                ));

                // Question 4 – True/False: Correct answer = False (index 1)
                questions.Add(new TQuestion(
                    "A firewall’s main function is to encrypt data on your hard drive.",
                    new List<string> { "True", "False" },
                    1,  
                    "A firewall controls incoming and outgoing network traffic based on security rules; it does not encrypt files. Encryption is typically handled by software like BitLocker or VeraCrypt."
                ));

                // Question 5 – Multiple Choice: Correct answer = "A process to convert data into unreadable code without a key" (index 1)
                questions.Add(new TQuestion(
                    "Which of the following best describes \"encryption\"?",
                    new List<string> {
                "A way to physically secure a device",
                "A process to convert data into unreadable code without a key",
                "A type of virus",
                "A firewall setting"
                    },
                    1,
                    "Encryption converts readable data into a coded format (ciphertext) that can only be read with the correct decryption key. It's crucial for data privacy."
                ));

                // Question 6 – True/False: Correct answer = True (index 0)
                questions.Add(new TQuestion(
                    "A VPN helps protect your online privacy.",
                    new List<string> { "True", "False" },
                    0,
                    "A VPN encrypts your internet traffic, masking your IP address and location, helping to maintain privacy and security online."
                ));

                // Question 7 – Multiple Choice: Correct answer = "Fingerprint" (index 2)
                questions.Add(new TQuestion(
                    "Which one is a two-factor authentication method?",
                    new List<string> { "Username", "Password", "Fingerprint", "Security question" },
                    2,
                    "Fingerprints, facial scans, or other biometrics used in addition to a password are examples of two-factor authentication (2FA)."
                ));

                // Question 8 – True/False: Correct answer = True (index 0)
                questions.Add(new TQuestion(
                    "HTTPS is more secure than HTTP.",
                    new List<string> { "True", "False" },
                    0,
                    "HTTPS encrypts the data exchanged between your browser and the server, offering secure communication and protection from eavesdropping."
                ));

                // Question 9 – Multiple Choice: Correct answer = "Grammar errors" (index 2)
                questions.Add(new TQuestion(
                    "Which of these is a common sign of a phishing email?",
                    new List<string> {
                    "No greeting",
                    "Personalized signature",
                    "Grammar errors",
                    "Sent from your boss"
                    },
                    2,
                    "Phishing emails often contain grammar and spelling mistakes, urgent language, or strange formatting to trick recipients."
                ));

                // Question 10 – Multiple Choice: Correct answer = "SSL/TLS" (index 2)
                questions.Add(new TQuestion(
                    "Which of the following is used to encrypt data?",
                    new List<string> { "Firewall", "Antivirus", "SSL/TLS", "VPN" },
                    2,
                    "SSL/TLS are cryptographic protocols that encrypt data sent over the internet, especially in HTTPS websites."
                ));

                return questions;
            }
        }
    }
}

//----------End Of File----------//
