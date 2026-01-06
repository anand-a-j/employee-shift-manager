

---bash
ShiftManager.Api
    │
    ├── Controllers
    │   ├── AuthController.cs
    │   ├── BusinessController.cs
    │   ├── StaffController.cs
    │   ├── ShiftController.cs
    │   ├── NotificationController.cs
    │   └── HealthController.cs
    │
    ├── Entities
    │   ├── User.cs
    │   ├── Business.cs
    │   ├── Shift.cs
    │   └── Notification.cs
    │
    ├── DTOs
    │   ├── Auth
    │   ├── Staff
    │   ├── Shift
    │   └── Notification
    │
    ├── Enums
    │   ├── UserRole.cs
    │   └── ShiftStatus.cs
    │
    ├── Services
    │   ├── AuthService.cs
    │   ├── StaffService.cs
    │   ├── ShiftService.cs
    │   └── NotificationService.cs
    │
    ├── Data
    │   ├── AppDbContext.cs
    │   └── DbSeeder.cs
    │
    ├── Middleware
    │   └── ExceptionMiddleware.cs
    │
    ├── Common
    │   └── ApiResponse.cs
    │
    ├── Program.cs
    └── appsettings.json
---