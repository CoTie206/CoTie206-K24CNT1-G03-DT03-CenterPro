using CenterPro.Data.Infrastructure;
using CenterPro.Web.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers.Api;

[ApiController]
[Route("api/lichhoc")]
[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class LichHocApiController(CenterProDbContext db) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LichHocApiDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LichHocApiDto>>> GetDanhSach(CancellationToken cancellationToken)
    {
        var danhSach = await db.LichHoc
            .AsNoTracking()
            .OrderBy(x => x.ThoiGianBatDau)
            .Select(x => new LichHocApiDto(
                x.MaLichHoc,
                x.MaLopHoc,
                x.LopHoc.TenLopHoc,
                x.LopHoc.MaGiangVien,
                x.LopHoc.GiangVien.HoTen,
                x.MaPhongHoc,
                x.PhongHoc.TenPhongHoc,
                x.ThoiGianBatDau,
                x.ThoiGianKetThuc))
            .ToListAsync(cancellationToken);

        return Ok(danhSach);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LichHocApiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LichHocApiDto>> GetTheoMa(int id, CancellationToken cancellationToken)
    {
        var lichHoc = await db.LichHoc
            .AsNoTracking()
            .Where(x => x.MaLichHoc == id)
            .Select(x => new LichHocApiDto(
                x.MaLichHoc,
                x.MaLopHoc,
                x.LopHoc.TenLopHoc,
                x.LopHoc.MaGiangVien,
                x.LopHoc.GiangVien.HoTen,
                x.MaPhongHoc,
                x.PhongHoc.TenPhongHoc,
                x.ThoiGianBatDau,
                x.ThoiGianKetThuc))
            .FirstOrDefaultAsync(cancellationToken);

        return lichHoc is null ? NotFound() : Ok(lichHoc);
    }
}
