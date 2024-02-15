using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTs.Entity
{
    public class Grade
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
