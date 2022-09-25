using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class ClassificationModel : IClassificationModel
    {
        public int id { get; set; }

        public string ClassificationNameEn { get; set; }

        public string ClassificationNameAr { get; set; }
    }
}
