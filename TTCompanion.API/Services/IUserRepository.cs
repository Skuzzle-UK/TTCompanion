namespace TTCompanion.API.Services
{
    public interface IUserRepository
    {
        public Task<Entities.User?> GetUser(string username, string password);
    }
}