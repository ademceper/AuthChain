
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; } = false;
    
    public void SetUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

