using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatBotLib
{
    public class ConversationPacer
    {
        public bool TaskInput { get; set; }
        public bool TaskDescriptionInput { get; set; }
        public bool AskForDate { get; set; }
        public bool AskForTime { get; set; }

        public string TempTitle { get; set; }
        public string TempDescription { get; set; }
        public DateTime? TempDate { get; set; }
    }
}
