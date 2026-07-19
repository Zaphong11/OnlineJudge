# 📖 HpuniJudgeApi — Hướng Dẫn Clean Architecture

> Đọc file này để biết: **viết gì → ở đâu → theo thứ tự nào**.

---

## 1. Cấu Trúc 4 Tầng

```
HpuniJudgeApi/
├── HpuniJudgeApi.Domain           💎 Tầng trong cùng — Entity (model database)
├── HpuniJudgeApi.Application      ⚙️ Tầng logic      — DTOs, Interfaces, Services
├── HpuniJudgeApi.Infrastructure   🗄️ Tầng hạ tầng    — DbContext, Repositories, Migrations
└── HpuniJudgeApi.API              🌐 Tầng ngoài cùng — Controllers, Program.cs
```

---

## 2. Ai Reference Ai?

```
Domain         → (không reference gì)
Application    → Domain
Infrastructure → Application (gián tiếp có Domain)
API            → Application + Infrastructure
```

> **Quy tắc**: Tầng trong KHÔNG BAO GIỜ biết tầng ngoài tồn tại.

---

## 3. Viết Gì Ở Đâu?

### 💎 Domain — `Entities/`

Chỉ chứa **Entity class** = model đại diện cho bảng database.

```
Domain/
├── Entities/
│   └── {Tên}Entity.cs        ← Chỉ có property, không có logic
└── DependencyInjection.cs
```

**Namespace**: `HpuniJudgeApi.Domain.Entities`

---

### ⚙️ Application — `DTOs/` + `Interfaces/` + `Services/`

Chứa **toàn bộ business logic** và **định nghĩa interface**.

```
Application/
├── DTOs/
│   └── {Module}/
│       ├── {Action}Request.cs       ← Dữ liệu client gửi lên
│       └── {Action}Response.cs      ← Dữ liệu trả về client
├── Interfaces/
│   ├── I{Module}Service.cs          ← Interface service
│   └── Repositories/
│       └── I{Entity}Repository.cs   ← Interface repository (chỉ KHAI BÁO, không implement)
├── Services/
│   └── {Module}Service.cs           ← Implement business logic
└── DependencyInjection.cs           ← Đăng ký: services.AddScoped<IService, Service>()
```

**Lưu ý quan trọng**: Interface repository **khai báo ở Application** nhưng **implement ở Infrastructure**.

---

### 🗄️ Infrastructure — `Data/` + `Repositories/` + `Migrations/`

Chứa **mọi thứ liên quan database**: cấu hình bảng, truy vấn, migration.

```
Infrastructure/
├── Data/
│   └── AppDbContext.cs              ← DbSet, cấu hình bảng, quan hệ, seed data
├── Repositories/
│   └── {Entity}Repository.cs       ← Implement I{Entity}Repository (CRUD database)
├── Migrations/                      ← Tự sinh bởi dotnet ef
└── DependencyInjection.cs           ← Đăng ký: DbContext + services.AddScoped<IRepo, Repo>()
```

---

### 🌐 API — `Controllers/`

Chứa **endpoint HTTP**. Controller chỉ validate input rồi gọi Service.

```
API/
├── Controllers/
│   └── {Module}Controller.cs        ← Nhận request, gọi service, trả response
├── DependencyInjection.cs           ← Gọi chuỗi DI: AddApplicationDI() + AddInfrastructureDI()
├── Program.cs                       ← Entry point, cấu hình middleware
└── appsettings.json                 ← Connection string, JWT, config
```

---

## 4. Flow Xử Lý Request

```
Client gửi HTTP request
  → Controller (validate sơ bộ)
    → Service (business logic)
      → Repository (truy vấn DB)
        → Database
      ← trả kết quả
    ← trả kết quả
  ← trả kết quả
← Client nhận response
```

> **Quy tắc**: Mỗi tầng chỉ gọi tầng ngay bên dưới, không nhảy cóc.  
> Controller gọi **Service interface**, Service gọi **Repository interface**.

---

## 5. Chuỗi Dependency Injection

```
Program.cs
  └── builder.Services.AddAppDI(config)          ← API/DependencyInjection.cs
        ├── services.AddApplicationDI()           ← Application/DependencyInjection.cs
        │     └── AddScoped<IService, Service>()  ← Đăng ký service ở đây
        └── services.AddInfrastructureDI(config)  ← Infrastructure/DependencyInjection.cs
              ├── AddDbContext<AppDbContext>()
              └── AddScoped<IRepo, Repo>()        ← Đăng ký repository ở đây
```

> **Quên đăng ký DI** → Lỗi runtime: `Unable to resolve service for type 'I...'`

---

## 6. Thứ Tự Code Khi Thêm Module Mới

Luôn code từ **trong ra ngoài**: Domain → Application → Infrastructure → API.

```
1. [Domain]          Tạo Entity
2. [Application]     Tạo DTO (Request/Response)
3. [Application]     Tạo Repository Interface
4. [Application]     Tạo Service Interface
5. [Application]     Implement Service
6. [Infrastructure]  Thêm DbSet + cấu hình bảng trong AppDbContext
7. [Infrastructure]  Implement Repository
8. [API]             Tạo Controller
9. [Application]     Đăng ký DI cho Service
10.[Infrastructure]  Đăng ký DI cho Repository
11.[Terminal]        dotnet ef migrations add ... → dotnet ef database update ...
```

---

## 7. Convention Đặt Tên

| Loại | Tên file | Namespace |
|---|---|---|
| Entity | `{Tên}Entity.cs` | `Domain.Entities` |
| DTO | `{Action}Request.cs` / `{Action}Response.cs` | `Application.DTOs.{Module}` |
| Service Interface | `I{Module}Service.cs` | `Application.Interfaces` |
| Repository Interface | `I{Entity}Repository.cs` | `Application.Interfaces.Repositories` |
| Service Class | `{Module}Service.cs` | `Application.Services` |
| Repository Class | `{Entity}Repository.cs` | `Infrastructure.Repositories` |
| Controller | `{Module}Controller.cs` | `API.Controllers` |

---

## 8. Lệnh Migration

```bash
# Tạo migration
dotnet ef migrations add <Tên> --project HpuniJudgeApi.Infrastructure --startup-project HpuniJudgeApi.API

# Áp dụng vào database
dotnet ef database update --project HpuniJudgeApi.Infrastructure --startup-project HpuniJudgeApi.API

# Xóa migration cuối (chưa apply)
dotnet ef migrations remove --project HpuniJudgeApi.Infrastructure --startup-project HpuniJudgeApi.API
```

---

## 9. Quy Tắc Quan Trọng

- **Controller**: Chỉ validate + gọi service. KHÔNG chứa business logic.
- **Service**: Chứa business logic. Inject **interface**, không inject class cụ thể.
- **Repository**: Chỉ CRUD database. KHÔNG chứa business logic.
- **Entity**: Chỉ property. KHÔNG chứa method logic.
- **DI**: Dùng `AddScoped` (1 instance/request) cho Service và Repository.
- **Async**: Tất cả method truy vấn DB dùng `async/await` + hậu tố `Async`.
