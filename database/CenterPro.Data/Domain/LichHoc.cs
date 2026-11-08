namespace CenterPro.Data.Domain;

public sealed class LichHoc
{
    public int MaLichHoc { get; set; }
    public int MaLopHoc { get; set; }
    public LopHoc LopHoc { get; set; } = null!;
    public int MaPhongHoc { get; set; }
    public PhongHoc PhongHoc { get; set; } = null!;
    public int? MaDeXuatLich { get; set; }
    public DeXuatLichDay? DeXuatLichDay { get; set; }
    public DateTime ThoiGianBatDau { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
}
