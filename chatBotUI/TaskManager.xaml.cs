using chatBotLib;
using System;
using System.Collections.Generic;
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

namespace chatBotUI
{
    /// <summary>
    /// Interaction logic for TaskManager.xaml
    /// </summary>
    public partial class TaskManager : Page
    {
        public TaskManager()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            var tasks = Methods.ViewTasks();
            ViewTaskPanel.Children.Clear();

            foreach (var task in tasks)
            {
                var taskText = new TextBlock
                {
                    Text = task,
                    Margin = new Thickness(5),
                    FontSize = 15
                };
                ViewTaskPanel.Children.Add(taskText);
            }
        }
    }
}
