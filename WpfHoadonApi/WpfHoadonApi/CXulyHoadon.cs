using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace WpfHoadonApi
{
    class CXulyHoadon
    {
        private static HttpClient hc = new HttpClient();
        public static List<CHoadon> getDSHoadon()
        {
            try
            {
                string url = @"https://localhost:44363/api/HoaDon";
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<CHoadon>>();
                list.Wait();
                return list.Result;
            }catch(Exception)
            {
                return null;
            }
        }

        public static CHoadon getHoadon(string sohd)
        {
            try
            {
                string url = @"https://localhost:44363/api/HoaDon/"+sohd;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<CHoadon>();
                list.Wait();
                return list.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static IEnumerable<object> getDSChitietHoadon(CHoadon hd)
        {
            return hd.chitiethoadons.Select(x => new
            {
                mahang=x.mahang,
                tenhang=x.hanghoa.tenhang,
                dvt=x.hanghoa.dvt,
                dongia=x.dongia,
                soluong=x.soluong,
                thanhtien=x.dongia.Value*x.soluong.Value
            });
        }
        public static double getThanhtienHoadon(CHoadon hd)
        {
            return hd.chitiethoadons.Sum(x => x.dongia.Value * x.soluong.Value);
        }

        public static bool themHoadon(CHoadon hd)
        {
            try
            {
                string url = @"https://localhost:44363/api/HoaDon/" ;
                var kq = hc.PostAsJsonAsync(url,hd);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
