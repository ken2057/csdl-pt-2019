namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiTaiLieu")]
    public partial class LoaiTaiLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiTaiLieu()
        {
            TaiLieux = new HashSet<TaiLieu>();
        }

        [Key]
        [StringLength(20)]
        public string ma_loai { get; set; }

        [StringLength(20)]
        public string ten_loai { get; set; }

        [Column(TypeName = "ntext")]
        public string ghichu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiLieu> TaiLieux { get; set; }
    }
}
