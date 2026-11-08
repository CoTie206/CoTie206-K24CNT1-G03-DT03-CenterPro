using CenterPro.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Data.Infrastructure;

public sealed class CenterProDbContext(DbContextOptions<CenterProDbContext> options)
    : IdentityDbContext<NguoiDungUngDung>(options)
{
    public DbSet<GiangVien> GiangVien => Set<GiangVien>();
    public DbSet<PhongHoc> PhongHoc => Set<PhongHoc>();
    public DbSet<LopHoc> LopHoc => Set<LopHoc>();
    public DbSet<DeXuatLichDay> DeXuatLichDay => Set<DeXuatLichDay>();
    public DbSet<LichHoc> LichHoc => Set<LichHoc>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.ToTable("GiangVien");
            entity.HasKey(x => x.MaGiangVien);
            entity.Property(x => x.HoTen).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<PhongHoc>(entity =>
        {
            entity.ToTable("PhongHoc");
            entity.HasKey(x => x.MaPhongHoc);
            entity.Property(x => x.TenPhongHoc).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.ToTable("LopHoc");
            entity.HasKey(x => x.MaLopHoc);
            entity.Property(x => x.TenLopHoc).HasMaxLength(150).IsRequired();
            entity.HasOne(x => x.GiangVien).WithMany(x => x.DanhSachLopHoc)
                .HasForeignKey(x => x.MaGiangVien).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => x.MaGiangVien);
        });

        modelBuilder.Entity<DeXuatLichDay>(entity =>
        {
            entity.ToTable("DeXuatLichDay", table =>
                table.HasCheckConstraint("CK_DeXuatLichDay_ThoiGian", "[ThoiGianKetThucDeXuat] > [ThoiGianBatDauDeXuat]"));
            entity.HasKey(x => x.MaDeXuatLich);
            entity.Property(x => x.TrangThai).HasMaxLength(24).IsRequired();
            entity.Property(x => x.ThoiGianGui).HasColumnType("datetime2");
            entity.Property(x => x.ThoiGianBatDauDeXuat).HasColumnType("datetime2");
            entity.Property(x => x.ThoiGianKetThucDeXuat).HasColumnType("datetime2");
            entity.HasOne(x => x.LopHoc).WithMany(x => x.DanhSachDeXuat)
                .HasForeignKey(x => x.MaLopHoc).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.GiangVien).WithMany()
                .HasForeignKey(x => x.MaGiangVien).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.MaGiangVien, x.TrangThai });
        });

        modelBuilder.Entity<LichHoc>(entity =>
        {
            entity.ToTable("LichHoc", table =>
                table.HasCheckConstraint("CK_LichHoc_ThoiGian", "[ThoiGianKetThuc] > [ThoiGianBatDau]"));
            entity.HasKey(x => x.MaLichHoc);
            entity.Property(x => x.ThoiGianBatDau).HasColumnType("datetime2");
            entity.Property(x => x.ThoiGianKetThuc).HasColumnType("datetime2");
            entity.HasOne(x => x.LopHoc).WithMany(x => x.DanhSachLichHoc)
                .HasForeignKey(x => x.MaLopHoc).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.PhongHoc).WithMany(x => x.DanhSachLichHoc)
                .HasForeignKey(x => x.MaPhongHoc).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.DeXuatLichDay).WithOne()
                .HasForeignKey<LichHoc>(x => x.MaDeXuatLich).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => x.MaDeXuatLich).IsUnique().HasFilter("[MaDeXuatLich] IS NOT NULL");
            entity.HasIndex(x => new { x.MaPhongHoc, x.ThoiGianBatDau, x.ThoiGianKetThuc });
        });

        modelBuilder.Entity<NguoiDungUngDung>(entity =>
        {
            entity.HasOne(x => x.GiangVien).WithOne()
                .HasForeignKey<NguoiDungUngDung>(x => x.MaGiangVien).OnDelete(DeleteBehavior.Restrict);
        });

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GiangVien>().HasData(
            new GiangVien { MaGiangVien = 1, HoTen = "Nguyễn Minh An" },
            new GiangVien { MaGiangVien = 2, HoTen = "Trần Thu Hà" },
            new GiangVien { MaGiangVien = 3, HoTen = "Lê Quốc Bình" });

        modelBuilder.Entity<PhongHoc>().HasData(
            new PhongHoc { MaPhongHoc = 1, TenPhongHoc = "Phòng A" },
            new PhongHoc { MaPhongHoc = 2, TenPhongHoc = "Phòng B" });

        modelBuilder.Entity<LopHoc>().HasData(
            new LopHoc { MaLopHoc = 1, TenLopHoc = "Lập trình C# 01", MaGiangVien = 1 },
            new LopHoc { MaLopHoc = 2, TenLopHoc = "Web căn bản 01", MaGiangVien = 2 },
            new LopHoc { MaLopHoc = 3, TenLopHoc = "Cơ sở dữ liệu 01", MaGiangVien = 1 });

        var ngayGoc = new DateTime(2026, 11, 10, 0, 0, 0, DateTimeKind.Unspecified);
        modelBuilder.Entity<LichHoc>().HasData(new LichHoc
        {
            MaLichHoc = 1, MaLopHoc = 1, MaPhongHoc = 1, MaDeXuatLich = null,
            ThoiGianBatDau = ngayGoc.AddHours(8), ThoiGianKetThuc = ngayGoc.AddHours(10)
        });

        modelBuilder.Entity<DeXuatLichDay>().HasData(
            new DeXuatLichDay
            {
                MaDeXuatLich = 1, MaLopHoc = 3, MaGiangVien = 1,
                ThoiGianBatDauDeXuat = ngayGoc.AddHours(9), ThoiGianKetThucDeXuat = ngayGoc.AddHours(11),
                TrangThai = "Pending", ThoiGianGui = new DateTime(2026, 11, 8, 8, 0, 0, DateTimeKind.Unspecified)
            },
            new DeXuatLichDay
            {
                MaDeXuatLich = 2, MaLopHoc = 2, MaGiangVien = 2,
                ThoiGianBatDauDeXuat = ngayGoc.AddHours(8.5), ThoiGianKetThucDeXuat = ngayGoc.AddHours(9.5),
                TrangThai = "Pending", ThoiGianGui = new DateTime(2026, 11, 8, 8, 5, 0, DateTimeKind.Unspecified)
            });

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "role-lecturer", Name = "Lecturer", NormalizedName = "LECTURER", ConcurrencyStamp = "role-lecturer" },
            new IdentityRole { Id = "role-training-staff", Name = "TrainingStaff", NormalizedName = "TRAININGSTAFF", ConcurrencyStamp = "role-training-staff" },
            new IdentityRole { Id = "role-administrator", Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = "role-administrator" });
    }
}
