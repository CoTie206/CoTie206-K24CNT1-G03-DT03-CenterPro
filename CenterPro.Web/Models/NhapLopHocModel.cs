using System.ComponentModel.DataAnnotations;

namespace CenterPro.Web.Models;

public sealed class NhapLopHocModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên lớp.")]
    [StringLength(150, ErrorMessage = "Tên lớp không vượt quá 150 ký tự.")]
    [Display(Name = "Tên lớp")]
    public string TenLopHoc { get; set; } = "";

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn giảng viên.")]
    [Display(Name = "Giảng viên phụ trách")]
    public int MaGiangVien { get; set; }
}
