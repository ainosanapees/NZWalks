namespace NZWalks.Api.Repositories
{
    public interface IUserRepository
    {
        Task<bool>AuthenticateAsync(string userName, string Password);  
    }
}
