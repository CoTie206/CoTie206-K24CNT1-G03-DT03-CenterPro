# CENTERPRO – HỆ THỐNG QUẢN LÝ TRUNG TÂM ĐÀO TẠO CNTT

## 1. Giới thiệu

CenterPro là hệ thống quản lý trung tâm đào tạo Công nghệ thông tin, được xây dựng nhằm hỗ trợ quản lý giảng viên, phòng học, lớp học và lịch giảng dạy.

Hệ thống giúp đơn giản hóa quá trình quản lý và sắp xếp lịch học, đồng thời hỗ trợ kiểm tra xung đột lịch giảng viên và phòng học.

## 2. Công nghệ sử dụng

- **Backend:** ASP.NET Core MVC, C#
- **Frontend:** HTML, CSS, JavaScript, Bootstrap 5, jQuery
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Xác thực và phân quyền:** ASP.NET Core Identity

## 3. Chức năng chính

- Quản lý giảng viên.
- Quản lý phòng học.
- Quản lý lớp học.
- Giảng viên gửi đề xuất lịch giảng dạy.
- Nhân viên quản lý tiếp nhận và xử lý đề xuất.
- Kiểm tra xung đột lịch giảng viên.
- Kiểm tra xung đột phòng học.
- Quản lý tài khoản và phân quyền người dùng.

## 4. Các vai trò sử dụng

| Vai trò | Quyền sử dụng |
|---|---|
| Administrator | Quản lý hệ thống và tài khoản |
| Lecturer | Xem thông tin lớp học và gửi đề xuất lịch |
| TrainingStaff | Quản lý lớp học, giảng viên, phòng học và xử lý đề xuất lịch |

## 5. Tài khoản đăng nhập demo

| Vai trò | Email | Mật khẩu |
|---|---|---|
| Administrator | admin@centerpro.local | CenterPro@2026 |
| Lecturer | lecturer1@centerpro.local | Lecturer@12345 |
| Training Staff | staff@centerpro.local | Staff@12345 |

Lưu ý: Đây là các tài khoản demo phục vụ học tập, kiểm thử và trải nghiệm hệ thống.

## 6. Hướng dẫn cài đặt

**Bước 1: Clone dự án**

```bash
git clone https://github.com/CoTie206/CoTie206-K24CNT1-G03-DT03-CenterPro.git
```

**Bước 2: Mở dự án**

Mở file Solution (`.sln`) bằng Visual Studio.

**Bước 3: Cấu hình cơ sở dữ liệu**

- Cài đặt Microsoft SQL Server.
- Mở SQL Server Management Studio (SSMS).
- Thực hiện script SQL trong thư mục `database`.
- Kiểm tra và cấu hình chuỗi kết nối trong `appsettings.json` cho phù hợp với máy tính.

**Bước 4: Khôi phục thư viện**

Mở Terminal tại thư mục dự án và chạy:

```bash
dotnet restore
```

**Bước 5: Chạy chương trình**

Chọn project `CenterPro.Web` làm Startup Project, sau đó nhấn `F5` hoặc chạy:

```bash
dotnet run --project CenterPro.Web
```

## 7. Cấu trúc dự án

```text
CenterPro/
│
├── CenterPro.Web/
│   ├── Controllers/
│   ├── Views/
│   ├── Models/
│   ├── wwwroot/
│   └── Program.cs
│
├── database/
│   ├── CenterPro.Data/
│   │   ├── Domain/
│   │   ├── Infrastructure/
│   │   └── Migrations/
│   ├── CSDL_CenterPro.sql
│   └── README.md
│
└── README.md
```

## 8. Nhóm thực hiện

**Đề tài:** DT03 – Hệ thống quản lý trung tâm đào tạo CNTT (CenterPro).

**Nhóm thực hiện:**
- Hoàng Công Tiến
- Phạm Văn Thái
- Nghiêm Văn Trọng
- Vũ Hoàng Anh

**Lớp:** K24CNT1

**Trường:** Đại học Nguyễn Trãi

## 9. Mục đích dự án

Dự án được thực hiện nhằm vận dụng kiến thức lập trình web, xây dựng cơ sở dữ liệu và phân tích thiết kế hệ thống vào một sản phẩm thực tế phục vụ học tập.