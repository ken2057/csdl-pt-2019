namespace csdl_pt.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class qltv : DbContext
    {
        public qltv()
            : base("name=qltv")
        {
        }

        public virtual DbSet<BanSao> BanSaos { get; set; }
        public virtual DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public virtual DbSet<DangKy> DangKies { get; set; }
        public virtual DbSet<DocGia> DocGias { get; set; }
        public virtual DbSet<LoaiTaiLieu> LoaiTaiLieux { get; set; }
        public virtual DbSet<Muon> Muons { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<QuaTrinhMuon> QuaTrinhMuons { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<TaiLieu> TaiLieux { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanSao>()
                .Property(e => e.ma_tailieu)
                .IsUnicode(false);

            modelBuilder.Entity<BanSao>()
                .Property(e => e.ma_bansao)
                .IsUnicode(false);

            modelBuilder.Entity<BanSao>()
                .Property(e => e.ma_ChiNhanh)
                .IsUnicode(false);

            modelBuilder.Entity<BanSao>()
                .Property(e => e.tinhtrang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BanSao>()
                .HasOptional(e => e.Muon)
                .WithRequired(e => e.BanSao);

            modelBuilder.Entity<BanSao>()
                .HasMany(e => e.QuaTrinhMuons)
                .WithRequired(e => e.BanSao)
                .HasForeignKey(e => new { e.ma_tailieu, e.ma_bansao })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiNhanh>()
                .Property(e => e.ma_ChiNhanh)
                .IsUnicode(false);

            modelBuilder.Entity<ChiNhanh>()
                .HasMany(e => e.BanSaos)
                .WithRequired(e => e.ChiNhanh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiNhanh>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.ChiNhanh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.ma_tailieu)
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.ma_sinhvien)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .Property(e => e.ma_sinhvien)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .Property(e => e.diachi)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<DocGia>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.DocGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocGia>()
                .HasMany(e => e.Muons)
                .WithRequired(e => e.DocGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocGia>()
                .HasMany(e => e.QuaTrinhMuons)
                .WithRequired(e => e.DocGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiTaiLieu>()
                .Property(e => e.ma_loai)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiTaiLieu>()
                .HasMany(e => e.TaiLieux)
                .WithRequired(e => e.LoaiTaiLieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Muon>()
                .Property(e => e.ma_tailieu)
                .IsUnicode(false);

            modelBuilder.Entity<Muon>()
                .Property(e => e.ma_bansao)
                .IsUnicode(false);

            modelBuilder.Entity<Muon>()
                .Property(e => e.ma_nhanvien_dua)
                .IsUnicode(false);

            modelBuilder.Entity<Muon>()
                .Property(e => e.ma_sinhvien)
                .IsUnicode(false);

            modelBuilder.Entity<Muon>()
                .Property(e => e.tien_datcoc)
                .HasPrecision(19, 4);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.ma_nhanvien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.quyen)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.ma_ChiNhanh)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.Muons)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.ma_nhanvien_dua)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.QuaTrinhMuons)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.ma_nhanvien_dua)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.QuaTrinhMuons1)
                .WithRequired(e => e.NhanVien1)
                .HasForeignKey(e => e.ma_nhanvien_nhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.ma_tailieu)
                .IsUnicode(false);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.ma_bansao)
                .IsUnicode(false);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.ma_nhanvien_dua)
                .IsUnicode(false);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.ma_nhanvien_nhan)
                .IsUnicode(false);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.ma_sinhvien)
                .IsUnicode(false);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.tien_muon)
                .HasPrecision(19, 4);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.tien_datra)
                .HasPrecision(19, 4);

            modelBuilder.Entity<QuaTrinhMuon>()
                .Property(e => e.tien_datcoc)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.quyen1)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.ghichu)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.Quyen1)
                .HasForeignKey(e => e.quyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TacGia>()
                .Property(e => e.ma_tacgia)
                .IsUnicode(false);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.TaiLieux)
                .WithOptional(e => e.TacGia)
                .HasForeignKey(e => e.ma_tacgia_1);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.TaiLieux1)
                .WithOptional(e => e.TacGia1)
                .HasForeignKey(e => e.ma_tacgia_2);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.TaiLieux2)
                .WithOptional(e => e.TacGia2)
                .HasForeignKey(e => e.ma_tacgia_3);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.ma_tailieu)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.ma_tacgia_1)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.ma_tacgia_2)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.ma_tacgia_3)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.ma_loai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.tinhtrang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TaiLieu>()
                .HasMany(e => e.BanSaos)
                .WithRequired(e => e.TaiLieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiLieu>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.TaiLieu)
                .WillCascadeOnDelete(false);
        }
    }
}
