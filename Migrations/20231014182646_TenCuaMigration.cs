using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThanhThoaiRestaurant.Migrations
{
    public partial class TenCuaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanAn",
                columns: table => new
                {
                    MaBan = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    TrangThaiBA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanAn", x => x.MaBan);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    TenDangNhap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailND = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung_1", x => x.TenDangNhap);
                });

            migrationBuilder.CreateTable(
                name: "NhomMonAn",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomMonAn", x => x.MaNhom);
                });

            migrationBuilder.CreateTable(
                name: "PhieuGiamGia",
                columns: table => new
                {
                    MaPhieuGG = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhanTram = table.Column<double>(type: "float", nullable: false),
                    LoaiMa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SoLuongPhieu = table.Column<int>(type: "int", nullable: false),
                    Diem = table.Column<int>(type: "int", nullable: false),
                    TrangThaiPGG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGiamGia", x => x.MaPhieuGG);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayTG = table.Column<DateTime>(type: "datetime", nullable: false),
                    DoanhSo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NgaySinhKH = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinhKH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmailKH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SDTKH = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    DiaChiKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiemTichLuy = table.Column<int>(type: "int", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                    table.ForeignKey(
                        name: "fk_TenDN1",
                        column: x => x.TenDangNhap,
                        principalTable: "NguoiDung",
                        principalColumn: "TenDangNhap");
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    TenNV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayVL = table.Column<DateTime>(type: "date", nullable: false),
                    SDTNV = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: true),
                    EmailNV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GioiTinhNV = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DiaChiNV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NgaySinhNV = table.Column<DateTime>(type: "date", nullable: false),
                    CCCDNV = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    TenDangNhap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                    table.ForeignKey(
                        name: "fk_TenDN",
                        column: x => x.TenDangNhap,
                        principalTable: "NguoiDung",
                        principalColumn: "TenDangNhap");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHD = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    NgayHD = table.Column<DateTime>(type: "datetime", nullable: false),
                    TenMonAnHD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TienGiam = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TienTT = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaPhieuGG = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHD);
                    table.ForeignKey(
                        name: "fk_HoaDon_PGG",
                        column: x => x.MaPhieuGG,
                        principalTable: "PhieuGiamGia",
                        principalColumn: "MaPhieuGG");
                });

            migrationBuilder.CreateTable(
                name: "DatBan",
                columns: table => new
                {
                    MaLichHen = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    TenKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoKhach = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayHen = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiLH = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaBan = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_DatBan_BanAn",
                        column: x => x.MaBan,
                        principalTable: "BanAn",
                        principalColumn: "MaBan");
                    table.ForeignKey(
                        name: "fk_DatBan_Khachhang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDonHang = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    NgayDatHang = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThaiDH = table.Column<int>(type: "int", nullable: false),
                    PhiVanChuyen = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaHD = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "fk_DonHang_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                    table.ForeignKey(
                        name: "fk_DonHang_Khachhang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "PhieuGoiMon",
                columns: table => new
                {
                    MaPhieuGM = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    TenMonAnPGM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayGM = table.Column<DateTime>(type: "datetime", nullable: false),
                    MaBan = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    MaHD = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGoiMon", x => x.MaPhieuGM);
                    table.ForeignKey(
                        name: "fk_PhieuGoiMon_BanAn",
                        column: x => x.MaBan,
                        principalTable: "BanAn",
                        principalColumn: "MaBan");
                    table.ForeignKey(
                        name: "fk_PhieuGoiMon_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDH",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaDonHang = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    TenMonAnDH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoLuongMMDH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DH", x => new { x.MaMon, x.MaDonHang });
                    table.ForeignKey(
                        name: "fk_DH_CTDH",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGM",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaPhieuGM = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    SoLuongCT1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGM", x => new { x.MaMon, x.MaPhieuGM });
                    table.ForeignKey(
                        name: "fk_PGM_CTGM",
                        column: x => x.MaPhieuGM,
                        principalTable: "PhieuGoiMon",
                        principalColumn: "MaPhieuGM");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHD",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaHD = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    SoLuongCT = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTHD", x => new { x.MaHD, x.MaMon });
                    table.ForeignKey(
                        name: "fk_HD_CTHD",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaKH = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    SoLuongMM = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => new { x.MaMon, x.MaKH });
                    table.ForeignKey(
                        name: "FK_MaKH_MaMon",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "MonAn",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MoTaNgan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoTaDai = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HinhAnh = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    MaNhom = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: true),
                    GioHangMaKH = table.Column<int>(type: "int", nullable: true),
                    GioHangMaMon = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAn", x => x.MaMon);
                    table.ForeignKey(
                        name: "fk_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomMonAn",
                        principalColumn: "MaNhom");
                    table.ForeignKey(
                        name: "FK_MonAn_GioHang_GioHangMaMon_GioHangMaKH",
                        columns: x => new { x.GioHangMaMon, x.GioHangMaKH },
                        principalTable: "GioHang",
                        principalColumns: new[] { "MaMon", "MaKH" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDH_MaDonHang",
                table: "ChiTietDH",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGM_MaPhieuGM",
                table: "ChiTietGM",
                column: "MaPhieuGM");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHD_MaMon",
                table: "ChiTietHD",
                column: "MaMon");

            migrationBuilder.CreateIndex(
                name: "IX_DatBan_MaBan",
                table: "DatBan",
                column: "MaBan");

            migrationBuilder.CreateIndex(
                name: "IX_DatBan_MaKH",
                table: "DatBan",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaHD",
                table: "DonHang",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKH",
                table: "DonHang",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_MaKH",
                table: "GioHang",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaPhieuGG",
                table: "HoaDon",
                column: "MaPhieuGG");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_TenDangNhap",
                table: "KhachHang",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_GioHangMaMon_GioHangMaKH",
                table: "MonAn",
                columns: new[] { "GioHangMaMon", "GioHangMaKH" });

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_MaNhom",
                table: "MonAn",
                column: "MaNhom");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TenDangNhap",
                table: "NhanVien",
                column: "TenDangNhap");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGoiMon_MaBan",
                table: "PhieuGoiMon",
                column: "MaBan");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGoiMon_MaHD",
                table: "PhieuGoiMon",
                column: "MaHD");

            migrationBuilder.AddForeignKey(
                name: "fk_MonAn_CTDH",
                table: "ChiTietDH",
                column: "MaMon",
                principalTable: "MonAn",
                principalColumn: "MaMon");

            migrationBuilder.AddForeignKey(
                name: "fk_MonAn_CTGM",
                table: "ChiTietGM",
                column: "MaMon",
                principalTable: "MonAn",
                principalColumn: "MaMon");

            migrationBuilder.AddForeignKey(
                name: "fk_MonAn_CTHD",
                table: "ChiTietHD",
                column: "MaMon",
                principalTable: "MonAn",
                principalColumn: "MaMon");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHang_MonAn",
                table: "GioHang",
                column: "MaMon",
                principalTable: "MonAn",
                principalColumn: "MaMon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHang_MonAn",
                table: "GioHang");

            migrationBuilder.DropTable(
                name: "ChiTietDH");

            migrationBuilder.DropTable(
                name: "ChiTietGM");

            migrationBuilder.DropTable(
                name: "ChiTietHD");

            migrationBuilder.DropTable(
                name: "DatBan");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "PhieuGoiMon");

            migrationBuilder.DropTable(
                name: "BanAn");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "PhieuGiamGia");

            migrationBuilder.DropTable(
                name: "MonAn");

            migrationBuilder.DropTable(
                name: "NhomMonAn");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
