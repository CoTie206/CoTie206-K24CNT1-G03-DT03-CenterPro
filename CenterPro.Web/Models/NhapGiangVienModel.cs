using System.ComponentModel.DataAnnotations;

namespace CenterPro.Web.Models;

public sealed class NhapGiangVienModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên giảng viên.")]
    [StringLength(150, ErrorMessage = "Họ tên không vượt quá 150 ký tự.")]
    [Display(Name = "Họ tên")]
    public string HoTen { get; set; } = "";
}
