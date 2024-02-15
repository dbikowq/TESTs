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

namespace TESTs.View
{
    /// <summary>
    /// Логика взаимодействия для TeacherView.xaml
    /// </summary>
    public partial class TeacherView : Window
    {
        public TeacherView()
        {
            InitializeComponent();
        }
        private User _user { get; set; }
        public TeacherView(User user)
        {
            InitializeComponent();
            _user = user;
            this.Title = user.FIO;
        }
    }
}
