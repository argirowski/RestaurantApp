namespace Application.Claims
{
    public record CurrentUser(string Id, string Email, string Password, IEnumerable<string> Roles)
    {
        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    }
}
