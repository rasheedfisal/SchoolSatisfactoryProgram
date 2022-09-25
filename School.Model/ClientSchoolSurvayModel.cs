using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class ClientSchoolSurvayModel
    {
        public int id { get; set; }
        public DateTime DateAdded { get; set; }

        public string Others { get; set; }

        public string ClientQualification { get; set; }

        public string ClientName { get; set; }

        public int SatisfactoryRateNo { get; set; }

        public int SchoolNo { get; set; }

        public string SchoolNameAr { get; set; }

        public int LevelNo { get; set; }

        public int TerritoryNo { get; set; }

        public int ClassificationNo { get; set; }

        public int GenderNo { get; set; }
    }
}
