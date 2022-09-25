using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SchoolModel : ISchool
    {
        public int id { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }
    }
}
