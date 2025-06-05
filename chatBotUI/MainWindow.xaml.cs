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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static chatBotLib.GeneralQuestionResult;

namespace chatBotUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isCleared = false;
        public MainWindow()
            {
                InitializeComponent();

            this.Loaded += MainWindow_Loaded;

            Methods.ShowTaskManagerWindow = () =>
            {
                var taskManager = new TaskManagerWindow();
                taskManager.Show();
            };

        }


        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Play opening tone async, UI stays responsive
            await Task.Run(() => Greeting.OpeningTone());

            // After tone plays, focus name input box
            UserNameTextBox.Focus();
        }

        private void SubmitNameButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredName = UserNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(enteredName))
            {
                MessageBox.Show("Please enter your name.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                UserNameTextBox.Focus();
                return;
            }

            // Save user name
            Methods.UserName = enteredName;

            // Hide the name prompt overlay
            NamePromptOverlay.Visibility = Visibility.Collapsed;

            // Enable chat input and button
            UserText.IsEnabled = true;
            SendButton.IsEnabled = true;

            // Clear placeholder text if you want
            UserText.Clear();

            // Focus chat input
            UserText.Focus();

            // Optionally greet user
            ConversationBlock($"Hello {enteredName}, how can I assist you today?", "CyberBot");
        }

        private void UserText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;  // Prevent beep sound
                sendButton_Click(SendButton, new RoutedEventArgs());
            }
        }


        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string userText = UserText.Text.Trim();

            if (!string.IsNullOrEmpty(userText))
            {
                ConversationBlock(userText, Methods.UserName);
                UserText.Clear();

                // Create an instance of GettersAndSetters to pass as the required parameter
                var setters = new GettersAndSetters();

                // Call the async method with the required 'setters' parameter
                var result = await Responses.GeneralQuestionsAsync(
                    userText,
                    Methods.UserName,
                    setters
                );

                foreach (var response in result.Responses)
                {
                    ConversationBlock(response, "CyberBot");
                }

                if (result.EndConversation)
                {
                    ConversationBlock("CyberBot", "Session ended.");
                }

                
            }
        }

        private async void ConversationBlock(string message, string sender)
        {
            var isUser = string.Equals(sender, Methods.UserName, StringComparison.OrdinalIgnoreCase);

            if (!isUser)
            {
                var typingBubble = new TextBlock
                {
                    Text = "CyberBot is typing...",
                    FontStyle = FontStyles.Italic,
                    Foreground = Brushes.Gray,
                    Margin = new Thickness(10, 0, 10, 5)
                };
                ConversationStackPanel.Children.Add(typingBubble);
                Scroller.ScrollToEnd();

                await Task.Delay(500);

                ConversationStackPanel.Children.Remove(typingBubble);
            }

            var messagePanel = new StackPanel
            {
                Margin = new Thickness(10),
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            var senderName = new TextBlock
            {
                Text = sender,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.DarkSlateGray,
                FontSize = 12,
                Margin = new Thickness(5, 0, 5, 2)
            };
            messagePanel.Children.Add(senderName);

            var textBlock = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Foreground = isUser ? Brushes.White : Brushes.Black,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 14
            };

            var bubble = new Border
            {
                Background = (Brush)new BrushConverter().ConvertFrom(isUser ? "#007AFF" : "#E5E5EA"),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                MaxWidth = 300,
                Child = textBlock
            };

            messagePanel.Children.Add(bubble);
            ConversationStackPanel.Children.Add(messagePanel);
            AnimateMessageIn(bubble);

            Scroller.ScrollToEnd();
        }
        private void UserText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!_isCleared)
            {
                UserText.Clear();
                _isCleared = true;
            }
        }

        private void AnimateMessageIn(UIElement message)
        {
            var transform = new TranslateTransform { X = 300 }; // start off right
            message.RenderTransform = transform;

            var anim = new DoubleAnimation
            {
                From = 300,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            transform.BeginAnimation(TranslateTransform.XProperty, anim);
        }

    }
}
