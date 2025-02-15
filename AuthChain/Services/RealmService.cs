using AuthChain.DTOs;

public class RealmService : IRealmService
{
    private readonly IRepository<Realm> _repository;

    public RealmService(IRepository<Realm> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RealmDto>> GetAllAsync()
    {
        var realms = await _repository.GetAllAsync();
        return realms.Select(MapToDto);
    }

    public async Task<RealmDto> GetByIdAsync(Guid id)
    {
        var realm = await _repository.GetByIdAsync(id);
        if (realm == null) 
            throw new KeyNotFoundException("Realm not found");

        return MapToDto(realm);
    }

    public async Task<RealmDto> CreateAsync(CreateRealmDto dto)
    {
        var realm = new Realm
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _repository.AddAsync(realm);
        return MapToDto(realm);
    }

    public async Task<RealmDto> UpdateAsync(Guid id, UpdateRealmDto dto)
    {
        var realm = await _repository.GetByIdAsync(id);
        if (realm == null) 
            throw new KeyNotFoundException("Realm not found");

        realm.Name = dto.Name;
        realm.Description = dto.Description;
        realm.SetUpdated();

        await _repository.UpdateAsync(realm);
        return MapToDto(realm);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var realm = await _repository.GetByIdAsync(id);
        if (realm == null) return false;

        await _repository.SoftDeleteAsync(id); 
        return true;
    }

    private static RealmDto MapToDto(Realm realm) => new RealmDto
    {
        Id = realm.Id,
        Name = realm.Name,
        Description = realm.Description,
        CreatedAt = realm.CreatedAt,
        UpdatedAt = realm.UpdatedAt
    };
}
