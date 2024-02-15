using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTs.Entity
{
    public class User
    {
        public int Id { get; set; }
        public String FIO { get; set; }
        public String Login { get;set; }
        public String Password { get; set; }
        public Role UserRole { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public String RoleName { get; set; }
    }

}
