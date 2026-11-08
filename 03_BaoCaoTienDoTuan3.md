# Báo cáo tiến độ Tuần 3 – DT03 CenterPro

**Nhóm:** K24CNT1-G03  
**Mục tiêu tuần:** Hoàn thành nền tảng ứng dụng và các phân hệ quản lý cơ bản theo kế hoạch 8 tuần.

## Công việc và sản phẩm

| Hạng mục | Sản phẩm trong gói | Đối chiếu kế hoạch |
|---|---|---|
| Giao diện chung | Layout Bootstrap 5, menu điều hướng theo vai trò, trang tổng quan | Tuần 3 |
| Đăng nhập và phân quyền | ASP.NET Core Identity, đăng nhập/đăng xuất, vai trò `Administrator`, `TrainingStaff`, `Lecturer`, chặn quyền ở controller | Tuần 3 |
| Quản lý giảng viên | Danh sách, thêm, sửa, xóa; không cho xóa khi đã được lớp tham chiếu | Tuần 3 |
| Quản lý phòng học | Danh sách, thêm, sửa, xóa; không cho xóa khi lịch tham chiếu | Tuần 3 |
| Quản lý lớp học | Tạo/cập nhật/xóa lớp, liên kết giảng viên phụ trách | Tuần 3 |
| Kiểm tra đầu vào | Data Annotations, kiểm tra phía máy chủ và thông báo lỗi trên biểu mẫu | Tuần 3 |
| Dữ liệu mẫu | Migration và seed giảng viên, phòng, lớp, vai trò | Tuần 2–3 |

## Tài liệu của Tuần 2

Gói này giữ SRS v1.0, tài liệu phân tích thiết kế, sơ đồ Use Case/lớp/ERD, thiết kế CSDL và wireframe đã lập cho mốc cuối Tuần 2.

## Trạng thái chạy và việc cần hoàn tất tại máy nhóm

Mã nguồn đã được chuẩn bị trong `CenterPro.Web/` và dùng EF Core migration có sẵn trong `database/CenterPro.Data/`. `dotnet build CenterPro.Web/CenterPro.Web.csproj --no-restore` đã biên dịch thành công, 0 lỗi; NuGet báo không lấy được dữ liệu cảnh báo lỗ hổng do môi trường tạo gói không truy cập được `api.nuget.org`. Để demo, nhóm cần cài .NET SDK 10 và SQL Server LocalDB/Developer, cấu hình tài khoản demo bằng User Secrets, rồi chạy theo `README.md`. Môi trường tạo gói này chưa xác nhận kết nối đến SQL Server của nhóm; vì vậy chưa ghi nhận kết quả chạy thực tế trên máy nhóm.

Trước khi nộp, nhóm cần chạy ứng dụng và ghi kết quả thật: đăng nhập theo từng vai trò; CRUD giảng viên/phòng/lớp; gán giảng viên; kiểm tra trường bắt buộc, giới hạn độ dài, thử quyền truy cập sai vai trò. Chụp ảnh màn hình chạy được và cập nhật trạng thái này sau buổi kiểm tra.

## Ngoài phạm vi Tuần 3

Luồng gửi/duyệt đề xuất, quản lý và kiểm tra xung đột lịch đầu-cuối, Web API và màn hình ReactJS là các mốc sau trong kế hoạch; chưa được báo cáo là hoàn thành ở tuần này.
