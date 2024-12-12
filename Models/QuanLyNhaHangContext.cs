using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;


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
        public virtual DbSet<OCung> OCungs { get; set; } = null!;
        public virtual DbSet<PhieuGiamGium> PhieuGiamGia { get; set; } = null!;
        public virtual DbSet<PhieuGoiMon> PhieuGoiMons { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public virtual DbSet<RAM> RAMs { get; set; } = null!;
        public virtual DbSet<CPU> CPUs { get; set; } = null!;
        public virtual DbSet<ManHinh> ManHinhs { get; set; } = null!;

        public virtual DbSet<TinNhan> TinNhans { get; set; } = null!;

        public virtual DbSet<ChiTietTinNhan> ChiTietTinNhans { get; set; } = null!;

        public virtual DbSet<DanhGia> DanhGias { get; set; } = null!;
		public virtual DbSet<KhuyenMai> KhuyenMais { get; set; } = null!;
        public virtual DbSet<LoHang> LoHangs { get; set; } = null!;

        public virtual DbSet<ChiTietLH> ChiTietLHs { get; set; } = null!;

        public virtual DbSet<HanhViND> HanhViNDs { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


            => optionsBuilder.UseSqlServer("Server=MYCOMPUTER\\THANHTHOAI225;Database=QuanLyNhaHang;Integrated Security=True; TrustServerCertificate=True");



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TinNhan>(entity =>
            {
                entity.HasKey(e => e.MaTinNhan);

                entity.ToTable("TinNhan");

                entity.Property(e => e.MaTinNhan)
                    .HasMaxLength(10)
                    .IsFixedLength();

                
            });

            modelBuilder.Entity<HanhViND>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("HanhViND");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.HanhDong)
                    .HasMaxLength(10)
                    .IsFixedLength();

            });

            modelBuilder.Entity<BanAn>(entity =>
            {
                entity.HasKey(e => e.MaBan);

                entity.ToTable("BanAn");

                entity.Property(e => e.MaBan)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TrangThaiBa).HasColumnName("TrangThaiBA");
            });

			modelBuilder.Entity<KhuyenMai>(entity =>
			{
				entity.HasKey(e => e.MaKM);

				entity.ToTable("KhuyenMai");

				entity.Property(e => e.MaKM)
					.HasMaxLength(10)
					.IsFixedLength();

				entity.Property(e => e.TenKM)
					.HasMaxLength(50)
					.IsFixedLength();

				entity.Property(e => e.MaKH)
					.HasMaxLength(10)
					.IsFixedLength();

				entity.Property(e => e.GiaGiam)
					.HasMaxLength(10)
					.IsFixedLength();

				entity.Property(e => e.NgayBD).HasColumnType("datetime");
				entity.Property(e => e.NgayKT).HasColumnType("datetime");

			});


			modelBuilder.Entity<ChiTietTinNhan>(entity =>
            {
                entity.HasKey(e => new { e.MaTinNhan, e.TenDangNhap })
                    .HasName("PK_ChiTietTinNhan");

                entity.ToTable("ChiTietTinNhan");

                entity.Property(e => e.MaTinNhan)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NoiDung).HasColumnName("NoiDung");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

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

                entity.Property(e => e.HinhAnhCt)
                   .HasMaxLength(50)
                   .HasColumnName("HinhAnhCt");

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

                    .HasColumnName("MaHD")
                    .IsFixedLength();

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();



                entity.Property(e => e.SoLuongCt).HasColumnName("SoLuongCT");

                entity.Property(e => e.HinhAnhHd).HasColumnName("HinhAnhHd");

                entity.Property(e => e.ThanhTien)
                   .IsRequired()
                   .HasColumnName("ThanhTien");



                entity.Property(e => e.TenMon).HasMaxLength(50);

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

                    .HasColumnName("MaHD")
                    .IsFixedLength();

                entity.Property(e => e.MaKH)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.NguoiNhan)
                    .HasMaxLength(50)
                    .HasColumnName("NguoiNhan")
                    .IsFixedLength();

                entity.Property(e => e.SDTNN)
                    .HasMaxLength(10)
                    .HasColumnName("SDTNN")
                    .IsFixedLength();

                entity.Property(e => e.DiaChiNhan)
                    .HasMaxLength(10)
                    .HasColumnName("DiaChiNhan")
                    .IsFixedLength();

                entity.Property(e => e.GhiChu)
                    .HasMaxLength(10)
                    .HasColumnName("GhiChu")
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

            modelBuilder.Entity<DanhGia>(entity =>
            {
                entity.HasKey(e => e.MaDanhGia);

                entity.ToTable("DanhGia");

                entity.Property(e => e.MaDanhGia)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaDanhGia)

                    .HasColumnName("MaDanhGia")
                    .IsFixedLength();

				entity.Property(e => e.HinhAnh1)

					.HasColumnName("HinhAnh1")
					.IsFixedLength();

				entity.Property(e => e.HinhAnh2)

					.HasColumnName("HinhAnh2")
					.IsFixedLength();

				entity.Property(e => e.HinhAnh3)

					.HasColumnName("HinhAnh3")
					.IsFixedLength();

				entity.Property(e => e.HinhAnh4)

					.HasColumnName("HinhAnh4")
					.IsFixedLength();

				entity.Property(e => e.HinhAnh5)

					.HasColumnName("HinhAnh5")
					.IsFixedLength();

				entity.Property(e => e.Video)

					.HasColumnName("Video")
					.IsFixedLength();

				entity.Property(e => e.TenDangNhap)
                    
                    .HasColumnName("TenDangNhap")
                    .IsFixedLength();

                entity.Property(e => e.MaMon)
                    
                    .HasColumnName("MaMon")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(200)
                    .HasColumnName("NoiDung")
                    .IsFixedLength();

                entity.Property(e => e.Diem)
                    
                    .HasColumnName("Diem")
                    .IsFixedLength();

                entity.Property(e => e.NgayDG).HasColumnType("datetime");         
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

                entity.Property(e => e.TongGiamGia)
                    .IsRequired()
                    .HasColumnName("TongGiamGia");

                entity.Property(e => e.TienThanhToan)
                    .IsRequired()
                    .HasColumnName("TienThanhToan");
            });


            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd)
                    .IsRequired()
                     .HasColumnName("MaHD");

                entity.Property(e => e.MaPhieuGg)
                    .HasMaxLength(10)
                    .HasColumnName("MaPhieuGG")
                    .IsFixedLength();

                entity.Property(e => e.NgayHd)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayHD");



                entity.Property(e => e.HinhThucTT)
                   .HasMaxLength(50)
                   .HasColumnName("HinhThucTT");


                entity.Property(e => e.TienTt)
                     .IsRequired()
                     .HasColumnName("TienTT");


                entity.Property(e => e.TienGiam)
                    .IsRequired()
                    .HasColumnName("TienGiam");


                entity.Property(e => e.TongTien)
                     .IsRequired()
                     .HasColumnName("TongTien");

                entity.Property(e => e.TrangThaiHD).HasColumnName("TrangThaiHD");

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
                    .HasMaxLength(50)
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

                entity.Property(e => e.GiaGoc)
                    .IsRequired()
                    .HasColumnName("GiaGoc");

                entity.Property(e => e.GiaKhuyenMai)
                    .IsRequired()
                    .HasColumnName("GiaKhuyenMai");

                entity.Property(e => e.DonViTinh).HasMaxLength(10);

                entity.Property(e => e.HinhAnh)
                    .HasMaxLength(20)
                    .IsFixedLength();
				entity.Property(e => e.HinhAnh1)
				   .HasMaxLength(20)
				   .IsFixedLength();
				entity.Property(e => e.HinhAnh2)
				   .HasMaxLength(20)
				   .IsFixedLength();
				entity.Property(e => e.HinhAnh3)
				   .HasMaxLength(20)
				   .IsFixedLength();
				

				entity.Property(e => e.TenNhom)
                   .HasMaxLength(20)
                   .IsFixedLength();

                entity.Property(e => e.MaNhom)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaOC)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.MaMH)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.MaRam)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.MaCPU)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SoLuongDaBan)
                   .HasMaxLength(10)
                   .IsFixedLength();

				entity.Property(e => e.NgayMoBan).HasColumnType("datetime");


				entity.Property(e => e.MoTaDai).HasMaxLength(200);

                entity.Property(e => e.MoTaNgan).HasMaxLength(50);

                entity.Property(e => e.TenMon).HasMaxLength(50);

                entity.HasOne(d => d.MaNhomNavigation)
                    .WithMany(p => p.MonAns)
                    .HasForeignKey(d => d.MaNhom)
                    .HasConstraintName("fk_MaNhom");
            });

            modelBuilder.Entity<LoHang>(entity =>
            {
                entity.HasKey(e => e.MaLH);

                entity.ToTable("LoHang");

                entity.Property(e => e.MaLH)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NgayNhap)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.GiaLo)
                    .IsRequired()
                    .HasColumnName("GiaLo");

                entity.Property(e => e.TrangThaiLH)
                    .IsRequired()
                    .HasColumnName("TrangThaiLH");

                entity.Property(e => e.MaNPP)
                    .IsRequired()
                    .HasColumnName("MaNPP");   
                
                entity.HasMany(lh => lh.ChiTietLHs)
                      .WithOne(ct => ct.LoHang)
                     .HasForeignKey(ct => ct.MaLH);

            });

            modelBuilder.Entity<ChiTietLH>(entity =>
            {
                entity.HasKey(e => new { e.MaMon, e.MaLH });
                    

                entity.ToTable("ChiTietLH");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaLH)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.GiaNhap)
                    .IsRequired()
                    .HasColumnName("GiaNhap");

                entity.Property(e => e.SoLuong)
                    .IsRequired()
                    .HasColumnName("SoLuong");

                entity.Property(e => e.DaBan)
                    .IsRequired()
                    .HasColumnName("DaBan");

                entity.Property(e => e.NgayNhapLH)
                    .IsRequired()
                    .HasColumnName("NgayNhapLH");

                entity.Property(e => e.HinhAnh).HasMaxLength(20);

                
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.TenDangNhap)
                    .HasName("PK_NguoiDung_1");

                entity.ToTable("NguoiDung");

                entity.Property(e => e.TenDangNhap).HasMaxLength(20);

                entity.Property(e => e.EmailNd)
                    .HasMaxLength(50)
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

                entity.Property(e => e.HinhAnh)
                    .HasMaxLength(20)
                    .HasColumnName("HinhAnh")
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

            modelBuilder.Entity<OCung>(entity =>
            {
                entity.HasKey(e => e.MaOC);

                entity.ToTable("OCung");

                entity.Property(e => e.MaOC)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DungLuong).HasMaxLength(20);
            });

            modelBuilder.Entity<RAM>(entity =>
            {
                entity.HasKey(e => e.MaRam);

                entity.ToTable("RAM");

                entity.Property(e => e.MaRam)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DungLuongRam).HasMaxLength(20);
            });

            modelBuilder.Entity<CPU>(entity =>
            {
                entity.HasKey(e => e.MaCPU);

                entity.ToTable("CPU");

                entity.Property(e => e.MaCPU)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TenLoaiCPU).HasMaxLength(20);
            });

            modelBuilder.Entity<ManHinh>(entity =>
            {
                entity.HasKey(e => e.MaMH);

                entity.ToTable("ManHinh");

                entity.Property(e => e.MaMH)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.KichThuoc).HasMaxLength(20);
            });


            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaTB);

                entity.ToTable("ThongBao");

                entity.Property(e => e.MaTB)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TrangThaiTB)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NoiDung).HasMaxLength(500);
                entity.Property(e => e.MaHD)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.ThoiGian)
                   .HasColumnType("datetime")
                   .HasColumnName("ThoiGian");

                entity.HasOne(d => d.HoaDon)
                   .WithMany(p => p.ThongBaos)
                   .HasForeignKey(d => d.MaHD)
                   .HasConstraintName("FK_ThongBao_HoaDon");

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
