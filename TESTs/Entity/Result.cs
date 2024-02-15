using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTs.Entity
{
    public class Result
    {
        public int Id { get; set; } 
        public string FIO { get; set; }
        public DateTime DateResult { get; set; }
        public string NameTest { get; set; }
        public string ResultTest { get; set; }

        public override string ToString()
        {
            return $"{DateResult.ToLongDateString()}|{FIO}|{NameTest}|{ResultTest}";
        }
    }
}
