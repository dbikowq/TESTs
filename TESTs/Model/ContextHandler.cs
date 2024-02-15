using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TESTs.Entity;

namespace TESTs.Model
{
    public class ContextHandler
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private static TestDB _db;
        public ContextHandler()
        {
            if(_db == null)
            {
                TestDB testDB = new TestDB();
                _db = testDB;
            }
        }


        public bool Login(string username, string password, out User user)
        {
            user = null;
            if(_db.Users.Any(x=>x.Login==username && x.Password==password))
            {
                user = _db.Users.Include("UserRole").First(x=>x.Login==username && x.Password==password);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Registry(string login, string password, string fio)
        {
            if(_db.Users.Any(x=>x.Login==login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует","Ошибка");
                return false;
            }
            else
            {
                var role = _db.Roles.First(x => x.RoleName == "Ученик");
                _db.Users.Add(new User { Login = login, Password = password , UserRole = role, FIO = fio});
                _db.SaveChanges();
                return true;
            }
        }

        private List<Category> _categoryes;
        public List<Category>GetCategories()
        {
            if(_categoryes != null)
            {
                return _categoryes;
            }
            var categories = _db.Categories.ToList();
            _categoryes = categories;
            return categories;
        }

        private List<Grade> _grades;
        public List<Grade> GetGrades()
        {
            if(_grades!=null)
            {
                return _grades;
            }
            var grades = _db.Grades.ToList();
            _grades = grades;
            return grades;
        }

        private List<Test> _tests;
        public List<Test> GetAllTests()
        {
            if(_tests!=null)
            {
                return _tests;
            }
            var tests = _db.Tests.ToList();
            _tests = tests;
            return tests;
        }

        public List<Question>GetAllQuestionsByTestId(int testId)
        {
            var questions = _db.Questions.Where(x=>x.TestId==testId).ToList();
            return questions;
        }

        public List<Answer>GetAllAnswersByQuestionId(int questionId)
        {
            var answers = _db.Answers.Where(x=>x.QuestionId==questionId).ToList();
            return answers;
        }

        public User GetUserByFIO(String fio)
        {
            var user = _db.Users.First(x=>x.FIO==fio);
            return user;
        }

        public List<Result>GetAllResult()
        {
            var results = _db.Results.ToList();
            return results;
        }

        public void AddResult(Result result)
        {
            _db.Results.Add(result);
            _db.SaveChanges();
        }

        public void AddTest(Test test)
        {
            _db.Tests.Add(test);
            _db.SaveChanges();
            _tests = null;
        }

        public void AddQuestion(Question question)
        {
            _db.Questions.Add(question);
            _db.SaveChanges();
        }

        public void UpdateQuestion(Question question)
        {
            _db.Questions.AddOrUpdate(question);
            _db.SaveChanges();
        }

        public void AddAnswer(Answer answer)
        {
            _db.Answers.Add(answer);
            _db.SaveChanges();
        }

        public void DeleteTest(Test test)
        {
            var questions = _db.Questions.ToList();
            foreach(var question in questions.Where(x=>x.TestId==test.Id).ToList())
            {
                var answers = _db.Answers.Where(x => x.QuestionId == question.Id).ToList();
                foreach(var answer in answers)
                {
                    _db.Answers.Remove(answer);
                    _db.SaveChanges();
                }
                _db.Questions.Remove(question);
                _db.SaveChanges();
            }
            _db.Tests.Remove(test);
            _db.SaveChanges();
            _tests=null;
        }

        public void CreateAdminUser()
        {
            if(!_db.Users.Any(x=>x.Login=="admin" && x.Password=="admin"))
            {
                var role = _db.Roles.First(x => x.RoleName == "Учитель");
                _db.Users.Add(new User() { FIO="Администратор", Login="admin", Password="admin", UserRole = role});
                _db.SaveChanges();
            }
        }
    }
}
