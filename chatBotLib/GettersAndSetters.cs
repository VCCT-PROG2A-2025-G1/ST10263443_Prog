using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatBotLib
{
    public class GettersAndSetters // Class to hold various properties related to the chatbot's conversation state
    {
        public string LastTopic { get; set; }
        public List<string> PreviousResponses { get; set; } = new List<string>();
        public HashSet<string> Interests { get; set; } = new HashSet<string>();
        public bool TaskInput { get; internal set; }
    }

    //------------------------------------------------------------------------------------------------------------------//

    public class TaskGetters : INotifyPropertyChanged // Class to represent a task with properties for title, description, date, time, and completion status
    {
        private bool _isTaskCompleted;

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? TaskDate { get; set; }
        public TimeSpan? TaskTime { get; set; }

        public bool IsTaskCompleted // Property to indicate if the task is completed
        {
            get => _isTaskCompleted;
            set
            {
                if (_isTaskCompleted != value)
                {
                    _isTaskCompleted = value;
                    OnPropertyChanged(nameof(IsTaskCompleted));
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public event PropertyChangedEventHandler PropertyChanged; // Event to notify when a property changes
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public class TQuestion // Class to represent a multiple-choice question with text, options, correct answer, and explanation
        {

            public string Text { get; set; }
            public List<string> Options { get; set; }
            public int CorrectOptionSelected { get; set; }
            public string Explanation { get; set; }

            public TQuestion(string text, List<string> options, int correctAnswer, string explaination)
            {
                Text = text;
                Options = options;
                CorrectOptionSelected = correctAnswer;
                Explanation = explaination;
            }
        }
    }
}

//---------END OF FILE-----------//