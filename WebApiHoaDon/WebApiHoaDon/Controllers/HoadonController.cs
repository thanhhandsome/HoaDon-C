using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiHoaDon.Controllers
{
    public class HoadonController : ApiController
    {
        private Models.ModelHoaDon dc = new Models.ModelHoaDon();
        public IHttpActionResult getDSHoadon()
        {
            var kq = dc.hoadons.Select(x => new Models.CHoadon
            {
                sohd = x.sohd,
                ngaylaphd = x.ngaylaphd,
                tenkh = x.tenkh
            }) ;
            return Ok(kq.ToList());
        }

        public IHttpActionResult getDSHoadon(string id)
        {
            Models.hoadon t = dc.hoadons.Find(id);
            if (t == null) return NotFound();
            Models.CHoadon hd = new Models.CHoadon
            {
                sohd = t.sohd,
                ngaylaphd = t.ngaylaphd,
                tenkh = t.tenkh,
                chitiethoadons = t.chitiethoadons.Select(x => new Models.CChitietHoadon
                {
                    sohd=x.sohd,
                    mahang=x.mahang,
                    dongia=x.dongia,
                    soluong=x.soluong,
                    hanghoa=new Models.CHanghoa
                    {
                        mahang=x.hanghoa.mahang,
                        tenhang=x.hanghoa.tenhang,
                        dvt=x.hanghoa.dvt,
                        dongia=x.hanghoa.dongia
                    }
                }).ToList()
            };
            return Ok(hd);
        }
        public IHttpActionResult postDSHoadon(Models.CHoadon hd)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.hoadon a = new Models.hoadon
            {
                sohd = hd.sohd,
                ngaylaphd = hd.ngaylaphd,
                tenkh = hd.tenkh,
                chitiethoadons = hd.chitiethoadons.Select(x=>new Models.chitiethoadon
                {
                    sohd = x.sohd,
                    mahang = x.mahang,
                    soluong = x.soluong,
                    dongia =x.dongia,
                }).ToList()
            };
            dc.hoadons.Add(a);
            dc.SaveChanges();
            return Ok(hd);
        }
    }
}
