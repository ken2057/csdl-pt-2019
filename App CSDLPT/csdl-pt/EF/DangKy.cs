namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKy")]
    public partial class DangKy
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string ma_tailieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ma_sinhvien { get; set; }

        public DateTime? ngaygio_dk { get; set; }

        [Column(TypeName = "ntext")]
        public string ghichu { get; set; }

        public virtual DocGia DocGia { get; set; }

        public virtual TaiLieu TaiLieu { get; set; }
    }
}
