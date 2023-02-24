namespace TTCompanion.API.FantasyFootball.Services
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
    }
}
