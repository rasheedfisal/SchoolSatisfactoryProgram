using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class TerritoryModel : ITerritoryModel
    {
        public int id { get; set; }

        public string TerritoryNameEn { get; set; }

        public string TerritoryNameAr { get; set; }
    }
}
