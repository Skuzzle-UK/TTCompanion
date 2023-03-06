using TTCompanion.API.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services
{
    public class Repository : IRepository
    {
        private readonly DBContext _context;

        public Repository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
