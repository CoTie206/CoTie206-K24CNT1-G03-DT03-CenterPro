# Tạo cơ sở dữ liệu CenterPro

`TaoCSDL_CenterPro.sql` là baseline xuất trực tiếp từ model EF Core hiện tại. Bảng nghiệp vụ được tạo ngay với tên tiếng Việt không dấu; script không đổi tên từ schema tiếng Anh. File có dữ liệu mẫu `HasData` và ghi lịch sử migration để ứng dụng chạy F5 không tạo lại bảng.

1. Nếu đã chạy file SQL cũ, hãy xóa database thử nghiệm `CenterProDb` trong SSMS trước khi chạy lại, nhưng chỉ khi database đó không có dữ liệu cần giữ. Script sẽ dừng và báo nếu phát hiện schema cũ.
2. Mở SSMS, kết nối `(localdb)\MSSQLLocalDB` bằng Windows Authentication.
3. Mở `TaoCSDL_CenterPro.sql`, nhấn **Execute**.
4. Mở `CenterPro.sln` trong Visual Studio và nhấn **F5** trên dự án `CenterPro.Web`.
