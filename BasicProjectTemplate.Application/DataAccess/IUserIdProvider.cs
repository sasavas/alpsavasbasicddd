namespace BasicProjectTemplate.Application.DataAccess;

public interface IUserIdProvider
{
    Guid GetUserId();
}