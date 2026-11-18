# DATABASE – CENTERPRO

## 1. Giới thiệu

Thư mục này chứa các thành phần liên quan đến cơ sở dữ liệu của hệ thống CenterPro – Hệ thống quản lý trung tâm đào tạo CNTT.

Cơ sở dữ liệu được xây dựng trên Microsoft SQL Server và sử dụng Entity Framework Core để kết nối, truy vấn và quản lý dữ liệu.

## 2. Công nghệ sử dụng

- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Entity Framework Core
- ASP.NET Core Identity

## 3. Cấu trúc thư mục

```text
database/
│
├── CenterPro.Data/
│   ├── Domain/
│   │   ├── GiangVien.cs
│   │   ├── LopHoc.cs
│   │   ├── PhongHoc.cs
│   │   ├── LichHoc.cs
│   │   └── DeXuatLichDay.cs
│   │
│   ├── Infrastructure/
│   │   ├── CenterProDbContext.cs
│   │   ├── CenterProDbContextFactory.cs
│   │   └── NguoiDungUngDung.cs
│   │
│   └── Migrations/
│
├── CSDL_CenterPro.sql
└── README.md
```

## 4. Cơ sở dữ liệu

**Tên database:** `CenterProDb`

Cơ sở dữ liệu lưu trữ các thông tin chính:

- Giảng viên.
- Phòng học.
- Lớp học.
- Lịch học.
- Đề xuất lịch giảng dạy.
- Tài khoản người dùng.
- Thông tin phân quyền.

## 5. Hướng dẫn tạo cơ sở dữ liệu

**Bước 1:** Mở SQL Server Management Studio (SSMS).

**Bước 2:** Kết nối đến SQL Server trên máy tính.

**Bước 3:** Mở file:

```text
CSDL_CenterPro.sql
```

**Bước 4:** Nhấn Execute hoặc phím F5 để thực thi script.

**Bước 5:** Kiểm tra database `CenterProDb` đã được tạo thành công trong Object Explorer.

## 6. Kết nối cơ sở dữ liệu

Sau khi tạo database, cần kiểm tra chuỗi kết nối trong file `appsettings.json` của project `CenterPro.Web`.

Ví dụ:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CenterProDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Lưu ý: Thay `Server=.` bằng tên SQL Server Instance đang sử dụng trên máy tính.

## 7. Entity Framework Core

Dự án sử dụng Entity Framework Core để:

- Ánh xạ các bảng trong SQL Server thành các Entity.
- Truy vấn và cập nhật dữ liệu.
- Quản lý quan hệ giữa các bảng.
- Quản lý phiên bản cơ sở dữ liệu thông qua Migrations.

Các file Migration được lưu trong thư mục:

```text
CenterPro.Data/Migrations/
```

## 8. Lưu ý

- Cần cài đặt SQL Server trước khi khởi tạo database.
- Kiểm tra đúng tên database và chuỗi kết nối.
- Không thực thi lại script tạo database trên cơ sở dữ liệu đang có dữ liệu quan trọng nếu chưa sao lưu.
- Khi thay đổi cấu trúc Entity, cần kiểm tra và cập nhật Migration tương ứng.

## 9. Mục đích

Thư mục database phục vụ việc xây dựng, lưu trữ và quản lý dữ liệu cho hệ thống CenterPro.