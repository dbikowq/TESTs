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
    public class StudentViewModel:INotifyPropertyChanged
    {

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Grade> Grades { get; set; } = new ObservableCollection<Grade>();
        public ObservableCollection<Test> AllTests { get; set; } = new ObservableCollection<Test>();


        ContextHandler handler = new ContextHandler();
        public StudentViewModel()
        {
            foreach (var category in handler.GetCategories())
            {
                Categories.Add(category);
            }

            foreach (var grade in handler.GetGrades())
            {
                Grades.Add(grade);
            }

            PrintTests();
            _currentUser = handler.GetUserByFIO(StudentView.FIOStudent);
        }

        User _currentUser;

        public void PrintTests()
        {
            AllTests.Clear();
            foreach (var test in handler.GetAllTests())
            {
                AllTests.Add(test);
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
                foreach (var test in handler.GetAllTests().Where(x => x.CategoryId == selectedCategory.Id))
                {
                    if (SelectedGrade != null)
                    {
                        if (test.GradeId == SelectedGrade.Id)
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
                foreach (var test in handler.GetAllTests().Where(x => x.GradeId == SelectedGrade.Id))
                {
                    if (SelectedCategory != null)
                    {
                        if (test.CategoryId == SelectedCategory.Id)
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

        public RelayCommand StartTest
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if(SelectedTest!=null)
                    {
                        TestView testView = new TestView(SelectedTest, _currentUser);
                        if (testView.ShowDialog() == true)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Не выбран тест для прохождения","Ошибка");
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
