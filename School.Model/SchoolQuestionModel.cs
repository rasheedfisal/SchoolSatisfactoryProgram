using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SchoolQuestionModel
    {
        public int id { get; set; }

        public string QuestionNameEn { get; set; }

        public string QuestionNameAr { get; set; }

        public int SchoolNo { get; set; }

        public int SchoolSatisfactoryNo { get; set; }
        public List<SchoolQuestionAnswerModel> QuestionAnswers { get; set; }

    }
}
