using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model
{
    public class SchoolQRCodeModel
    {
        public int id { get; set; }

        public int SchoolNo { get; set; }

        public string GeneratedQRCode { get; set; }
    }
}
