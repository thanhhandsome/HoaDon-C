using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHoaDon.Models
{
    public class CHanghoa
    {
        public string mahang { get; set; }

        public string tenhang { get; set; }

        public string dvt { get; set; }

        public double? dongia { get; set; }

        public ICollection<CChitietHoadon> chitiethoadons { get; set; }
    }
}