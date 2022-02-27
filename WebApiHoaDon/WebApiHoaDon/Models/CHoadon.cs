using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHoaDon.Models
{
    public class CHoadon
    {
        public string sohd { get; set; }

        public DateTime? ngaylaphd { get; set; }

        public string tenkh { get; set; }

        public ICollection<CChitietHoadon> chitiethoadons { get; set; }
    }
}