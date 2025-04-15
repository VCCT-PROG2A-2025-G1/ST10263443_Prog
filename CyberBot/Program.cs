using System.Media;
using System.Speech.Synthesis;

namespace CyberBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Greeting.LogoPrint();
            Greeting.OpeningTone();
            string userResponse = Greeting.InitializeName();
        }
    }
}