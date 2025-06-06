using chatBotLib;
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
using System.Windows.Shapes;

namespace chatBotUI
{
    /// <summary>
    /// Interaction logic for TaskManagerWindow.xaml
    /// </summary>
    public partial class TaskManagerWindow : Window
    {
        public ObservableCollection<TaskGetters> Tasks { get; set; }

        public TaskManagerWindow()
        {
            InitializeComponent();
            Tasks = Methods.Tasks;
            DataContext = this;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TaskGetters task)
                Methods.RemoveCreatedTask(task);
        }

        private void CompleteTask_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is TaskGetters task)
                Methods.CompleteTask(task);
        }
    }
}
