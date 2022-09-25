using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SchoolFullDetailsModel : ISchoolModel
    {
        public int SchoolId { get; set; }

        public string SchoolNameEn { get; set; }

        public string SchoolNameAr { get; set; }

        public string GenderNameEn { get; set; }

        public string GenderNameAr { get; set; }

        public string ClassificationNameEn { get; set; }

        public string ClassificationNameAr { get; set; }

        public string TerritoryNameEn { get; set; }

        public string TerritoryNameAr { get; set; }

        public string LevelNameEn { get; set; }

        public string LevelNameAr { get; set; }

        public int LevelNo { get; set; }

        public int TerritoryNo { get; set; }

        public int ClassificationNo { get; set; }

        public int GenderNo { get; set; }

        public string GeneratedQRCode { get; set; }
        public int? SatisfactoryRateId { get; set; }
    }
}
