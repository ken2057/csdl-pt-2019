namespace csdl_pt.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BanSao")]
    public partial class BanSao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BanSao()
        {
            QuaTrinhMuons = new HashSet<QuaTrinhMuon>();
        }

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
        public string ma_ChiNhanh { get; set; }

        [StringLength(1)]
        public string tinhtrang { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual TaiLieu TaiLieu { get; set; }

        public virtual Muon Muon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhMuon> QuaTrinhMuons { get; set; }
    }
}
