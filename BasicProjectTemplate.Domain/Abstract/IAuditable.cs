namespace BasicProjectTemplate.Domain.Abstract;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}