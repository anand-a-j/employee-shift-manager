namespace ShiftManager.Api.Entities
{
    public class Business
    {
        public Guid Id { get; set; }
        public string Name {get;set;} = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}