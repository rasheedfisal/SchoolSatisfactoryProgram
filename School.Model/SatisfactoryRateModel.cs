using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SatisfactoryRateModel : ISatisfactoryRateModel
    {
        public int id { get; set; }

        public string RateNameEn { get; set; }

        public string RateNameAr { get; set; }
    }
}
