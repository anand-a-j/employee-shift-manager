namespace ShiftManager.Api.Entities
{
    public class Business
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name {get;set;} = null!;
    }
}