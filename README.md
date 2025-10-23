# 🧍‍♂️ User Manager – ASP.NET Core MVC + Entity Framework Core (Code First)

## 📝 Giới thiệu

**User Manager** là một ứng dụng web được xây dựng bằng **ASP.NET Core MVC** và **Entity Framework Core (Code First)**, dùng để **quản lý thông tin người dùng** trong hệ thống.

Ứng dụng minh họa quy trình **CRUD cơ bản**, kết hợp với các kỹ thuật:
- ✅ **Xác thực dữ liệu (Data Validation)**
- 💬 **Thông báo giao diện (UI Notification)**
- 🗄️ **Tương tác cơ sở dữ liệu (Database Interaction)**

---

## ⚙️ Chức năng chính

- ➕ **Thêm / Sửa / Xóa / Xem chi tiết người dùng** (CRUD)  
- 🔍 **Kiểm tra trùng**: Mã người dùng, Email, Số điện thoại *(Remote Validation)*  
- ✅ **Kiểm tra định dạng dữ liệu** bằng *Data Annotations*  
- 💬 **Hiển thị thông báo thành công / lỗi** bằng *TempData* + *Bootstrap Alert*  

---

## 🧰 Công nghệ sử dụng

| Công nghệ | Mô tả |
|------------|--------|
| **ASP.NET Core MVC (.NET 8+)** | Framework xây dựng ứng dụng web |
| **Entity Framework Core (Code First)** | ORM để thao tác cơ sở dữ liệu |
| **Microsoft SQL Server** | Hệ quản trị cơ sở dữ liệu |
| **Bootstrap 5** | Giao diện người dùng |
| **jQuery** & **jQuery Unobtrusive Validation** | Hỗ trợ xác thực dữ liệu phía client |

---

## 🚀 Hướng dẫn chạy dự án

### 1️⃣ Clone repository

```bash
git clone https://github.com/Strikerzzzz/LamHaiTest_UserManager.git
cd LamHaiTest_UserManager
```
### 2️⃣ Mở project
Mở dự án bằng Visual Studio 2022 hoặc VS Code

⚠️ Yêu cầu: đã cài đặt .NET SDK 8.0 trở lên.

### 3️⃣ Cấu hình chuỗi kết nối
Mở file **`appsettings.json`** và chỉnh lại phần **DefaultConnection** cho phù hợp với môi trường của bạn:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=UserManagerDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
}
```
💡 Nếu bạn sử dụng SQL Server Express, thay bằng:

```json
"Server=.\\SQLEXPRESS;"
```
### 4️⃣ Tạo database từ migrations
Mở Terminal hoặc Command Prompt ngay trong thư mục gốc của project (nơi chứa file .csproj).

> Trong Visual Studio, bạn có thể mở bằng:  
> `View → Terminal` **hoặc** `Tools → Command Line → Developer Command Prompt`

Đảm bảo bạn đã cài đặt Entity Framework Core Tools (chỉ cần 1 lần duy nhất):
```bash
dotnet tool install --global dotnet-ef
```

Chạy lệnh sau để tạo cơ sở dữ liệu từ các migration có sẵn:
```bash
dotnet ef database update
```

Lệnh này sẽ tự động:

Tạo database (nếu chưa có)

Tạo các bảng dựa trên cấu trúc định nghĩa trong thư mục Migrations/

✅ Sau khi lệnh hoàn tất, bạn có thể mở SQL Server Management Studio (SSMS) để kiểm tra database đã được tạo thành công.
### 5️⃣ Chạy ứng dụng

🔹 Cách 1: Dùng Visual Studio
Chọn Run hoặc nhấn Ctrl + F5

🔹 Cách 2: Dùng dòng lệnh
```bash
dotnet run
```
Sau đó truy cập ứng dụng tại:

👉 http://localhost:5000
(hoặc URL mà Visual Studio cung cấp)

## 🧩 Thông tin thêm
📁 Dữ liệu được quản lý qua mô hình Code First

🔒 Có xác thực dữ liệu phía client và server

🎨 Giao diện đơn giản, thân thiện, dễ mở rộng

🧑‍💻 Tác giả **Nguyễn Tuấn Tiến**

📧 Email: nguyentuantienit@gmail.com

🌐 GitHub: github.com/Strikerzzzz
