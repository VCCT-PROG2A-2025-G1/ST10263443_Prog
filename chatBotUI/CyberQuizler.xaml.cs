using chatBotLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for CyberQuizler.xaml
    /// </summary>
    public partial class CyberQuizler : Window
    {
        private bool isShowingFeedback = false;
        public CyberQuizler()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            Methods.CyberQuizlerFunctions();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowWelcomeMessageThenStart();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public async void ShowWelcomeMessageThenStart()
        {
            // Clear existing content just in case
            QuizPanel.Children.Clear();

            // Create a welcome TextBlock
            TextBlock welcome = new TextBlock
            {
                Text = "🔐 Welcome to CyberQuizler!\nTest your cybersecurity knowledge...",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10)
            };

            // Add it to the StackPanel
            QuizPanel.Children.Add(welcome);

            // Wait for 2 seconds
            await Task.Delay(5000);

            // Start the quiz
            ShowQuestion();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void ShowQuestion()
        {
            QuizPanel.Children.Clear();

            var question = Methods.GetCurrentQuestion();
            if (question == null)
            {
                ShowFinalScore();
                return;
            }

            // Display question text
            TextBlock questionText = new TextBlock
            {
                Text = question.Text,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 10),
                TextWrapping = TextWrapping.Wrap
            };
            QuizPanel.Children.Add(questionText);

            // Display options A), B), C), D)
            char label = 'A';
            foreach (string option in question.Options)
            {
                TextBlock optionText = new TextBlock
                {
                    Text = label + ") " + option,
                    FontSize = 16,
                    Margin = new Thickness(0, 0, 0, 5),
                    TextWrapping = TextWrapping.Wrap
                };
                QuizPanel.Children.Add(optionText);
                label++;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void SendAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (isShowingFeedback)
            {
                isShowingFeedback = false;
                if (Methods.IsQuizComplete())
                {
                    ShowFinalScore();
                }
                else
                {
                    ShowQuestion();
                }
                QuestionAnswered.Text = "";
                return;
            }

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

            string input = QuestionAnswered.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(input) || input.Length != 1 || input[0] < 'A' || input[0] > 'D')
            {
                MessageBox.Show("Please enter A, B, C, or D.");
                return;
            }

            int selectedIndex = input[0] - 'A';
            bool isCorrect = Methods.CheckAnswer(selectedIndex);
            QuestionAnswered.Text = "";

            ShowFeedback(isCorrect, selectedIndex);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void ShowFinalScore()
        {
            QuizPanel.Children.Clear();

            TextBlock finalText = new TextBlock
            {
                Text = "Quiz Complete!",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.DarkGreen,
                Margin = new Thickness(0, 0, 0, 10)
            };

            int finalScore = Methods.GetScore();
            int totalQuestions = Methods.GetTotalQuestions();

            TextBlock scoreText = new TextBlock
            {
                Text = $"Your Score: {finalScore} / {totalQuestions}",
                FontSize = 20,
                Margin = new Thickness(0, 0, 0, 5)
            };

            QuizPanel.Children.Add(finalText);
            QuizPanel.Children.Add(scoreText);

            if (finalScore == totalQuestions)
            {
                QuizPanel.Children.Add(new TextBlock
                {
                    Text = "🎉 Unbelieveable! You got all questions right!",
                    FontSize = 18,
                    Foreground = Brushes.Green,
                    Margin = new Thickness(0, 0, 0, 10)
                });
            }

            else if (finalScore >= totalQuestions / 2)
            {
                QuizPanel.Children.Add(new TextBlock
                {
                    Text = "Good job! You passed the quiz.",
                    FontSize = 18,
                    Foreground = Brushes.Orange,
                    Margin = new Thickness(0, 0, 0, 10)
                });
            }
            else
            {
                QuizPanel.Children.Add(new TextBlock
                {
                    Text = "You can do better! Keep learning.",
                    FontSize = 18,
                    Foreground = Brushes.Red,
                    Margin = new Thickness(0, 0, 0, 10)
                });
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void ShowFeedback(bool isCorrect, int userIndex)
        {
            isShowingFeedback = true;
            QuizPanel.Children.Clear();

            var question = Methods.GetCurrentQuestion(); // we already moved to next, so get previous
            if (Methods.IsQuizComplete())
            {
                question = Methods.GetPreviousQuestion(); // you'll need to add this
            }
            else if (Methods.GetCurrentIndex() > 0)
            {
                question = Methods.GetQuestionAtIndex(Methods.GetCurrentIndex() - 1);
            }

            Debug.WriteLine($"Question Explanation: {question?.Explanation ?? "NULL"}");

            string correctLetter = ((char)('A' + question.CorrectOptionSelected)).ToString();
            string userLetter = ((char)('A' + userIndex)).ToString();

            TextBlock feedback = new TextBlock
            {
                Text = isCorrect
                    ? $"Correct! You chose {userLetter}, which is right.\n\n{question.Explanation}"
                    : $"Incorrect. You chose {userLetter}, but the correct answer was {correctLetter}.\n\n{question.Explanation}",
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock nextPrompt = new TextBlock
            {
                Text = "Press Send to continue.",
                FontSize = 14,
                Foreground = Brushes.Gray
            };

            QuizPanel.Children.Add(feedback);
            QuizPanel.Children.Add(nextPrompt);
        }


    }
}

//-------------END OF FILE-------------//
