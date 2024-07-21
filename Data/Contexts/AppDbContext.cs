using Microsoft.EntityFrameworkCore;
using BanDongHoSolution.Data.Models;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;

namespace BanDongHoSolution.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public DbSet<CHITIETKM> CHITIETKMs { get; set; }
        public DbSet<DONHANG> DONHANGs { get; set; }
        public DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
        public DbSet<LOAITK> LOAITKs { get; set; }
        public DbSet<SANPHAM> SANPHAMs { get; set; }
        public DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public DbSet<THUONGHIEU> THUONGHIEUs { get; set; }

        // Use Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CHITIETDONHANG>(e => {
                e.ToTable("CHITIETDONHANG");
                e.HasKey(p => new { p.MASP, p.MADH});
            });

            modelBuilder.Entity<CHITIETDONHANG>()
            .HasOne(p => p.DONHANG)
            .WithMany(b => b.CHITIETDONHANGs)
            .HasForeignKey(p => p.MADH)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CHITIETDONHANG>()
            .HasOne(p => p.SANPHAM)
            .WithMany(b => b.CHITIETDONHANGs)
            .HasForeignKey(p => p.MASP)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CHITIETKM>(e => {
                e.ToTable("CHITIETKM");
                e.HasKey(p => new { p.MASP, p.MAKM });
                e.Property(e => e.MAKM).HasColumnType("char(7)");
            });

            modelBuilder.Entity<CHITIETKM>()
            .HasOne(p => p.KHUYENMAI)
            .WithMany(b => b.CHITIETKMs)
            .HasForeignKey(p => p.MAKM)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CHITIETKM>()
            .HasOne(p => p.SANPHAM)
            .WithMany(b => b.CHITIETKMs)
            .HasForeignKey(p => p.MASP)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DONHANG>(e => {
                e.ToTable("DONHANG");
                e.HasKey(p => p.MADH);
                e.Property(e => e.TRANGTHAI).HasColumnType("nvarchar(20)");
                e.Property(e => e.DIACHIGIAO).HasColumnType("ntext");
                e.Property(e => e.SDT).HasColumnType("varchar(12)");
                e.Property(e => e.MOTA).HasColumnType("ntext");
            });

            modelBuilder.Entity<DONHANG>()
            .HasOne(p => p.KHACHHANG)
            .WithMany(b => b.DONHANGs)
            .HasForeignKey(p => p.MAKH)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KHACHHANG>(e => {
                e.ToTable("KHACHHANG");
                e.HasKey(p => p.MAKH);
                e.Property(e => e.TENKH).HasColumnType("nvarchar(50)");
                e.Property(e => e.EMAIL).HasColumnType("varchar(50)");
                e.Property(e => e.SDT).HasColumnType("varchar(12)");
                e.Property(e => e.GIOITINH).HasColumnType("nvarchar(3)");
                e.Property(e => e.DIACHI).HasColumnType("ntext");
            });

            modelBuilder.Entity<KHACHHANG>()
            .HasOne(p => p.TAIKHOAN)
            .WithMany(b => b.KHACHHANGs)
            .HasForeignKey(p => p.MATK)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KHUYENMAI>(e => {
                e.ToTable("KHUYENMAI");
                e.HasKey(p => p.MAKM);
                e.Property(e => e.MAKM).HasColumnType("char(7)");
                e.Property(e => e.TENKM).HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<LOAISANPHAM>(e =>
            {
                e.ToTable("LOAISANPHAM");
                e.HasKey(p => p.MALOAISP);
                e.Property(e => e.MALOAISP).HasColumnType("char(7)");
                e.Property(e => e.TENLOAISP).HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<LOAITK>(e =>
            {
                e.ToTable("LOAITK");
                e.HasKey(p => p.MALOAITK); 
                e.Property(e => e.MALOAITK).HasColumnType("char(7)");
                e.Property(e => e.TENLOAITK).HasColumnType("nvarchar(20)");
            });

            modelBuilder.Entity<SANPHAM>(e =>
            {
                e.ToTable("SANPHAM");
                e.HasKey(p => p.MASP);
                e.Property(e => e.TENSP).HasColumnType("nvarchar(500)");
                e.Property(e => e.HINHLON).HasColumnType("nvarchar(500)");
                e.Property(e => e.HINHNHO).HasColumnType("nvarchar(500)");
                e.Property(e => e.MOTA).HasColumnType("ntext");
                e.Property(e => e.MALOAISP).HasColumnType("char(7)");
            });

            modelBuilder.Entity<SANPHAM>()
            .HasOne(p => p.THUONGHIEU)
            .WithMany(b => b.SANPHAMs)
            .HasForeignKey(p => p.MATH)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SANPHAM>()
            .HasOne(p => p.LOAISANPHAM)
            .WithMany(b => b.SANPHAMs)
            .HasForeignKey(p => p.MALOAISP)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TAIKHOAN>(e =>
            {
                e.ToTable("TAIKHOAN");
                e.HasKey(p => p.MATK);
                e.Property(e => e.TENDN).HasColumnType("varchar(20)");
                e.Property(e => e.MATKHAU).HasColumnType("varchar(MAX)");
                e.Property(e => e.MALOAITK).HasColumnType("char(7)");
            });

            modelBuilder.Entity<TAIKHOAN>()
            .HasOne(p => p.LOAITK)
            .WithMany(b => b.TAIKHOANs)
            .HasForeignKey(p => p.MALOAITK)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<THUONGHIEU>(e =>
            {
                e.ToTable("THUONGHIEU");
                e.HasKey(p => p.MATH);
                e.Property(e => e.TENTH).HasColumnType("nvarchar(50)");
                e.Property(e => e.HINHTH).HasColumnType("nvarchar(50)");
            });
        }
    }
}