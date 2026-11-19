namespace CenterPro.Web.Models.Api;

public sealed record LopHocApiDto(
    int MaLopHoc,
    string TenLopHoc,
    int MaGiangVien,
    string TenGiangVien);
