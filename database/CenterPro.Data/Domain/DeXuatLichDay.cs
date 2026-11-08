namespace CenterPro.Data.Domain;

public sealed class DeXuatLichDay
{
    public int MaDeXuatLich { get; set; }
    public int MaLopHoc { get; set; }
    public LopHoc LopHoc { get; set; } = null!;
    public int MaGiangVien { get; set; }
    public GiangVien GiangVien { get; set; } = null!;
    public DateTime ThoiGianBatDauDeXuat { get; set; }
    public DateTime ThoiGianKetThucDeXuat { get; set; }
    public string TrangThai { get; set; } = "Pending";
    public DateTime ThoiGianGui { get; set; }
}
