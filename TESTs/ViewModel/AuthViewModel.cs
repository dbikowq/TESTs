using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TESTs.Model;
using TESTs.View;

namespace TESTs.ViewModel
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private String login;
        public String Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private String pass;
        public String Pass
        {
            get { return pass; }
            set
            {
                pass = value;
                OnPropertyChanged();
            }
        }

        private String fio;
        public String FIO
        {
            get { return fio; }
            set
            {
                fio = value;
                OnPropertyChanged();
            }
        }

        ContextHandler handler = new ContextHandler();

        public AuthViewModel()
        {
            Login = "admin";
            Pass = "admin";

            handler.CreateAdminUser();
        }


        public RelayCommand Auth
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if(handler.Login(Login,Pass, out var user))
                    {
                        switch(user.UserRole.RoleName)
                        {
                            case "Ученик":
                                StudentView studentView = new StudentView(user);
                                if (studentView.ShowDialog() == true)
                                {

                                }
                                break;
                            case "Учитель":
                                TeacherView teacherView = new TeacherView(user);
                                if (teacherView.ShowDialog() == true)
                                {

                                }
                                break;
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден","Ошибка");
                    }    
                });
            }
        }

        public RelayCommand OpenRegistryForm
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    Registry reg = new Registry();
                    reg.Show();
                });
            }
        }

        public RelayCommand Registry
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    handler.Registry(Login, Pass, FIO);
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
