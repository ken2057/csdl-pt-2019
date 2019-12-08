using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csdl_pt.View
{
    class rp_LsuMuon
    {
        //ma_tailieu, ma_bansao, ngay_hethan, ngay_muon, ma_sinhvien, ngay_tra, ma_sinhvien, ma_nhanvien_dua, ma_nhanvien_nhan
        public string ma_tailieu { get; set; }
        public string ma_bansao { get; set; }
        public DateTime ngay_hethan { get; set; }
        public DateTime ngay_muon { get; set; }
        public string ma_sinhvien { get; set; }
        public DateTime ngay_tra { get; set; }

        public string ma_nhanvien_dua { get; set; }
        public string ma_nhanvien_nhan { get; set; }
    }
}
