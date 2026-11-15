using System.ComponentModel.DataAnnotations;

namespace CenterPro.Web.Models;

public sealed class NhapDeXuatLichModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn lớp học.")]
    [Display(Name = "Lớp học")]
    public int MaLopHoc { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn thời gian bắt đầu.")]
    [Display(Name = "Thời gian bắt đầu")]
    public DateTime? ThoiGianBatDauDeXuat { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn thời gian kết thúc.")]
    [Display(Name = "Thời gian kết thúc")]
    public DateTime? ThoiGianKetThucDeXuat { get; set; }
}