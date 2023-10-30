using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ThanhThoaiRestaurant.Models
{
    public partial class QuanLyNhaHangContext : DbContext
    {
        public QuanLyNhaHangContext()
        {
        }

        public QuanLyNhaHangContext(DbContextOptions<QuanLyNhaHangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BanAn> BanAns { get; set; } = null!;
        public virtual DbSet<ChiTietDh> ChiTietDhs { get; set; } = null!;
        public virtual DbSet<ChiTietGm> ChiTietGms { get; set; } = null!;
        public virtual DbSet<ChiTietHd> ChiTietHds { get; set; } = null!;
        public virtual DbSet<ChiTietGh> ChiTietGhs { get; set; } = null!;
        public virtual DbSet<DatBan> DatBans { get; set; } = null!;
        public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
        public virtual DbSet<GioHang> GioHangs { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<MonAn> MonAns { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<NhomMonAn> NhomMonAns { get; set; } = null!;
        public virtual DbSet<PhieuGiamGium> PhieuGiamGia { get; set; } = null!;
        public virtual DbSet<PhieuGoiMon> PhieuGoiMons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
           
            =>    optionsBuilder.UseSqlServer("Server=MYCOMPUTER\\THANHTHOAI225;Database=QuanLyNhaHang;Integrated Security=True");
            
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<BanAn>(entity =>
            {
                entity.HasKey(e => e.MaBan);

                entity.ToTable("BanAn");

                entity.Property(e => e.MaBan)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TrangThaiBa).HasColumnName("TrangThaiBA");
            });

            modelBuilder.Entity<ChiTietDh>(entity =>
            {
                entity.HasKey(e => new { e.MaMon, e.MaDonHang })
                    .HasName("PK_DH");

                entity.ToTable("ChiTietDH");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaDonHang)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SoLuongMmdh).HasColumnName("SoLuongMMDH");

                entity.Property(e => e.TenMonAnDh)
                    .HasMaxLength(50)
                    .HasColumnName("TenMonAnDH");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany(p => p.ChiTietDhs)
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DH_CTDH");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.ChiTietDhs)
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MonAn_CTDH");
            });

            modelBuilder.Entity<ChiTietGh>(entity =>
            {
                entity.HasKey(e => new { e.MaMon, e.MaGioHang })
                    .HasName("PK_CTGH");

                entity.ToTable("ChiTietGH");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaGioHang)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SoLuongMM).HasColumnName("SoLuongMM");

                entity.Property(e => e.SoLuong).HasColumnName("SoLuong");

                entity.Property(e => e.TenMon)
                    .HasMaxLength(50)
                    .HasColumnName("TenMon");

                entity.Property(e => e.HinhAnh)
                    .HasMaxLength(50)
                    .HasColumnName("HinhAnh");

                entity.Property(e => e.GiaBan)
                    .IsRequired()
                    .HasColumnName("GiaBan");

                entity.Property(e => e.ThanhTien)
                    .IsRequired()
                    .HasColumnName("ThanhTien");

                entity.HasOne(d => d.MaGioHangNavigation)
                    .WithMany(p => p.ChiTietGhs)
                    .HasForeignKey(d => d.MaGioHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_GH1_CTGH");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.ChiTietGhs)
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MA_CTGH");
            });

            modelBuilder.Entity<ChiTietGm>(entity =>
            {
                entity.HasKey(e => new { e.MaMon, e.MaPhieuGm })
                    .HasName("PK_CTGM");

                entity.ToTable("ChiTietGM");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaPhieuGm)
                    .HasMaxLength(10)
                    .HasColumnName("MaPhieuGM")
                    .IsFixedLength();

                entity.Property(e => e.SoLuongCt1).HasColumnName("SoLuongCT1");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.ChiTietGms)
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MonAn_CTGM");

                entity.HasOne(d => d.MaPhieuGmNavigation)
                    .WithMany(p => p.ChiTietGms)
                    .HasForeignKey(d => d.MaPhieuGm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PGM_CTGM");
            });

            modelBuilder.Entity<ChiTietHd>(entity =>
            {
                entity.HasKey(e => new { e.MaHd, e.MaMon })
                    .HasName("PK_CTHD");

                entity.ToTable("ChiTietHD");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD")
                    .IsFixedLength();

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SoLuongCt).HasColumnName("SoLuongCT");

                entity.Property(e => e.ThanhTien).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.ChiTietHds)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_HD_CTHD");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.ChiTietHds)
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MonAn_CTHD");
            });

            modelBuilder.Entity<DatBan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DatBan");

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.MaBan)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaKH)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.MaLichHen)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NgayDat).HasColumnType("datetime");

                entity.Property(e => e.NgayHen).HasColumnType("datetime");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.Property(e => e.TrangThaiLh).HasColumnName("TrangThaiLH");

                entity.HasOne(d => d.MaBanNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaBan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DatBan_BanAn");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaKH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DatBan_Khachhang");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang);

                entity.ToTable("DonHang");

                entity.Property(e => e.MaDonHang)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD")
                    .IsFixedLength();

                entity.Property(e => e.MaKH)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.NgayDatHang).HasColumnType("datetime");

                entity.Property(e => e.PhiVanChuyen).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TrangThaiDh).HasColumnName("TrangThaiDH");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DonHang_HoaDon");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaKH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DonHang_Khachhang");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.ToTable("GioHang");

                

                entity.Property(e => e.MaGioHang)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

               

                entity.Property(e => e.SoLuongMon)
                    .IsRequired()
                    .HasColumnName("SoLuongMon");

                entity.Property(e => e.TongTien)
                    .IsRequired()
                    .HasColumnName("TongTien");

               
            });


            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD")
                    .IsFixedLength();

                entity.Property(e => e.MaPhieuGg)
                    .HasMaxLength(10)
                    .HasColumnName("MaPhieuGG")
                    .IsFixedLength();

                entity.Property(e => e.NgayHd)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayHD");

                entity.Property(e => e.TenMonAnHd)
                    .HasMaxLength(50)
                    .HasColumnName("TenMonAnHD");

                entity.Property(e => e.TienGiam).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TienTt)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TienTT");

                entity.Property(e => e.TongTien).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.MaPhieuGgNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaPhieuGg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_HoaDon_PGG");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKH);

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKH)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.DiaChiKh)
                    .HasMaxLength(100)
                    .HasColumnName("DiaChiKH");

                entity.Property(e => e.DoanhSo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.EmailKh)
                    .HasMaxLength(20)
                    .HasColumnName("EmailKH");

                entity.Property(e => e.GioiTinhKh)
                    .HasMaxLength(10)
                    .HasColumnName("GioiTinhKH");

                entity.Property(e => e.NgaySinhKh)
                    .HasColumnType("date")
                    .HasColumnName("NgaySinhKH");

                entity.Property(e => e.NgayTg)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayTG");

                entity.Property(e => e.Sdtkh)
                    .HasMaxLength(20)
                    .HasColumnName("SDTKH")
                    .IsFixedLength();

                entity.Property(e => e.TenDangNhap).HasMaxLength(20);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.HasOne(d => d.TenDangNhapNavigation)
                    .WithMany(p => p.KhachHangs)
                    .HasForeignKey(d => d.TenDangNhap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TenDN1");
            });

            modelBuilder.Entity<MonAn>(entity =>
            {
                entity.HasKey(e => e.MaMon);

                entity.ToTable("MonAn");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TrangThaiMA)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.GiaBan)
                    .IsRequired()
                    .HasColumnName("GiaBan");

                entity.Property(e => e.DonViTinh).HasMaxLength(10);

                entity.Property(e => e.HinhAnh)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.TenNhom)
                   .HasMaxLength(20)
                   .IsFixedLength();

                entity.Property(e => e.MaNhom)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MoTaDai).HasMaxLength(200);

                entity.Property(e => e.MoTaNgan).HasMaxLength(50);

                entity.Property(e => e.TenMon).HasMaxLength(50);

                entity.HasOne(d => d.MaNhomNavigation)
                    .WithMany(p => p.MonAns)
                    .HasForeignKey(d => d.MaNhom)
                    .HasConstraintName("fk_MaNhom");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.TenDangNhap)
                    .HasName("PK_NguoiDung_1");

                entity.ToTable("NguoiDung");

                entity.Property(e => e.TenDangNhap).HasMaxLength(20);

                entity.Property(e => e.EmailNd)
                    .HasMaxLength(20)
                    .HasColumnName("EmailND")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau).HasMaxLength(50);
                    


                entity.Property(e => e.VaiTro).HasMaxLength(20);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .HasColumnName("MaNV")
                    .IsFixedLength();

                entity.Property(e => e.Cccdnv)
                    .HasMaxLength(20)
                    .HasColumnName("CCCDNV")
                    .IsFixedLength();

                entity.Property(e => e.ChucVu).HasMaxLength(20);

                entity.Property(e => e.DiaChiNv)
                    .HasMaxLength(100)
                    .HasColumnName("DiaChiNV");

                entity.Property(e => e.EmailNv)
                    .HasMaxLength(20)
                    .HasColumnName("EmailNV");

                entity.Property(e => e.GioiTinhNv)
                    .HasMaxLength(10)
                    .HasColumnName("GioiTinhNV");

                entity.Property(e => e.NgaySinhNv)
                    .HasColumnType("date")
                    .HasColumnName("NgaySinhNV");

                entity.Property(e => e.NgayVl)
                    .HasColumnType("date")
                    .HasColumnName("NgayVL");

                entity.Property(e => e.Sdtnv)
                    .HasMaxLength(15)
                    .HasColumnName("SDTNV")
                    .IsFixedLength();

                entity.Property(e => e.TenDangNhap).HasMaxLength(20);

                entity.Property(e => e.TenNv)
                    .HasMaxLength(50)
                    .HasColumnName("TenNV");

                entity.HasOne(d => d.TenDangNhapNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.TenDangNhap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TenDN");
            });

            modelBuilder.Entity<NhomMonAn>(entity =>
            {
                entity.HasKey(e => e.MaNhom);

                entity.ToTable("NhomMonAn");

                entity.Property(e => e.MaNhom)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TenNhom).HasMaxLength(20);
            });

            modelBuilder.Entity<PhieuGiamGium>(entity =>
            {
                entity.HasKey(e => e.MaPhieuGg);

                entity.Property(e => e.MaPhieuGg)
                    .HasMaxLength(10)
                    .HasColumnName("MaPhieuGG")
                    .IsFixedLength();

                entity.Property(e => e.LoaiMa).HasMaxLength(20);

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.TrangThaiPgg).HasColumnName("TrangThaiPGG");
            });

            modelBuilder.Entity<PhieuGoiMon>(entity =>
            {
                entity.HasKey(e => e.MaPhieuGm);

                entity.ToTable("PhieuGoiMon");

                entity.Property(e => e.MaPhieuGm)
                    .HasMaxLength(10)
                    .HasColumnName("MaPhieuGM")
                    .IsFixedLength();

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.MaBan)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaHd)
                    .HasMaxLength(10)
                    .HasColumnName("MaHD")
                    .IsFixedLength();

                entity.Property(e => e.NgayGm)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayGM");

                entity.Property(e => e.TenMonAnPgm)
                    .HasMaxLength(50)
                    .HasColumnName("TenMonAnPGM");

                entity.HasOne(d => d.MaBanNavigation)
                    .WithMany(p => p.PhieuGoiMons)
                    .HasForeignKey(d => d.MaBan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PhieuGoiMon_BanAn");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.PhieuGoiMons)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PhieuGoiMon_HoaDon");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
