namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Muon")]
    public partial class Muon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string ma_tailieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ma_bansao { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_nhanvien_dua { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_sinhvien { get; set; }

        public DateTime? ngay_muon { get; set; }

        public DateTime? ngay_hethan { get; set; }

        [Column(TypeName = "money")]
        public decimal? tien_datcoc { get; set; }

        public virtual BanSao BanSao { get; set; }

        public virtual DocGia DocGia { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
