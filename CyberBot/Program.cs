using System.Media;
using System.Speech.Synthesis;


//References
//https://chatgpt.com/
//https://stackoverflow.com/questions/14491431/playing-wav-file-with-c-sharp
//https://patorjk.com/software/taag/#p=testall&f=Graffiti&t=%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20CYBERBOT
//https://stackoverflow.com/questions/12847960/centering-text-in-c-sharp-console-app-only-working-with-some-input#:~:text=Console.-,WriteLine(String.,gets%20centered%20as%20it%20should.&text=The%20problem%20occurs%20when%2Fbecause,the%20width%20of%20the%20screen.

namespace CyberBot
{
    internal class Program
    {
        static void Main(string[] args)
        { // Calling all classes 
            Greeting.LogoPrint();
            Greeting.OpeningTone();
            string userResponse = Greeting.InitializeName();
            Responses.GeneralQuestions(userResponse);
        }
    }
}