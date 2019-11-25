namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuaTrinhMuon")]
    public partial class QuaTrinhMuon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string ma_tailieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ma_bansao { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime ngayGio_muon { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_nhanvien_dua { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_nhanvien_nhan { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_sinhvien { get; set; }

        public DateTime? ngay_hethan { get; set; }

        public DateTime? ngay_tra { get; set; }

        public DateTime? ngay_muon { get; set; }

        [Column(TypeName = "money")]
        public decimal? tien_muon { get; set; }

        [Column(TypeName = "money")]
        public decimal? tien_datra { get; set; }

        [Column(TypeName = "money")]
        public decimal? tien_datcoc { get; set; }

        [Column(TypeName = "ntext")]
        public string ghichu { get; set; }

        public virtual BanSao BanSao { get; set; }

        public virtual DocGia DocGia { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual NhanVien NhanVien1 { get; set; }
    }
}
