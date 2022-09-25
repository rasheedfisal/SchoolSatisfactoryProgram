using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SchoolQuestionAnswerModel
    {
        public int id { get; set; }

        public int SchoolQuestionNo { get; set; }

        public string AnswerNameEn { get; set; }

        public string AnswerNameAr { get; set; }
    }
}
