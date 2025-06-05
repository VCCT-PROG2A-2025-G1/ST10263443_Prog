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

    public class TaskGetters : INotifyPropertyChanged
    {
        public bool IsTaskCompleted;
        private bool _IsTaskCompleted
        {
            get => _IsTaskCompleted;
            set
            {
                if (_IsTaskCompleted != value)
                {
                    _IsTaskCompleted = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTaskCompleted)));
                }
            }
        }

        public string TaskDescription { get; set; }
        public string TaskTitle { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime? TaskDate { get; set; }
        public TimeSpan? TaskTime { get; set; }
    }
}