using BasicProjectTemplate.Domain.Abstract;

namespace BasicProjectTemplate.Domain.Features.Authentication.ValueObjects;

public class Password : ValueObject
{
    public Password(string password)
    {
        //TODO: hash
        Value = password;
    }
    public string Value { get; set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}