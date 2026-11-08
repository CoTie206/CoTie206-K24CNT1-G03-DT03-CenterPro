using System.ComponentModel.DataAnnotations;

namespace CenterPro.Data.Domain;

public sealed class LopHoc
{
    public int MaLopHoc { get; set; }

    [Required, StringLength(150)]
    public required string TenLopHoc { get; set; }

    public int MaGiangVien { get; set; }
    public GiangVien GiangVien { get; set; } = null!;
    public ICollection<DeXuatLichDay> DanhSachDeXuat { get; set; } = new List<DeXuatLichDay>();
    public ICollection<LichHoc> DanhSachLichHoc { get; set; } = new List<LichHoc>();
}
