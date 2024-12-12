using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThanhThoaiRestaurant.Migrations
{
    public partial class TenCuaMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHang_MonAn",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "FK_MaKH_MaMon",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "fk_MaNhom",
                table: "MonAn");

            migrationBuilder.DropForeignKey(
                name: "FK_MonAn_GioHang_GioHangMaMon_GioHangMaKH",
                table: "MonAn");

            migrationBuilder.DropIndex(
                name: "IX_MonAn_GioHangMaMon_GioHangMaKH",
                table: "MonAn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.DropIndex(
                name: "IX_GioHang_MaKH",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "GioHangMaKH",
                table: "MonAn");

            migrationBuilder.DropColumn(
                name: "GioHangMaMon",
                table: "MonAn");

            migrationBuilder.DropColumn(
                name: "MaMon",
                table: "GioHang");

            migrationBuilder.RenameColumn(
                name: "TenMonAnHD",
                table: "HoaDon",
                newName: "HinhThucTT");

            migrationBuilder.RenameColumn(
                name: "ThanhTien",
                table: "GioHang",
                newName: "TongTien");

            migrationBuilder.RenameColumn(
                name: "SoLuongMM",
                table: "GioHang",
                newName: "SoLuongMon");

            migrationBuilder.RenameColumn(
                name: "MaKH",
                table: "GioHang",
                newName: "MaGioHang");

            migrationBuilder.AlterColumn<int>(
                name: "PhanTram",
                table: "PhieuGiamGia",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "VaiTro",
                table: "NguoiDung",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "MatKhau",
                table: "NguoiDung",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "EmailND",
                table: "NguoiDung",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "MaNhom",
                table: "MonAn",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNhom",
                table: "MonAn",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiMA",
                table: "MonAn",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "EmailKH",
                table: "KhachHang",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "TongTien",
                table: "HoaDon",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<float>(
                name: "TienTT",
                table: "HoaDon",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<float>(
                name: "TienGiam",
                table: "HoaDon",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "MaHD",
                table: "HoaDon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MaGioHang",
                table: "GioHang",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "KhachHangMaKH",
                table: "GioHang",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TienThanhToan",
                table: "GioHang",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TongGiamGia",
                table: "GioHang",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<int>(
                name: "MaDonHang",
                table: "DonHang",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<float>(
                name: "ThanhTien",
                table: "ChiTietHD",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenMon",
                table: "ChiTietHD",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaDonHang",
                table: "ChiTietDH",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                column: "MaGioHang");

            migrationBuilder.CreateTable(
                name: "ChiTietGH",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    MaGioHang = table.Column<int>(type: "int", fixedLength: true, maxLength: 10, nullable: false),
                    SoLuongMM = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GiaBan = table.Column<float>(type: "real", nullable: false),
                    ThanhTien = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTGH", x => new { x.MaMon, x.MaGioHang });
                    table.ForeignKey(
                        name: "fk_GH1_CTGH",
                        column: x => x.MaGioHang,
                        principalTable: "GioHang",
                        principalColumn: "MaGioHang");
                    table.ForeignKey(
                        name: "fk_MA_CTGH",
                        column: x => x.MaMon,
                        principalTable: "MonAn",
                        principalColumn: "MaMon");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_KhachHangMaKH",
                table: "GioHang",
                column: "KhachHangMaKH");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGH_MaGioHang",
                table: "ChiTietGH",
                column: "MaGioHang");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHang_KhachHang_KhachHangMaKH",
                table: "GioHang",
                column: "KhachHangMaKH",
                principalTable: "KhachHang",
                principalColumn: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "fk_MaNhom",
                table: "MonAn",
                column: "MaNhom",
                principalTable: "NhomMonAn",
                principalColumn: "MaNhom",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHang_KhachHang_KhachHangMaKH",
                table: "GioHang");

            migrationBuilder.DropForeignKey(
                name: "fk_MaNhom",
                table: "MonAn");

            migrationBuilder.DropTable(
                name: "ChiTietGH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang");

            migrationBuilder.DropIndex(
                name: "IX_GioHang_KhachHangMaKH",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "TenNhom",
                table: "MonAn");

            migrationBuilder.DropColumn(
                name: "TrangThaiMA",
                table: "MonAn");

            migrationBuilder.DropColumn(
                name: "KhachHangMaKH",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "TienThanhToan",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "TongGiamGia",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "TenMon",
                table: "ChiTietHD");

            migrationBuilder.RenameColumn(
                name: "HinhThucTT",
                table: "HoaDon",
                newName: "TenMonAnHD");

            migrationBuilder.RenameColumn(
                name: "TongTien",
                table: "GioHang",
                newName: "ThanhTien");

            migrationBuilder.RenameColumn(
                name: "SoLuongMon",
                table: "GioHang",
                newName: "SoLuongMM");

            migrationBuilder.RenameColumn(
                name: "MaGioHang",
                table: "GioHang",
                newName: "MaKH");

            migrationBuilder.AlterColumn<int>(
                name: "PhanTram",
                table: "PhieuGiamGia",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "VaiTro",
                table: "NguoiDung",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MatKhau",
                table: "NguoiDung",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailND",
                table: "NguoiDung",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaNhom",
                table: "MonAn",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "GioHangMaKH",
                table: "MonAn",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GioHangMaMon",
                table: "MonAn",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailKH",
                table: "KhachHang",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TongTien",
                table: "HoaDon",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "TienTT",
                table: "HoaDon",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "TienGiam",
                table: "HoaDon",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "MaHD",
                table: "HoaDon",
                type: "int",
                fixedLength: true,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MaKH",
                table: "GioHang",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MaMon",
                table: "GioHang",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MaDonHang",
                table: "DonHang",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "ThanhTien",
                table: "ChiTietHD",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "MaDonHang",
                table: "ChiTietDH",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GioHang",
                table: "GioHang",
                columns: new[] { "MaMon", "MaKH" });

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_GioHangMaMon_GioHangMaKH",
                table: "MonAn",
                columns: new[] { "GioHangMaMon", "GioHangMaKH" });

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_MaKH",
                table: "GioHang",
                column: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHang_MonAn",
                table: "GioHang",
                column: "MaMon",
                principalTable: "MonAn",
                principalColumn: "MaMon");

            migrationBuilder.AddForeignKey(
                name: "FK_MaKH_MaMon",
                table: "GioHang",
                column: "MaKH",
                principalTable: "KhachHang",
                principalColumn: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "fk_MaNhom",
                table: "MonAn",
                column: "MaNhom",
                principalTable: "NhomMonAn",
                principalColumn: "MaNhom");

            migrationBuilder.AddForeignKey(
                name: "FK_MonAn_GioHang_GioHangMaMon_GioHangMaKH",
                table: "MonAn",
                columns: new[] { "GioHangMaMon", "GioHangMaKH" },
                principalTable: "GioHang",
                principalColumns: new[] { "MaMon", "MaKH" });
        }
    }
}
