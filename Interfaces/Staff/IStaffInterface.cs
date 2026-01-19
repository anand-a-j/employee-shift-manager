namespace ShiftManager.Api.interfaces
{
    public interface IStaffService
    {
        Task<string> CreateStaffAsync(CreateStaffDto dto, Guid ownerUserId);
        Task<StaffResponseDto> UpdateStaffAsync(Guid staffId, UpdateStaffDto dto,Guid ownerUserId);
    }
}