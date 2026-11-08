using CenterPro.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace CenterPro.Data.Infrastructure;

public sealed class NguoiDungUngDung : IdentityUser
{
    public int? MaGiangVien { get; set; }
    public GiangVien? GiangVien { get; set; }
}
