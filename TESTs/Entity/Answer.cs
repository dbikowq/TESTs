using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTs.Entity
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get;set; }
        public string Title { get; set; }
        public bool Success { get; set; }
    }
}
