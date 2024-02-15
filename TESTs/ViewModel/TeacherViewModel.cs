using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TESTs.Entity;
using TESTs.Model;
using TESTs.View;

namespace TESTs.ViewModel
{
    public class TeacherViewModel:INotifyPropertyChanged
    {

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Grade> Grades { get; set; } = new ObservableCollection<Grade>();
        public ObservableCollection<Test> AllTests { get; set; } = new ObservableCollection<Test>();
        public ObservableCollection<Result> AllResults { get; set; } = new ObservableCollection<Result>();
        ContextHandler handler = new ContextHandler();
        public TeacherViewModel()
        {
            foreach(var category in handler.GetCategories()) 
            {
                Categories.Add(category);
            }
            
            foreach(var  grade in handler.GetGrades())
            {
                Grades.Add(grade);
            }

            foreach(var result in handler.GetAllResult())
            {
                AllResults.Add(result);
            }

            PrintTests();
        }

        public void PrintTests()
        {
            AllTests.Clear();
            foreach (var test in handler.GetAllTests())
            {
                AllTests.Add(test);
            }
        }

        public RelayCommand AddNewTest
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    Questions.Clear();
                    Answers.Clear();
                    newTestView = new NewTestView();
                    //Test test = new Test();
                    if(newTestView.ShowDialog()==true)
                    {
                        handler.AddTest(NewTest);
                        foreach(var question in Questions)
                        {
                            question.TestId = NewTest.Id;
                            handler.UpdateQuestion(question);
                        }
                        AllTests.Add(NewTest);
                    }
                });
            }
        }

        public RelayCommand AddQuestion
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    newQuestionView = new NewQuestionView();
                    if(newQuestionView.ShowDialog()==true)
                    {
                        Questions.Add(NewQuestion);
                    }
                });
            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                AllTests.Clear();
                foreach(var test in handler.GetAllTests().Where(x=>x.CategoryId==selectedCategory.Id))
                {
                    if(SelectedGrade!=null)
                    {
                        if(test.GradeId==SelectedGrade.Id)
                        {
                            AllTests.Add(test);
                        }
                    }
                    else
                    {
                        AllTests.Add(test);
                    }
                }
                OnPropertyChanged();
            }
        }

        private Grade selectedGrade;
        public Grade SelectedGrade
        {
            get { return selectedGrade; }
            set
            {
                selectedGrade = value;
                AllTests.Clear();
                foreach(var test in handler.GetAllTests().Where(x=>x.GradeId==SelectedGrade.Id))
                {
                    if (SelectedCategory!=null)
                    {
                        if(test.CategoryId==SelectedCategory.Id)
                        {
                            AllTests.Add(test);
                        }
                    }
                    else
                    {
                        AllTests.Add(test);
                    }
                }
                OnPropertyChanged();
            }
        }

        private Test selectedTest;
        public Test SelectedTest
        {
            get { return selectedTest; }
            set
            {
                selectedTest = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddAnswer
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    newAnswerView = new NewAnswerView();
                    if(newAnswerView.ShowDialog()==true)
                    {
                        Answers.Add(NewAnswer);
                    }
                });
            }
        }
        #region Тест
        static NewTestView newTestView;
        static Test NewTest;
        private string newTestName;
        public string NewTestName
        {
            get { return newTestName; }
            set
            {
                newTestName = value;
                OnPropertyChanged();
            }
        }

        public static ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
        public RelayCommand CompleteTest
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if(string.IsNullOrEmpty(NewTestName))
                    {
                        MessageBox.Show("Название теста не можеты быть пустым","Ошибка");
                        return;
                    }
                    if(Questions.Count == 0)
                    {
                        MessageBox.Show("Тест не может не содержать вопросов","Ошибка");
                        return;
                    }
                    NewTest = new Test()
                    {
                        Name = NewTestName,
                        CategoryId = SelectedCategory.Id,
                        GradeId = SelectedGrade.Id,
                    };
                    newTestView.DialogResult = true;
                });
            }
        }
        #endregion

        #region Вопрос
        static Question NewQuestion;
        static NewQuestionView newQuestionView;
        private string newQuestionName;
        public string NewQuestionName
        {
            get { return newQuestionName; }
            set
            {
                newQuestionName = value;
                OnPropertyChanged();
            }
        }
        public static ObservableCollection<Answer> Answers { get; set; } = new ObservableCollection<Answer>();
        public RelayCommand QuestionComplete
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if(string.IsNullOrEmpty(NewQuestionName))
                    {
                        MessageBox.Show("Вопрос не может быть пустым","Ошибка");
                        return;
                    }
                    if(Answers.Count==0)
                    {
                        MessageBox.Show("Нельзя сохранить вопрос без вариантов ответа","Ошибка");
                        return;
                    }
                    NewQuestion = new Question()
                    {
                        Title = NewQuestionName,
                    };
                    handler.AddQuestion(NewQuestion);
                    foreach(var answer in Answers)
                    {
                        answer.QuestionId = NewQuestion.Id;
                        handler.AddAnswer(answer);
                    }
                    Answers.Clear();
                    newQuestionView.DialogResult = true;
                });
            }
        }
        #endregion

        #region Ответ
        static NewAnswerView newAnswerView;
        private string answerText;
        public string AnswerText
        {
            get { return answerText; }
            set 
            {
                answerText = value;
                OnPropertyChanged();
            }
        }

        private bool answerOk;
        public bool AnswerOk
        {
            get { return answerOk; }
            set 
            { 
                answerOk = value; 
                OnPropertyChanged();
            }
        }

        private bool answerErr;
        public bool AnswerErr
        {
            get { return answerErr; }
            set
            {
                answerErr = value;
                OnPropertyChanged();
            }
        }

        static Answer NewAnswer;

        public RelayCommand AnswerComplete
        {
            get
            {
                return new RelayCommand((obj) =>
                {

                    if(string.IsNullOrEmpty(AnswerText) || (AnswerOk==false && AnswerErr==false))
                    {
                        MessageBox.Show("Ошибка заполнения ответа на вопрос","Ошибка");
                        return;
                    }

                    NewAnswer = new Answer()
                    {
                        Title = AnswerText,
                        Success = AnswerOk == true ? true : false,
                    };
                    newAnswerView.DialogResult = true;
                });
            }
        }
        #endregion


        public RelayCommand DeleteTest
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if(SelectedTest!=null)
                    {
                        handler.DeleteTest(SelectedTest);
                        PrintTests();
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] String prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
