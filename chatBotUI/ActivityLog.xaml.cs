using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static chatBotLib.Methods;

namespace chatBotUI
{
    /// <summary>
    /// Interaction logic for ActivityLog.xaml
    /// </summary>
    public partial class ActivityLog : Window
    {

        public ObservableCollection<string> VisibleLogs { get; set; } = new ObservableCollection<string>();
        private bool showFullLog = false;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public ActivityLog(List<string> activities)
        {
            InitializeComponent();
            VisibleLogs = new ObservableCollection<string>(activities);
            ActivityLogList.ItemsSource = VisibleLogs;
            RefreshLog();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public void RefreshLog()
        {
            var fullLog = ActivityTracker.GetLog();
            var logsToShow = showFullLog ? fullLog : fullLog.Take(5);

            VisibleLogs.Clear();
            foreach (var log in logsToShow)
            {
                VisibleLogs.Add(log);
            }

            showMore.Content = showFullLog ? "Show Less" : "Show More";
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void ShowMore_Click(object sender, RoutedEventArgs e)
        {
            showFullLog = !showFullLog;
            RefreshLog();
        }

    }
}
