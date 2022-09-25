using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SchoolSatisfactoryModel
    {
        public int id { get; set; }

        public int SchoolNo { get; set; }

        public int SatisfactoryRateNo { get; set; }
        public string ClientName { get; set; }
        public string ClientQualification { get; set; }
        public string Others { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
