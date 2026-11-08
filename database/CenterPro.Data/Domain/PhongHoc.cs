using System.ComponentModel.DataAnnotations;

namespace CenterPro.Data.Domain;

public sealed class PhongHoc
{
    public int MaPhongHoc { get; set; }

    [Required, StringLength(100)]
    public required string TenPhongHoc { get; set; }

    public ICollection<LichHoc> DanhSachLichHoc { get; set; } = new List<LichHoc>();
}
