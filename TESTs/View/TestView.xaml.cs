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
using System.Windows.Shapes;
using TESTs.Entity;
using TESTs.Model;
using static System.Net.Mime.MediaTypeNames;

namespace TESTs.View
{
    /// <summary>
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        public TestView()
        {
            InitializeComponent();
        }
        ContextHandler handler = new ContextHandler();

        List<Question>AllQuestions = new List<Question>();
        List<Answer>AllAnswers = new List<Answer>();
        int IndexQuestion = 0;
        User _currentUser;
        Test _currentTest;
        public TestView(Test test, User user)
        {
            InitializeComponent();
            _currentUser = user;
            _currentTest = test;
            AllQuestions = handler.GetAllQuestionsByTestId(test.Id);
            

            PrintControlsByQuestion(AllQuestions[IndexQuestion]);
        }

        void PrintControlsByQuestion(Question question)
        {
            TextQuestion.Text = question.Title;
            AnswersPanel.Children.Clear();

            AllAnswers = handler.GetAllAnswersByQuestionId(question.Id);
            foreach(var answer in AllAnswers)
            {
                RadioButton radioButton = new RadioButton()
                {
                    Content = answer.Title,
                    Margin = new Thickness(5),
                    IsChecked = false,
                };
                AnswersPanel.Children.Add(radioButton);
            }
            Title = $"{_currentTest.Name}. Вопрос {(IndexQuestion+1)} из {AllQuestions.Count}";
        }

        Dictionary<String,int>SendAnswer = new Dictionary<String,int>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(var answer in AnswersPanel.Children)
            {
                if((answer as RadioButton).IsChecked==true)
                {
                    var currentAnswer = AllAnswers.First(x => x.Title == (answer as RadioButton).Content.ToString());
                    if(currentAnswer.Success)
                    {
                        SendAnswer.Add(TextQuestion.Text, 1);
                    }
                    else
                    {
                        SendAnswer.Add(TextQuestion.Text, 0);
                    }
                    break;
                }
            }
            IndexQuestion++;
            if(IndexQuestion>=AllQuestions.Count)
            {
                EndTest();
            }
            else
            {
                PrintControlsByQuestion(AllQuestions[IndexQuestion]);
            }
        }

        void EndTest()
        {
            Result result = new Result()
            {
                NameTest = _currentTest.Name,
                FIO = _currentUser.FIO,
                DateResult = DateTime.Now,
                ResultTest = $"Набрано {SendAnswer.Sum(x => x.Value)} из {AllQuestions.Count} баллов"
            };
            handler.AddResult(result);
            MessageBox.Show(result.ResultTest,"Тест завеершен");
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EndTest();
        }
    }
}
