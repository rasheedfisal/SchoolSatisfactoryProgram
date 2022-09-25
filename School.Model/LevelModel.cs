using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class LevelModel : ILevelModel
    {
        public int id { get; set; }

        public string LevelNameEn { get; set; }

        public string LevelNameAr { get; set; }
    }
}
