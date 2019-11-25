namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiLieu")]
    public partial class TaiLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiLieu()
        {
            BanSaos = new HashSet<BanSao>();
            DangKies = new HashSet<DangKy>();
        }

        [Key]
        [StringLength(20)]
        public string ma_tailieu { get; set; }

        [StringLength(20)]
        public string ma_tacgia_1 { get; set; }

        [StringLength(20)]
        public string ma_tacgia_2 { get; set; }

        [StringLength(20)]
        public string ma_tacgia_3 { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_loai { get; set; }

        [StringLength(15)]
        public string ngonngu { get; set; }

        [StringLength(20)]
        public string bia { get; set; }

        [StringLength(1)]
        public string tinhtrang { get; set; }

        [Column(TypeName = "money")]
        public decimal? gia { get; set; }

        [StringLength(100)]
        public string ten_tailieu { get; set; }

        public byte? sl_kho { get; set; }

        [Column(TypeName = "ntext")]
        public string tomtat { get; set; }

        public DateTime? ngay_phathanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BanSao> BanSaos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKy> DangKies { get; set; }

        public virtual LoaiTaiLieu LoaiTaiLieu { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual TacGia TacGia1 { get; set; }

        public virtual TacGia TacGia2 { get; set; }
    }
}
