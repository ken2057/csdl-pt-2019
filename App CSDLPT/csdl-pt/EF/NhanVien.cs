namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            Muons = new HashSet<Muon>();
            QuaTrinhMuons = new HashSet<QuaTrinhMuon>();
            QuaTrinhMuons1 = new HashSet<QuaTrinhMuon>();
        }

        [Key]
        [StringLength(20)]
        public string ma_nhanvien { get; set; }

        [Required]
        [StringLength(15)]
        public string quyen { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_ChiNhanh { get; set; }

        [StringLength(50)]
        public string matkhau { get; set; }

        [StringLength(15)]
        public string sdt { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Muon> Muons { get; set; }

        public virtual Quyen Quyen1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhMuon> QuaTrinhMuons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhMuon> QuaTrinhMuons1 { get; set; }
    }
}
