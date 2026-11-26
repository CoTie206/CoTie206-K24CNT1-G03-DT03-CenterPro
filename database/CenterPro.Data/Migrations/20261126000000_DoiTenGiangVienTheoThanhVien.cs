using CenterPro.Data.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CenterPro.Data.Migrations;

[DbContext(typeof(CenterProDbContext))]
[Migration("20261126000000_DoiTenGiangVienTheoThanhVien")]
public sealed class DoiTenGiangVienTheoThanhVien : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "GiangVien", keyColumn: "MaGiangVien", keyValue: 1,
            column: "HoTen", value: "Hoàng Công Tiến");
        migrationBuilder.UpdateData(
            table: "GiangVien", keyColumn: "MaGiangVien", keyValue: 2,
            column: "HoTen", value: "Vũ Hoàng Anh");
        migrationBuilder.UpdateData(
            table: "GiangVien", keyColumn: "MaGiangVien", keyValue: 3,
            column: "HoTen", value: "Nghiêm Văn Trọng");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "GiangVien", keyColumn: "MaGiangVien", keyValue: 1,
            column: "HoTen", value: "Nguyễn Minh An");
        migrationBuilder.UpdateData(
            table: "GiangVien", keyColumn: "MaGiangVien", keyValue: 2,
            column: "HoTen", value: "Trần Thu Hà");
        migrationBuilder.UpdateData(
            table: "GiangVien", keyColumn: "MaGiangVien", keyValue: 3,
            column: "HoTen", value: "Lê Quốc Bình");
    }
}
