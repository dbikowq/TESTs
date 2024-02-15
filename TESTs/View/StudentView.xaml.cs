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
    /// Логика взаимодействия для StudentView.xaml
    /// </summary>
    
    public partial class StudentView : Window
    {
        public static String FIOStudent;
        public StudentView()
        {
            InitializeComponent();
        }
        private User _user;
        public StudentView(User user)
        {
            FIOStudent = user.FIO;
            InitializeComponent();
            _user = user;
            this.Title = user.FIO;
        }
    }
}
