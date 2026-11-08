using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterPro.Data.Migrations;

/// <summary>Đổi tên bảng và cột nghiệp vụ sang tiếng Việt, giữ nguyên dữ liệu hiện có.</summary>
public partial class DoiTenCSDLThanhTiengViet : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(name: "CK_Proposal_Time", table: "ScheduleProposals");
        migrationBuilder.DropCheckConstraint(name: "CK_ClassSchedule_Time", table: "ClassSchedules");
        migrationBuilder.DropForeignKey(name: "FK_ClassSchedules_ScheduleProposals_ProposalId", table: "ClassSchedules");
        migrationBuilder.DropIndex(name: "IX_ClassSchedules_ProposalId", table: "ClassSchedules");
        migrationBuilder.RenameTable(name: "Instructors", newName: "GiangVien");
        migrationBuilder.RenameTable(name: "Rooms", newName: "PhongHoc");
        migrationBuilder.RenameTable(name: "TrainingClasses", newName: "LopHoc");
        migrationBuilder.RenameTable(name: "ScheduleProposals", newName: "DeXuatLichDay");
        migrationBuilder.RenameTable(name: "ClassSchedules", newName: "LichHoc");
        migrationBuilder.RenameColumn(name: "InstructorId", table: "GiangVien", newName: "MaGiangVien");
        migrationBuilder.RenameColumn(name: "FullName", table: "GiangVien", newName: "HoTen");
        migrationBuilder.RenameColumn(name: "RoomId", table: "PhongHoc", newName: "MaPhongHoc");
        migrationBuilder.RenameColumn(name: "RoomName", table: "PhongHoc", newName: "TenPhongHoc");
        migrationBuilder.RenameColumn(name: "ClassId", table: "LopHoc", newName: "MaLopHoc");
        migrationBuilder.RenameColumn(name: "ClassName", table: "LopHoc", newName: "TenLopHoc");
        migrationBuilder.RenameColumn(name: "InstructorId", table: "LopHoc", newName: "MaGiangVien");
        migrationBuilder.RenameColumn(name: "ProposalId", table: "DeXuatLichDay", newName: "MaDeXuatLich");
        migrationBuilder.RenameColumn(name: "ClassId", table: "DeXuatLichDay", newName: "MaLopHoc");
        migrationBuilder.RenameColumn(name: "InstructorId", table: "DeXuatLichDay", newName: "MaGiangVien");
        migrationBuilder.RenameColumn(name: "ProposedStartAt", table: "DeXuatLichDay", newName: "ThoiGianBatDauDeXuat");
        migrationBuilder.RenameColumn(name: "ProposedEndAt", table: "DeXuatLichDay", newName: "ThoiGianKetThucDeXuat");
        migrationBuilder.RenameColumn(name: "Status", table: "DeXuatLichDay", newName: "TrangThai");
        migrationBuilder.RenameColumn(name: "SubmittedAt", table: "DeXuatLichDay", newName: "ThoiGianGui");
        migrationBuilder.RenameColumn(name: "ScheduleId", table: "LichHoc", newName: "MaLichHoc");
        migrationBuilder.RenameColumn(name: "ClassId", table: "LichHoc", newName: "MaLopHoc");
        migrationBuilder.RenameColumn(name: "RoomId", table: "LichHoc", newName: "MaPhongHoc");
        migrationBuilder.Sql("""
            IF COL_LENGTH(N'dbo.LichHoc', N'MaDeXuatLich') IS NULL
            BEGIN
                IF COL_LENGTH(N'dbo.LichHoc', N'ProposalId') IS NOT NULL
                    EXEC sys.sp_rename N'dbo.LichHoc.ProposalId', N'MaDeXuatLich', N'COLUMN';
                ELSE IF COL_LENGTH(N'dbo.LichHoc', N'ScheduleProposalId') IS NOT NULL
                    EXEC sys.sp_rename N'dbo.LichHoc.ScheduleProposalId', N'MaDeXuatLich', N'COLUMN';
                ELSE IF COL_LENGTH(N'dbo.LichHoc', N'DeXuatLichDayId') IS NOT NULL
                    EXEC sys.sp_rename N'dbo.LichHoc.DeXuatLichDayId', N'MaDeXuatLich', N'COLUMN';
                ELSE
                    THROW 51001, 'Khong tim thay cot lien ket de xuat trong dbo.LichHoc.', 1;
            END
            """);
        migrationBuilder.RenameColumn(name: "StartAt", table: "LichHoc", newName: "ThoiGianBatDau");
        migrationBuilder.RenameColumn(name: "EndAt", table: "LichHoc", newName: "ThoiGianKetThuc");
        migrationBuilder.RenameColumn(name: "InstructorId", table: "AspNetUsers", newName: "MaGiangVien");
        migrationBuilder.AddCheckConstraint(name: "CK_DeXuatLichDay_ThoiGian", table: "DeXuatLichDay", sql: "[ThoiGianKetThucDeXuat] > [ThoiGianBatDauDeXuat]");
        migrationBuilder.AddCheckConstraint(name: "CK_LichHoc_ThoiGian", table: "LichHoc", sql: "[ThoiGianKetThuc] > [ThoiGianBatDau]");
        RenameIndexes(migrationBuilder, reverse: false);
        RenameConstraints(migrationBuilder, reverse: false);
        migrationBuilder.AddForeignKey(name: "FK_LichHoc_DeXuatLichDay_MaDeXuatLich", table: "LichHoc", column: "MaDeXuatLich", principalTable: "DeXuatLichDay", principalColumn: "MaDeXuatLich", onDelete: ReferentialAction.Restrict);
        migrationBuilder.CreateIndex(name: "IX_LichHoc_MaDeXuatLich", table: "LichHoc", column: "MaDeXuatLich", unique: true, filter: "[MaDeXuatLich] IS NOT NULL");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(name: "CK_DeXuatLichDay_ThoiGian", table: "DeXuatLichDay");
        migrationBuilder.DropCheckConstraint(name: "CK_LichHoc_ThoiGian", table: "LichHoc");
        migrationBuilder.DropForeignKey(name: "FK_LichHoc_DeXuatLichDay_MaDeXuatLich", table: "LichHoc");
        migrationBuilder.DropIndex(name: "IX_LichHoc_MaDeXuatLich", table: "LichHoc");
        RenameConstraints(migrationBuilder, reverse: true);
        RenameIndexes(migrationBuilder, reverse: true);
        migrationBuilder.RenameColumn(name: "MaGiangVien", table: "AspNetUsers", newName: "InstructorId");
        migrationBuilder.RenameColumn(name: "MaLichHoc", table: "LichHoc", newName: "ScheduleId");
        migrationBuilder.RenameColumn(name: "MaLopHoc", table: "LichHoc", newName: "ClassId");
        migrationBuilder.RenameColumn(name: "MaPhongHoc", table: "LichHoc", newName: "RoomId");
        migrationBuilder.Sql("""
            IF COL_LENGTH(N'dbo.LichHoc', N'ProposalId') IS NULL
               AND COL_LENGTH(N'dbo.LichHoc', N'MaDeXuatLich') IS NOT NULL
                EXEC sys.sp_rename N'dbo.LichHoc.MaDeXuatLich', N'ProposalId', N'COLUMN';
            """);
        migrationBuilder.RenameColumn(name: "ThoiGianBatDau", table: "LichHoc", newName: "StartAt");
        migrationBuilder.RenameColumn(name: "ThoiGianKetThuc", table: "LichHoc", newName: "EndAt");
        migrationBuilder.RenameColumn(name: "MaDeXuatLich", table: "DeXuatLichDay", newName: "ProposalId");
        migrationBuilder.RenameColumn(name: "MaLopHoc", table: "DeXuatLichDay", newName: "ClassId");
        migrationBuilder.RenameColumn(name: "MaGiangVien", table: "DeXuatLichDay", newName: "InstructorId");
        migrationBuilder.RenameColumn(name: "ThoiGianBatDauDeXuat", table: "DeXuatLichDay", newName: "ProposedStartAt");
        migrationBuilder.RenameColumn(name: "ThoiGianKetThucDeXuat", table: "DeXuatLichDay", newName: "ProposedEndAt");
        migrationBuilder.RenameColumn(name: "TrangThai", table: "DeXuatLichDay", newName: "Status");
        migrationBuilder.RenameColumn(name: "ThoiGianGui", table: "DeXuatLichDay", newName: "SubmittedAt");
        migrationBuilder.RenameColumn(name: "MaLopHoc", table: "LopHoc", newName: "ClassId");
        migrationBuilder.RenameColumn(name: "TenLopHoc", table: "LopHoc", newName: "ClassName");
        migrationBuilder.RenameColumn(name: "MaGiangVien", table: "LopHoc", newName: "InstructorId");
        migrationBuilder.RenameColumn(name: "MaPhongHoc", table: "PhongHoc", newName: "RoomId");
        migrationBuilder.RenameColumn(name: "TenPhongHoc", table: "PhongHoc", newName: "RoomName");
        migrationBuilder.RenameColumn(name: "MaGiangVien", table: "GiangVien", newName: "InstructorId");
        migrationBuilder.RenameColumn(name: "HoTen", table: "GiangVien", newName: "FullName");
        migrationBuilder.RenameTable(name: "LichHoc", newName: "ClassSchedules");
        migrationBuilder.RenameTable(name: "DeXuatLichDay", newName: "ScheduleProposals");
        migrationBuilder.RenameTable(name: "LopHoc", newName: "TrainingClasses");
        migrationBuilder.RenameTable(name: "PhongHoc", newName: "Rooms");
        migrationBuilder.RenameTable(name: "GiangVien", newName: "Instructors");
        migrationBuilder.AddCheckConstraint(name: "CK_ClassSchedule_Time", table: "ClassSchedules", sql: "[EndAt] > [StartAt]");
        migrationBuilder.AddCheckConstraint(name: "CK_Proposal_Time", table: "ScheduleProposals", sql: "[ProposedEndAt] > [ProposedStartAt]");
        migrationBuilder.AddForeignKey(name: "FK_ClassSchedules_ScheduleProposals_ProposalId", table: "ClassSchedules", column: "ProposalId", principalTable: "ScheduleProposals", principalColumn: "ProposalId", onDelete: ReferentialAction.Restrict);
        migrationBuilder.CreateIndex(name: "IX_ClassSchedules_ProposalId", table: "ClassSchedules", column: "ProposalId", unique: true, filter: "[ProposalId] IS NOT NULL");
    }

    private static void RenameIndexes(MigrationBuilder migrationBuilder, bool reverse)
    {
        var pairs = new (string Table, string OldName, string NewName)[]
        {
            ("LopHoc", "IX_TrainingClasses_InstructorId", "IX_LopHoc_MaGiangVien"),
            ("DeXuatLichDay", "IX_ScheduleProposals_ClassId", "IX_DeXuatLichDay_MaLopHoc"),
            ("DeXuatLichDay", "IX_ScheduleProposals_InstructorId_Status", "IX_DeXuatLichDay_MaGiangVien_TrangThai"),
            ("LichHoc", "IX_ClassSchedules_ClassId", "IX_LichHoc_MaLopHoc"),
            ("LichHoc", "IX_ClassSchedules_RoomId_StartAt_EndAt", "IX_LichHoc_MaPhongHoc_ThoiGianBatDau_ThoiGianKetThuc"),
            ("AspNetUsers", "IX_AspNetUsers_InstructorId", "IX_AspNetUsers_MaGiangVien")
        };
        foreach (var pair in pairs) migrationBuilder.RenameIndex(name: reverse ? pair.NewName : pair.OldName, table: pair.Table, newName: reverse ? pair.OldName : pair.NewName);
    }

    private static void RenameConstraints(MigrationBuilder migrationBuilder, bool reverse)
    {
        var pairs = new (string OldName, string NewName)[]
        {
            ("PK_Instructors", "PK_GiangVien"), ("PK_Rooms", "PK_PhongHoc"), ("PK_TrainingClasses", "PK_LopHoc"), ("PK_ScheduleProposals", "PK_DeXuatLichDay"), ("PK_ClassSchedules", "PK_LichHoc"),
            ("FK_AspNetUsers_Instructors_InstructorId", "FK_AspNetUsers_GiangVien_MaGiangVien"), ("FK_TrainingClasses_Instructors_InstructorId", "FK_LopHoc_GiangVien_MaGiangVien"), ("FK_ScheduleProposals_Instructors_InstructorId", "FK_DeXuatLichDay_GiangVien_MaGiangVien"), ("FK_ScheduleProposals_TrainingClasses_ClassId", "FK_DeXuatLichDay_LopHoc_MaLopHoc"), ("FK_ClassSchedules_Rooms_RoomId", "FK_LichHoc_PhongHoc_MaPhongHoc"), ("FK_ClassSchedules_TrainingClasses_ClassId", "FK_LichHoc_LopHoc_MaLopHoc")
        };
        foreach (var pair in pairs) { var from = reverse ? pair.NewName : pair.OldName; var to = reverse ? pair.OldName : pair.NewName; migrationBuilder.Sql($"EXEC sp_rename N'dbo.{from}', N'{to}', N'OBJECT';"); }
    }
}
