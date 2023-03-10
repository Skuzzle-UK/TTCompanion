using TTCompanion.API.DBContexts;

namespace TTCompanion.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;

        public UserRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Entities.User?> GetUser(string username, string password)
        {
            var result = _context.Users as IQueryable<Entities.User>;
            
            var user = result
            .Where(u => u.Username == username)
            .FirstOrDefault();

            if(user == null)
            {
                return null;
            }

            return user;
        }
    }
}
