using System.ComponentModel.DataAnnotations;

namespace CenterPro.Web.Models;

public sealed class NhapPhongHocModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên phòng.")]
    [StringLength(100, ErrorMessage = "Tên phòng không vượt quá 100 ký tự.")]
    [Display(Name = "Tên phòng")]
    public string TenPhongHoc { get; set; } = "";
}
