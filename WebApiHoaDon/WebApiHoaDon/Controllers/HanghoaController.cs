using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiHoaDon.Controllers
{
    public class HanghoaController : ApiController
    {
        private Models.ModelHoaDon dc = new Models.ModelHoaDon();
        public IHttpActionResult getDSHanghoa()
        {
            var kq = dc.hanghoas.Select(x => new Models.CHanghoa
            {
                mahang=x.mahang,
                tenhang=x.tenhang,
                dvt=x.dvt,
                dongia=x.dongia,
            });
            return Ok(kq.ToList());
        }
    }
}
