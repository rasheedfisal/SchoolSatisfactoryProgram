using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class ClientAnswerModel
    {
        public int QuestionNo { get; set; }

        public int AnswerNo { get; set; }
        public int SchoolSatisfactoryRateNo { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
