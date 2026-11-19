namespace CenterPro.Web.Models.Api;

public sealed record LichHocApiDto(
    int MaLichHoc,
    int MaLopHoc,
    string TenLopHoc,
    int MaGiangVien,
    string TenGiangVien,
    int MaPhongHoc,
    string TenPhongHoc,
    DateTime ThoiGianBatDau,
    DateTime ThoiGianKetThuc);
