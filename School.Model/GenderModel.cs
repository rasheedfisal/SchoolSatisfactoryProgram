using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class GenderModel : IGenderModel
    {
        public int id { get; set; }

        public string GenderNameEn { get; set; }

        public string GenderNameAr { get; set; }
    }
}
