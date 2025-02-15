using AuthChain.DTOs;

public interface IRealmService
{
    Task<IEnumerable<RealmDto>> GetAllAsync();
    Task<RealmDto> GetByIdAsync(Guid id);
    Task<RealmDto> CreateAsync(CreateRealmDto dto);
    Task<RealmDto> UpdateAsync(Guid id, UpdateRealmDto dto);
    Task<bool> DeleteAsync(Guid id);
}
