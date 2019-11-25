namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocGia")]
    public partial class DocGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocGia()
        {
            DangKies = new HashSet<DangKy>();
            Muons = new HashSet<Muon>();
            QuaTrinhMuons = new HashSet<QuaTrinhMuon>();
        }

        [Key]
        [StringLength(20)]
        public string ma_sinhvien { get; set; }

        [StringLength(50)]
        public string hoten { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string diachi { get; set; }

        [StringLength(15)]
        public string sdt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKy> DangKies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Muon> Muons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhMuon> QuaTrinhMuons { get; set; }
    }
}
