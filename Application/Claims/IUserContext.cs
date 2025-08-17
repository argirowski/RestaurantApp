namespace Application.Claims
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}
