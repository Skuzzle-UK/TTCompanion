using TTCompanion.API.FantasyFootball.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services
{
    public class FFRepository : IFFRepository
    {
        private readonly FFContext _context;

        public FFRepository(FFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
