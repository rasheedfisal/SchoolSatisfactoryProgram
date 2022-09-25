using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class QuestionAnswerFullDetails
    {
        public int SchoolId { get; set; }

        public string SchoolNameEn { get; set; }

        public string SchoolNameAr { get; set; }

        public string QuestionNameEn { get; set; }

        public string QuestionNameAr { get; set; }

        public int SchoolNo { get; set; }

        public int SchoolQuestionNo { get; set; }

        public string AnswerNameEn { get; set; }

        public string AnswerNameAr { get; set; }

        public int SchoolQuestionAnswerId { get; set; }

        public string RateNameEn { get; set; }

        public string RateNameAr { get; set; }

        public int SatisfactoryRateId { get; set; }
    }
}
