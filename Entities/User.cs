namespace ShiftManager.Api.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name {get; set;} = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
        public Guid BusinessId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}