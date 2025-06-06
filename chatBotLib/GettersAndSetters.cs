using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatBotLib
{
    public class GettersAndSetters
    {
        public string LastTopic { get; set; }
        public List<string> PreviousResponses { get; set; } = new List<string>();
        public HashSet<string> Interests { get; set; } = new HashSet<string>();
        public bool TaskInput { get; internal set; }
    }

    //------------------------------------------------------------------------------------------------------------------//

    public class TaskGetters : INotifyPropertyChanged
    {
        private bool _isTaskCompleted;

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? TaskDate { get; set; }
        public TimeSpan? TaskTime { get; set; }

        public bool IsTaskCompleted
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}