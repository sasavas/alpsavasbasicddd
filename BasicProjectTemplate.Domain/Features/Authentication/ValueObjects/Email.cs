using BasicProjectTemplate.Domain.Abstract;

namespace BasicProjectTemplate.Domain.Features.Authentication.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; set; }

    public Email(string email)
    {
        this.Value = email;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}