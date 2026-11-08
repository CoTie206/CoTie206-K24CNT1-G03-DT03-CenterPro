using System.ComponentModel.DataAnnotations;

namespace CenterPro.Data.Domain;

public sealed class GiangVien
{
    public int MaGiangVien { get; set; }

    [Required, StringLength(150)]
    public required string HoTen { get; set; }

    public ICollection<LopHoc> DanhSachLopHoc { get; set; } = new List<LopHoc>();
}
