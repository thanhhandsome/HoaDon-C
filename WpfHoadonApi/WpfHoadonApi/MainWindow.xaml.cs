using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfHoadonApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CHoadon tempHD = new CHoadon
        {
            sohd = "",
            ngaylaphd = DateTime.Now,
            tenkh = "",

            chitiethoadons = new List<CChitietHoadon>()
        };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            List<CHoadon> ds = CXulyHoadon.getDSHoadon();
            if (ds == null) MessageBox.Show("Loi ket noi voi server!!!");
            else dgHoadon.ItemsSource = ds;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hienthi();
            List<CHanghoa> ds = CXulyHanghoa.getDSHanghoa();
            
            if (ds == null) MessageBox.Show("Loi ket noi voi server!!!");
            else cmbMahang.ItemsSource = ds;
        }

        private void dgHoadon_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            CHoadon t = e.Row.Item as CHoadon;
            DataGrid dg = e.DetailsElement.FindName("dgCTHD") as DataGrid;
            TextBox txt = e.DetailsElement.FindName("txtThanhtien") as TextBox;

            CHoadon hd = CXulyHoadon.getHoadon(t.sohd);
            if (hd == null) MessageBox.Show("Khong doc duoc du lieu!!!");
            else
            {
                dgChitiet.ItemsSource = CXulyHoadon.getDSChitietHoadon(hd);
                txt.Text = CXulyHoadon.getDSChitietHoadon(hd).ToString();
            }
        }
        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMahang.SelectedItem == null) return;
            CChitietHoadon ct = new CChitietHoadon();
            ct.hanghoa = cmbMahang.SelectedItem as CHanghoa;
            ct.mahang = ct.hanghoa.mahang;
            ct.dongia = ct.hanghoa.dongia;
            ct.soluong = int.Parse(txtSoluong.Text);
            tempHD.chitiethoadons.Add(ct);
            dgChitiet.ItemsSource = CXulyHoadon.getDSChitietHoadon(tempHD);
            txtThanhtien.Text = CXulyHoadon.getThanhtienHoadon(tempHD).ToString();
        }

        private void btnLapHD_Click(object sender, RoutedEventArgs e)
        {
            CHoadon hd = new CHoadon();
            hd.sohd = txtSoHD.Text;
            hd.ngaylaphd = dpNgaylapHD.SelectedDate;
            hd.tenkh = txtTenKH.Text;
            hd.chitiethoadons = tempHD.chitiethoadons.Select(x => new CChitietHoadon
            {
                sohd = hd.sohd,
                mahang = x.mahang,
                dongia = x.dongia,
                soluong =x.soluong
            }).ToList();
            bool ok = CXulyHoadon.themHoadon(hd);
            if (ok == false) MessageBox.Show("khong ghi duoc hoa don");
            else
            {
                hienthi();
                tempHD.chitiethoadons.Clear();
                dgChitiet.ItemsSource = CXulyHoadon.getDSChitietHoadon(tempHD);
                txtThanhtien.Text = CXulyHoadon.getThanhtienHoadon(tempHD).ToString();
            }
            
        }
    }
}
