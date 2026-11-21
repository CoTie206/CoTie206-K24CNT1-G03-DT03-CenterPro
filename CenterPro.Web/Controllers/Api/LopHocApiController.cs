using CenterPro.Data.Infrastructure;
using CenterPro.Web.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Controllers.Api;

[ApiController]
[Route("api/lophoc")]
[Authorize(Roles = "TrainingStaff,Administrator")]
public sealed class LopHocApiController(CenterProDbContext db) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LopHocApiDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LopHocApiDto>>> GetDanhSach(CancellationToken cancellationToken)
    {
        var danhSach = await db.LopHoc
            .AsNoTracking()
            .OrderBy(x => x.TenLopHoc)
            .Select(x => new LopHocApiDto(
                x.MaLopHoc,
                x.TenLopHoc,
                x.MaGiangVien,
                x.GiangVien.HoTen))
            .ToListAsync(cancellationToken);

        return Ok(danhSach);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LopHocApiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LopHocApiDto>> GetTheoMa(int id, CancellationToken cancellationToken)
    {
        var lopHoc = await db.LopHoc
            .AsNoTracking()
            .Where(x => x.MaLopHoc == id)
            .Select(x => new LopHocApiDto(
                x.MaLopHoc,
                x.TenLopHoc,
                x.MaGiangVien,
                x.GiangVien.HoTen))
            .FirstOrDefaultAsync(cancellationToken);

        return lopHoc is null ? NotFound() : Ok(lopHoc);
    }
}
