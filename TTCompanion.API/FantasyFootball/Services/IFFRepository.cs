namespace TTCompanion.API.FantasyFootball.Services
{
    public interface IFFRepository
    {
        Task<bool> SaveChangesAsync();
    }
}
