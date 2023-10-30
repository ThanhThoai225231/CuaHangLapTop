using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;


namespace ThanhThoaiRestaurant.Models
{
    public  class NguoiDung 
    {
        public NguoiDung()
        {
            KhachHangs = new HashSet<KhachHang>();
            NhanViens = new HashSet<NhanVien>();
        }

        
        public string TenDangNhap { get; set; }

       
        public string MatKhau { get; set; }

        
        public string EmailNd { get; set; }

       
        public string VaiTro { get; set; } 

       
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }

    }
}
